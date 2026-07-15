#ifndef UNIVERSAL_FORWARD_LIT_PASS_INCLUDED
#define UNIVERSAL_FORWARD_LIT_PASS_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
#if defined(LOD_FADE_CROSSFADE)
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/LODCrossFade.hlsl"
#endif

// GLES2 has limited amount of interpolators
#if defined(_PARALLAXMAP) && !defined(SHADER_API_GLES)
#define REQUIRES_TANGENT_SPACE_VIEW_DIR_INTERPOLATOR
#endif

#if (defined(_NORMALMAP) || (defined(_PARALLAXMAP) && !defined(REQUIRES_TANGENT_SPACE_VIEW_DIR_INTERPOLATOR))) || defined(_DETAIL)
#define REQUIRES_WORLD_SPACE_TANGENT_INTERPOLATOR
#endif
float3 RotateAroundYInDegrees(float3 vertex, float degrees)
{
    float alpha = degrees * 3.14 / 180.0;
    float sina, cosa;
    sincos(alpha, sina, cosa);
    float2x2 m = float2x2(cosa, -sina, sina, cosa);
    return float3(mul(m, vertex.xz), vertex.y).xzy;
}
// keep this file in sync with LitGBufferPass.hlsl

struct Attributes
{
    float4 positionOS   : POSITION;
    float3 normalOS     : NORMAL;
    float4 tangentOS    : TANGENT;
    float2 texcoord     : TEXCOORD0;
    float2 staticLightmapUV   : TEXCOORD1;
    float2 dynamicLightmapUV  : TEXCOORD2;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct Varyings
{
    float2 uv                       : TEXCOORD0;

#if defined(REQUIRES_WORLD_SPACE_POS_INTERPOLATOR)
    float3 positionWS               : TEXCOORD1;
#endif

    float3 normalWS                 : TEXCOORD2;
// #if defined(REQUIRES_WORLD_SPACE_TANGENT_INTERPOLATOR)
    half4 tangentWS                : TEXCOORD3;    // xyz: tangent, w: sign
// #endif
    half3 bitangentWS               : TEXCOORD4;

#ifdef _ADDITIONAL_LIGHTS_VERTEX
    half4 fogFactorAndVertexLight   : TEXCOORD5; // x: fogFactor, yzw: vertex light
#else
    half  fogFactor                 : TEXCOORD5;
#endif

#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
    float4 shadowCoord              : TEXCOORD6;
#endif

#if defined(REQUIRES_TANGENT_SPACE_VIEW_DIR_INTERPOLATOR)
    half3 viewDirTS                : TEXCOORD7;
#endif

    DECLARE_LIGHTMAP_OR_SH(staticLightmapUV, vertexSH, 8);
#ifdef DYNAMICLIGHTMAP_ON
    float2  dynamicLightmapUV : TEXCOORD9; // Dynamic lightmap UVs
#endif

    float4 positionCS               : SV_POSITION;
    UNITY_VERTEX_INPUT_INSTANCE_ID
    UNITY_VERTEX_OUTPUT_STEREO
};

void InitializeInputData(Varyings input, half3 normalTS, out InputData inputData)
{
    inputData = (InputData)0;

#if defined(REQUIRES_WORLD_SPACE_POS_INTERPOLATOR)
    inputData.positionWS = input.positionWS;
#endif

    half3 viewDirWS = GetWorldSpaceNormalizeViewDir(input.positionWS);
#if defined(_NORMALMAP) || defined(_DETAIL)
    float sgn = input.tangentWS.w;      // should be either +1 or -1
    float3 bitangent = sgn * cross(input.normalWS.xyz, input.tangentWS.xyz);
    half3x3 tangentToWorld = half3x3(input.tangentWS.xyz, bitangent.xyz, input.normalWS.xyz);

    #if defined(_NORMALMAP)
    inputData.tangentToWorld = tangentToWorld;
    #endif
    inputData.normalWS = TransformTangentToWorld(normalTS, tangentToWorld);
#else
    inputData.normalWS = input.normalWS;
#endif

    inputData.normalWS = NormalizeNormalPerPixel(inputData.normalWS);
    inputData.viewDirectionWS = viewDirWS;

#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
    inputData.shadowCoord = input.shadowCoord;
#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
    inputData.shadowCoord = TransformWorldToShadowCoord(inputData.positionWS);
#else
    inputData.shadowCoord = float4(0, 0, 0, 0);
#endif
#ifdef _ADDITIONAL_LIGHTS_VERTEX
    inputData.fogCoord = InitializeInputDataFog(float4(input.positionWS, 1.0), input.fogFactorAndVertexLight.x);
    inputData.vertexLighting = input.fogFactorAndVertexLight.yzw;
#else
    inputData.fogCoord = InitializeInputDataFog(float4(input.positionWS, 1.0), input.fogFactor);
#endif

#if defined(DYNAMICLIGHTMAP_ON)
    inputData.bakedGI = SAMPLE_GI(input.staticLightmapUV, input.dynamicLightmapUV, input.vertexSH, inputData.normalWS);
#else
    inputData.bakedGI = SAMPLE_GI(input.staticLightmapUV, input.vertexSH, inputData.normalWS);
#endif

    inputData.normalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(input.positionCS);
    inputData.shadowMask = SAMPLE_SHADOWMASK(input.staticLightmapUV);

    #if defined(DEBUG_DISPLAY)
    #if defined(DYNAMICLIGHTMAP_ON)
    inputData.dynamicLightmapUV = input.dynamicLightmapUV;
    #endif
    #if defined(LIGHTMAP_ON)
    inputData.staticLightmapUV = input.staticLightmapUV;
    #else
    inputData.vertexSH = input.vertexSH;
    #endif
    #endif
}

static float3 bakedGI = 0;
static float speMark = 1.0;;
half3 LightingPhysicallyBasedCartoon(BRDFData brdfData, BRDFData brdfDataClearCoat,
    half3 lightColor, half3 lightDirectionWS,
    half3 normalWS, half3 viewDirectionWS,
    half clearCoatMask, bool specularHighlightsOff, half3 normalWSNoMap)
{
    
    half NoV = saturate(dot(normalWS, viewDirectionWS));
    half3 brdf = 0.0;
#ifndef _SPECULARHIGHLIGHTS_OFF
    [branch] if (!specularHighlightsOff)
    {
        brdf += brdfData.specular * DirectBRDFSpecular(brdfData, normalWS, lightDirectionWS, viewDirectionWS);

#if defined(_CLEARCOAT) || defined(_CLEARCOATMAP)
        // Clear coat evaluates the specular a second timw and has some common terms with the base specular.
        // We rely on the compiler to merge these and compute them only once.
        half brdfCoat = kDielectricSpec.r * DirectBRDFSpecular(brdfDataClearCoat, normalWS, lightDirectionWS, viewDirectionWS);
        
        // Mix clear coat and base layer using khronos glTF recommended formula
        // https://github.com/KhronosGroup/glTF/blob/master/extensions/2.0/Khronos/KHR_materials_clearcoat/README.md
        // Use NoV for direct too instead of LoH as an optimization (NoV is light invariant).
        
        // Use slightly simpler fresnelTerm (Pow4 vs Pow5) as a small optimization.
        // It is matching fresnel used in the GI/Env, so should produce a consistent clear coat blend (env vs. direct)
        half coatFresnel = kDielectricSpec.x + kDielectricSpec.a * Pow4(1.0 - NoV);

        brdf = brdf * (1.0 - clearCoatMask * coatFresnel) + brdfCoat * clearCoatMask;
#endif // _CLEARCOAT
    }
#endif // _SPECULARHIGHLIGHTS_OFF
    float3 light1 = RotateAroundYInDegrees(-viewDirectionWS, _BackLightOffset);
    float ndl1 = dot(normalWS, light1);
    ndl1 = smoothstep(0, backLightEdge, ndl1);
    float3 light2 = RotateAroundYInDegrees(-viewDirectionWS, _BackLightOffset2);
    float ndl2 = dot(normalWS, light2);
    ndl2 = smoothstep(0, backLightEdge2, ndl2);
    
    bakedGI = lerp(bakedGI.xyz,_SecondShadowMultColor.rgb,_SecondShadowMultColor.a);
 #if _CARTOON
    half nDotLBase = dot(normalWSNoMap, lightDirectionWS);
    half hlambert = nDotLBase * 0.5 + 0.5;
    float t = smoothstep(_ShadowFeather - _ShadowRange, _ShadowFeather, hlambert);
    half3 col = lerp(bakedGI.xyz, 1, t);
    half3 radiance = lightColor * col;
    half NdotL = saturate(nDotLBase);
 
    return brdf * lightColor * NdotL * speMark + brdfData.diffuse * col + lerp(bakedGI.rgb, _BackLightColor.rgb, _BackLightColor.a) * ndl1 + lerp(bakedGI.rgb, _BackLightColor2.rgb, _BackLightColor2.a) * ndl2 + _FnlColor.rgb* pow( 1.0-max(NoV,0),_Fnl);
#else

    half NdotL = saturate(dot(normalWS, lightDirectionWS));
    half3 radiance = lightColor * NdotL;
    
    return (brdf * speMark + brdfData.diffuse) * radiance + lerp(bakedGI.rgb, _BackLightColor.rgb, _BackLightColor.a) * ndl1 + lerp(bakedGI.rgb, _BackLightColor2.rgb, _BackLightColor2.a) * ndl2 + _FnlColor.rgb* pow( 1.0-max(NoV,0),_Fnl);

#endif 
    
}

half3 LightingPhysicallyBasedCartoon(BRDFData brdfData, BRDFData brdfDataClearCoat, Light light, half3 normalWS, half3 viewDirectionWS, half clearCoatMask, bool specularHighlightsOff, half3 normalWSNoMap)
{
    return LightingPhysicallyBasedCartoon(brdfData, brdfDataClearCoat, light.color, light.direction, normalWS, viewDirectionWS, clearCoatMask, specularHighlightsOff, normalWSNoMap);
}
half3 LightingPhysicallyBasedCartoon(BRDFData brdfData, Light light, half3 normalWS, half3 viewDirectionWS, bool specularHighlightsOff, half3 normalWSNoMap)
{
    const BRDFData noClearCoat = (BRDFData)0;
    return LightingPhysicallyBasedCartoon(brdfData, noClearCoat, light, normalWS, viewDirectionWS, 0.0, specularHighlightsOff, normalWSNoMap);
}
// Backwards compatibility
half3 LightingPhysicallyBasedCartoon(BRDFData brdfData, Light light, half3 normalWS, half3 viewDirectionWS, half3 normalWSNoMap)
{
#ifdef _SPECULARHIGHLIGHTS_OFF
    bool specularHighlightsOff = true;
#else
    bool specularHighlightsOff = false;
#endif
    const BRDFData noClearCoat = (BRDFData)0;
    return LightingPhysicallyBasedCartoon(brdfData, noClearCoat, light, normalWS, viewDirectionWS, 0.0, specularHighlightsOff, normalWSNoMap);
}

half3 LightingPhysicallyBasedCartoon(BRDFData brdfData, half3 lightColor, half3 lightDirectionWS, half3 normalWS, half3 viewDirectionWS, bool specularHighlightsOff, half3 normalWSNoMap)
{
    Light light;
    light.color = lightColor;
    light.direction = lightDirectionWS;
    light.shadowAttenuation = 1;
    return LightingPhysicallyBasedCartoon(brdfData, light, viewDirectionWS, specularHighlightsOff, specularHighlightsOff, normalWSNoMap);
}

half3 CalculateLightingColorCartoon(LightingData lightingData, half3 albedo)
{
    half3 lightingColor = 0;

    /**if (IsOnlyAOLightingFeatureEnabled())
    {
        return lightingData.giColor; // Contains white + AO
    }*/

    if (IsLightingFeatureEnabled(DEBUGLIGHTINGFEATUREFLAGS_GLOBAL_ILLUMINATION))
    {
        lightingColor += lightingData.giColor;
    } 

    if (IsLightingFeatureEnabled(DEBUGLIGHTINGFEATUREFLAGS_MAIN_LIGHT))
    {
        lightingColor += lightingData.mainLightColor;
    }

    /*if (IsLightingFeatureEnabled(DEBUGLIGHTINGFEATUREFLAGS_ADDITIONAL_LIGHTS))
    {
        lightingColor += lightingData.additionalLightsColor;
    }

    if (IsLightingFeatureEnabled(DEBUGLIGHTINGFEATUREFLAGS_VERTEX_LIGHTING))
    {
        lightingColor += lightingData.vertexLightingColor;
    }*/

    lightingColor *= albedo;

    /*if (IsLightingFeatureEnabled(DEBUGLIGHTINGFEATUREFLAGS_EMISSION))
    {
        lightingColor += lightingData.emissionColor;
    }*/

    return lightingColor;
}
half4 CalculateFinalColorCartoon(LightingData lightingData, half alpha)
{
    half3 finalColor = CalculateLightingColorCartoon(lightingData, 1);

    return half4(finalColor, alpha);
}

half4 CalculateFinalColorCartoon(LightingData lightingData, half3 albedo, half alpha, float fogCoord)
{
#if defined(_FOG_FRAGMENT)
#if (defined(FOG_LINEAR) || defined(FOG_EXP) || defined(FOG_EXP2))
    float viewZ = -fogCoord;
    float nearToFarZ = max(viewZ - _ProjectionParams.y, 0);
    half fogFactor = ComputeFogFactorZ0ToFar(nearToFarZ);
#else
    half fogFactor = 0;
#endif
#else
    half fogFactor = fogCoord;
#endif
    half3 lightingColor = CalculateLightingColor(lightingData, albedo);
    half3 finalColor = MixFog(lightingColor, fogFactor);

    return half4(finalColor, alpha);
}




half4 UniversalFragmentPBRCartoon(InputData inputData, SurfaceData surfaceData, half3 normalWSNoMap)
{
#if defined(_SPECULARHIGHLIGHTS_OFF)
    bool specularHighlightsOff = true;
#else
    bool specularHighlightsOff = false;
#endif
    BRDFData brdfData;

    // NOTE: can modify "surfaceData"...
    InitializeBRDFData(surfaceData, brdfData);

#if defined(DEBUG_DISPLAY)
    half4 debugColor;

    if (CanDebugOverrideOutputColor(inputData, surfaceData, brdfData, debugColor))
    {
        return debugColor;
    }
#endif

    // Clear-coat calculation...
    BRDFData brdfDataClearCoat = CreateClearCoatBRDFData(surfaceData, brdfData);
    half4 shadowMask = CalculateShadowMask(inputData);
    AmbientOcclusionFactor aoFactor = CreateAmbientOcclusionFactor(inputData, surfaceData);
    uint meshRenderingLayers = GetMeshRenderingLayer();
    Light mainLight = GetMainLight(inputData, shadowMask, aoFactor);

    // NOTE: We don't apply AO to the GI here because it's done in the lighting calculation below...
    MixRealtimeAndBakedGI(mainLight, inputData.normalWS, inputData.bakedGI);

    LightingData lightingData = CreateLightingData(inputData, surfaceData);
   
    bakedGI = inputData.bakedGI;
    
    lightingData.giColor = GlobalIllumination(brdfData, brdfDataClearCoat, surfaceData.clearCoatMask,
        inputData.bakedGI, aoFactor.indirectAmbientOcclusion, inputData.positionWS,
        inputData.normalWS, inputData.viewDirectionWS, inputData.normalizedScreenSpaceUV);
#ifdef _LIGHT_LAYERS
    if (IsMatchingLightLayer(mainLight.layerMask, meshRenderingLayers))
#endif
    {
        lightingData.mainLightColor = LightingPhysicallyBasedCartoon(brdfData, brdfDataClearCoat,
            mainLight,
            inputData.normalWS, inputData.viewDirectionWS,
            surfaceData.clearCoatMask, specularHighlightsOff, normalWSNoMap);
    }
 
#if REAL_IS_HALF
    // Clamp any half.inf+ to HALF_MAX
    return min(CalculateFinalColorCartoon(lightingData, surfaceData.alpha), HALF_MAX);
#else
    return CalculateFinalColorCartoon(lightingData, surfaceData.alpha);
#endif
}





VertexPositionInputs GetVertexPositionInputsOL(float3 positionOS, float3 normalWS, float _offset)
{

    VertexPositionInputs input;
    input.positionWS = TransformObjectToWorld(positionOS);
    

 
  
	//normalCS.x*=aspect;
   
	
    //input.positionWS += normalWS * len *_EdgeThickness * _offset;
 
    input.positionVS = TransformWorldToView(input.positionWS);
    input.positionCS = TransformWorldToHClip(input.positionWS);


    float4 nearUpperRight = mul(unity_CameraInvProjection, float4(1, 1, UNITY_NEAR_CLIP_VALUE, _ProjectionParams.y));
	float aspect = abs(nearUpperRight.y / nearUpperRight.x);
 
	float3 viewNormal =  TransformWorldToViewNormal(normalWS);

    float3 clipNormal =  mul((float3x3)GetViewToHClipMatrix(), viewNormal); 
    // return mul(GetViewToHClipMatrix(), float4(positionVS, 1.0));
	//float3 clipNormal = TransformViewToProjection(viewNormal.xyz);
	float2 projectedNormal = normalize(clipNormal.xy);
	projectedNormal *= min(input.positionCS.w,2);
	projectedNormal.x *= aspect;
	input.positionCS.xy += _EdgeThickness  * _offset * projectedNormal.xy * saturate(1 - abs(normalize(viewNormal).z)); // ignore offset when normal toward camera
	
    float4 ndc = input.positionCS * 0.5f;
    input.positionNDC.xy = float2(ndc.x, ndc.y * _ProjectionParams.x) + ndc.w;
    input.positionNDC.zw = input.positionCS.zw;

    return input;
}
///////////////////////////////////////////////////////////////////////////////
//                  Vertex and Fragment functions                            //
///////////////////////////////////////////////////////////////////////////////

// Used in Standard (Physically Based) shader
Varyings LitPassVertex(Attributes input)
{
    Varyings output = (Varyings)0;

    UNITY_SETUP_INSTANCE_ID(input);
    UNITY_TRANSFER_INSTANCE_ID(input, output);
    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
    half4 mask = SAMPLE_TEXTURE2D_LOD(_PbrMask, sampler_PbrMask, output.uv, 0);
    VertexNormalInputs normalInput = GetVertexNormalInputs(input.normalOS, input.tangentOS);
#if defined(_OUT_LINE)
    VertexPositionInputs vertexInput = GetVertexPositionInputsOL(input.positionOS.xyz, normalInput.normalWS, mask.b);
#else
    //VertexPositionInputs vertexInput = GetVertexPositionInputsOL(input.positionOS.xyz, normalInput.normalWS, 1);
    VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
#endif
    // normalWS and tangentWS already normalize.
    // this is required to avoid skewing the direction during interpolation
    // also required for per-vertex lighting and SH evaluation
   



    half3 vertexLight = VertexLighting(vertexInput.positionWS, normalInput.normalWS);

    half fogFactor = 0;
    #if !defined(_FOG_FRAGMENT)
        fogFactor = ComputeFogFactor(vertexInput.positionCS.z);
    #endif

    output.uv = TRANSFORM_TEX(input.texcoord, _BaseMap);

    // already normalized from normal transform to WS.
    output.normalWS = normalInput.normalWS;
    output.bitangentWS = normalInput.bitangentWS;
#if defined(REQUIRES_WORLD_SPACE_TANGENT_INTERPOLATOR) || defined(REQUIRES_TANGENT_SPACE_VIEW_DIR_INTERPOLATOR)
    real sign = input.tangentOS.w * GetOddNegativeScale();
    half4 tangentWS = half4(normalInput.tangentWS.xyz, sign);
#endif
#if defined(REQUIRES_WORLD_SPACE_TANGENT_INTERPOLATOR)
    output.tangentWS = tangentWS;
#endif

#if defined(REQUIRES_TANGENT_SPACE_VIEW_DIR_INTERPOLATOR)
    half3 viewDirWS = GetWorldSpaceNormalizeViewDir(vertexInput.positionWS);
    half3 viewDirTS = GetViewDirectionTangentSpace(tangentWS, output.normalWS, viewDirWS);
    output.viewDirTS = viewDirTS;
#endif

    OUTPUT_LIGHTMAP_UV(input.staticLightmapUV, unity_LightmapST, output.staticLightmapUV);
#ifdef DYNAMICLIGHTMAP_ON
    output.dynamicLightmapUV = input.dynamicLightmapUV.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
#endif
    OUTPUT_SH(output.normalWS.xyz, output.vertexSH);
#ifdef _ADDITIONAL_LIGHTS_VERTEX
    output.fogFactorAndVertexLight = half4(fogFactor, vertexLight);
#else
    output.fogFactor = fogFactor;
#endif

#if defined(REQUIRES_WORLD_SPACE_POS_INTERPOLATOR)
    output.positionWS = vertexInput.positionWS;
#endif

#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
    output.shadowCoord = GetShadowCoord(vertexInput);
#endif

    output.positionCS = vertexInput.positionCS;

    return output;
}

// Used in Standard (Physically Based) shader
void LitPassFragment(
    Varyings input
    , out half4 outColor : SV_Target0
#ifdef _WRITE_RENDERING_LAYERS
    , out float4 outRenderingLayers : SV_Target1
#endif
)
{
    UNITY_SETUP_INSTANCE_ID(input);
    UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
     SurfaceData surfaceData;
    InitializeStandardLitSurfaceData(input.uv, surfaceData);
   
    clip(surfaceData.alpha-_AlphaClip);
#if defined(_OUT_LINE)

    outColor = _EdgeColor  ;
   
#else

   
    speMark = surfaceData.occlusion * _SpecPower;
    surfaceData.occlusion = 1.0;
    
    
#ifdef LOD_FADE_CROSSFADE
    LODFadeCrossFade(input.positionCS);
#endif

    InputData inputData;
    InitializeInputData(input, surfaceData.normalTS, inputData);
    half3 normalWSNoMap = inputData.normalWS;
    inputData.normalWS = normalize(mul(UnpackNormal(SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, input.uv)), float3x3(input.tangentWS.xyz, input.bitangentWS, input.normalWS)));
    SETUP_DEBUG_TEXTURE_DATA(inputData, input.uv, _BaseMap);
    
#ifdef _DBUFFER
    ApplyDecalToSurfaceData(input.positionCS, surfaceData, inputData);
#endif

    half4 color = UniversalFragmentPBRCartoon(inputData, surfaceData, normalWSNoMap);
    color.rgb = MixFog(color.rgb, inputData.fogCoord);
    color.a = color.a;

    outColor = color;
    outColor = min(1.0, outColor); 
    //outColor.rgb = inputData.albedo;
    outColor.rgb = lerp(outColor.rgb, surfaceData.albedo.rgb * surfaceData.emission,  min(1.0, surfaceData.emission ) );

	
    //outColor = surfaceData.occlusion * _SpecPower;
    //return speMark;
    //CalculateOutlineColor(surfaceData.albedo.rgb);
    //return surfaceData.albedo.rgbb;
   // outColor.rgb = MixFog(outColor.rgb, inputData.fogCoord);
#endif
#ifdef _WRITE_RENDERING_LAYERS
    uint renderingLayers = GetMeshRenderingLayer();
    outRenderingLayers = float4(EncodeMeshRenderingLayer(renderingLayers), 0, 0, 0);
#endif
}

#endif
