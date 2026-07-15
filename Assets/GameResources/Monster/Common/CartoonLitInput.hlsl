#ifndef UNIVERSAL_LIT_INPUT_INCLUDED
#define UNIVERSAL_LIT_INPUT_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/CommonMaterial.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/SurfaceInput.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/ParallaxMapping.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DBuffer.hlsl"

#if defined(_DETAIL_MULX2) || defined(_DETAIL_SCALED)
#define _DETAIL
#endif

// NOTE: Do not ifdef the properties here as SRP batcher can not handle different layouts.
CBUFFER_START(UnityPerMaterial)
float4 _BaseMap_ST;
float4 _DetailAlbedoMap_ST;
half4 _BaseColor;
half4 _SpecColor;
half4 _EmissionColor;
half _Cutoff;
half _Smoothness;
 
half _Metallic;
half _BumpScale;
half _Parallax;
half _OcclusionStrength;
half _ClearCoatMask;
half _ClearCoatSmoothness;
half _DetailAlbedoMapScale;
half _DetailNormalMapScale;
half _Surface;
half _Brightness;
half _Saturation;
half _Contrast;
half4 _SecondShadowMultColor;
 

/*half4 _SpecColor;
half4 _EmissionColor;


half _Metallic;
half _BumpScale;
half _Parallax;
half _OcclusionStrength;
half _ClearCoatMask;
half _ClearCoatSmoothness;
half _DetailAlbedoMapScale;
half _DetailNormalMapScale;*/




float4 _LightAreaMultColor;
float _ShadowFeather;
float _ShadowRange;
float4 _FnlColor;
float _Fnl;




 
 
half _EdgeThickness;
float _saturationFactor;
float _brightnessFactor;
half4 _EdgeColor;
float _SpecPower;

half4 _BackLightColor;
float _BackLightOffset;
float backLightEdge;
half4 _BackLightColor2;
float _BackLightOffset2;
float backLightEdge2;
float _Emission;
half _AlphaClip;
CBUFFER_END

// NOTE: Do not ifdef the properties for dots instancing, but ifdef the actual usage.
// Otherwise you might break CPU-side as property constant-buffer offsets change per variant.
// NOTE: Dots instancing is orthogonal to the constant buffer above.
#ifdef UNITY_DOTS_INSTANCING_ENABLED

UNITY_DOTS_INSTANCING_START(MaterialPropertyMetadata)
    UNITY_DOTS_INSTANCED_PROP(float4, _BaseColor)
    UNITY_DOTS_INSTANCED_PROP(float4, _SpecColor)
    UNITY_DOTS_INSTANCED_PROP(float4, _EmissionColor)
    UNITY_DOTS_INSTANCED_PROP(float , _Cutoff)
    UNITY_DOTS_INSTANCED_PROP(float , _Smoothness)
    UNITY_DOTS_INSTANCED_PROP(float , _Metallic)
    UNITY_DOTS_INSTANCED_PROP(float , _BumpScale)
    UNITY_DOTS_INSTANCED_PROP(float , _Parallax)
    UNITY_DOTS_INSTANCED_PROP(float , _OcclusionStrength)
    UNITY_DOTS_INSTANCED_PROP(float , _ClearCoatMask)
    UNITY_DOTS_INSTANCED_PROP(float , _ClearCoatSmoothness)
    UNITY_DOTS_INSTANCED_PROP(float , _DetailAlbedoMapScale)
    UNITY_DOTS_INSTANCED_PROP(float , _DetailNormalMapScale)
    UNITY_DOTS_INSTANCED_PROP(float , _Surface)

    UNITY_DOTS_INSTANCED_PROP(half, _EdgeThickness)
    UNITY_DOTS_INSTANCED_PROP(float, _saturationFactor)
    UNITY_DOTS_INSTANCED_PROP(float, _brightnessFactor)
    UNITY_DOTS_INSTANCED_PROP(half4, _EdgeColor)
    UNITY_DOTS_INSTANCED_PROP(float, _SpecPower)
    UNITY_DOTS_INSTANCED_PROP(half4, _BackLightColor)
    UNITY_DOTS_INSTANCED_PROP(float, _BackLightOffset)
    UNITY_DOTS_INSTANCED_PROP(float, backLightEdge)
    UNITY_DOTS_INSTANCED_PROP(half4, _BackLightColor2)
    UNITY_DOTS_INSTANCED_PROP(float, _BackLightOffset2)
    UNITY_DOTS_INSTANCED_PROP(float, backLightEdge2)
 
    UNITY_DOTS_INSTANCED_PROP(float, _Emission)

    UNITY_DOTS_INSTANCED_PROP(half, _Brightness)
    UNITY_DOTS_INSTANCED_PROP(half, _Saturation)
    UNITY_DOTS_INSTANCED_PROP(half, _Contrast)
    
    UNITY_DOTS_INSTANCED_PROP(half, _AlphaClip)
    UNITY_DOTS_INSTANCED_PROP(half4, _SecondShadowMultColor)
    UNITY_DOTS_INSTANCED_PROP(half4, _EmissionColor)
    
      
 
UNITY_DOTS_INSTANCING_END(MaterialPropertyMetadata)

#define _BaseColor              UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float4 , _BaseColor)
#define _SpecColor              UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float4 , _SpecColor)
#define _EmissionColor          UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float4 , _EmissionColor)
#define _Cutoff                 UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _Cutoff)
#define _Smoothness             UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _Smoothness)
#define _Metallic               UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _Metallic)
#define _BumpScale              UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _BumpScale)
#define _Parallax               UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _Parallax)
#define _OcclusionStrength      UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _OcclusionStrength)
#define _ClearCoatMask          UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _ClearCoatMask)
#define _ClearCoatSmoothness    UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _ClearCoatSmoothness)
#define _DetailAlbedoMapScale   UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _DetailAlbedoMapScale)
#define _DetailNormalMapScale   UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _DetailNormalMapScale)
#define _Surface                UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _Surface)


#define _EdgeThickness                UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(half  , _EdgeThickness)
#define _saturationFactor                UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _saturationFactor)
#define _brightnessFactor                UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _brightnessFactor)
#define _EdgeColor                UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _EdgeColor)
#define _SpecPower                UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _SpecPower)
#define _BackLightColor                UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(half4  , _BackLightColor)
#define _BackLightOffset                UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _BackLightOffset)
#define backLightEdge                UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , backLightEdge)
#define _BackLightColor2                UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(half4  , _BackLightColor2)
#define _BackLightOffset2                UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _BackLightOffset2)
#define backLightEdge2                UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , backLightEdge2)
#define _Surface                UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _Surface)
 
#define _Emission                UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _Emission)

#define _Brightness                UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _Brightness)
#define _Saturation                UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _Saturation)
#define _Contrast                UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _Contrast)
#define _AlphaClip                UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _AlphaClip)
#define _SecondShadowMultColor                UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(half4  , _SecondShadowMultColor)
#define _EmissionColor                UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(half4  , _EmissionColor)
 
#endif

TEXTURE2D(_ParallaxMap);        SAMPLER(sampler_ParallaxMap);
TEXTURE2D(_OcclusionMap);       SAMPLER(sampler_OcclusionMap);
TEXTURE2D(_DetailMask);         SAMPLER(sampler_DetailMask);
TEXTURE2D(_DetailAlbedoMap);    SAMPLER(sampler_DetailAlbedoMap);
TEXTURE2D(_DetailNormalMap);    SAMPLER(sampler_DetailNormalMap);
TEXTURE2D(_MetallicGlossMap);   SAMPLER(sampler_MetallicGlossMap);
TEXTURE2D(_SpecGlossMap);       SAMPLER(sampler_SpecGlossMap);
TEXTURE2D(_ClearCoatMap);       SAMPLER(sampler_ClearCoatMap);
TEXTURE2D(_EmissionTex);       SAMPLER(sampler_EmissionTex);

TEXTURE2D(_NormalMap);          SAMPLER(sampler_NormalMap);

TEXTURE2D(_PbrMask); SAMPLER(sampler_PbrMask);
 

#ifdef _SPECULAR_SETUP
    #define SAMPLE_METALLICSPECULAR(uv) SAMPLE_TEXTURE2D(_SpecGlossMap, sampler_SpecGlossMap, uv)
#else
    #define SAMPLE_METALLICSPECULAR(uv) SAMPLE_TEXTURE2D(_MetallicGlossMap, sampler_MetallicGlossMap, uv)
#endif

half4 SampleMetallicSpecGloss(float2 uv, half albedoAlpha)
{
    half4 specGloss;

#ifdef _METALLICSPECGLOSSMAP
    specGloss = half4(SAMPLE_METALLICSPECULAR(uv));
    #ifdef _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
        specGloss.a = albedoAlpha * _Smoothness;
    #else
        specGloss.a *= _Smoothness;
    #endif
#else // _METALLICSPECGLOSSMAP
    #if _SPECULAR_SETUP
        specGloss.rgb = _SpecColor.rgb;
    #else
        specGloss.rgb = _Metallic.rrr;
    #endif

    #ifdef _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
        specGloss.a = albedoAlpha * _Smoothness;
    #else
        specGloss.a = _Smoothness;
    #endif
#endif

    return specGloss;
}

half SampleOcclusion(float2 uv)
{
    #ifdef _OCCLUSIONMAP
        half occ = SAMPLE_TEXTURE2D(_OcclusionMap, sampler_OcclusionMap, uv).g;
        return LerpWhiteTo(occ, _OcclusionStrength);
    #else
        return half(1.0);
    #endif
}


// Returns clear coat parameters
// .x/.r == mask
// .y/.g == smoothness
half2 SampleClearCoat(float2 uv)
{
#if defined(_CLEARCOAT) || defined(_CLEARCOATMAP)
    half2 clearCoatMaskSmoothness = half2(_ClearCoatMask, _ClearCoatSmoothness);

#if defined(_CLEARCOATMAP)
    clearCoatMaskSmoothness *= SAMPLE_TEXTURE2D(_ClearCoatMap, sampler_ClearCoatMap, uv).rg;
#endif

    return clearCoatMaskSmoothness;
#else
    return half2(0.0, 1.0);
#endif  // _CLEARCOAT
}

void ApplyPerPixelDisplacement(half3 viewDirTS, inout float2 uv)
{
#if defined(_PARALLAXMAP)
    uv += ParallaxMapping(TEXTURE2D_ARGS(_ParallaxMap, sampler_ParallaxMap), viewDirTS, _Parallax, uv);
#endif
}

// Used for scaling detail albedo. Main features:
// - Depending if detailAlbedo brightens or darkens, scale magnifies effect.
// - No effect is applied if detailAlbedo is 0.5.
half3 ScaleDetailAlbedo(half3 detailAlbedo, half scale)
{
    // detailAlbedo = detailAlbedo * 2.0h - 1.0h;
    // detailAlbedo *= _DetailAlbedoMapScale;
    // detailAlbedo = detailAlbedo * 0.5h + 0.5h;
    // return detailAlbedo * 2.0f;

    // A bit more optimized
    return half(2.0) * detailAlbedo * scale - scale + half(1.0);
}

half3 ApplyDetailAlbedo(float2 detailUv, half3 albedo, half detailMask)
{
#if defined(_DETAIL)
    half3 detailAlbedo = SAMPLE_TEXTURE2D(_DetailAlbedoMap, sampler_DetailAlbedoMap, detailUv).rgb;

    // In order to have same performance as builtin, we do scaling only if scale is not 1.0 (Scaled version has 6 additional instructions)
#if defined(_DETAIL_SCALED)
    detailAlbedo = ScaleDetailAlbedo(detailAlbedo, _DetailAlbedoMapScale);
#else
    detailAlbedo = half(2.0) * detailAlbedo;
#endif

    return albedo * LerpWhiteTo(detailAlbedo, detailMask);
#else
    return albedo;
#endif
}

half3 ApplyDetailNormal(float2 detailUv, half3 normalTS, half detailMask)
{
#if defined(_DETAIL)
#if BUMP_SCALE_NOT_SUPPORTED
    half3 detailNormalTS = UnpackNormal(SAMPLE_TEXTURE2D(_DetailNormalMap, sampler_DetailNormalMap, detailUv));
#else
    half3 detailNormalTS = UnpackNormalScale(SAMPLE_TEXTURE2D(_DetailNormalMap, sampler_DetailNormalMap, detailUv), _DetailNormalMapScale);
#endif

    // With UNITY_NO_DXT5nm unpacked vector is not normalized for BlendNormalRNM
    // For visual consistancy we going to do in all cases
    detailNormalTS = normalize(detailNormalTS);

    return lerp(normalTS, BlendNormalRNM(normalTS, detailNormalTS), detailMask); // todo: detailMask should lerp the angle of the quaternion rotation, not the normals
#else
    return normalTS;
#endif
}

/*inline void InitializeStandardLitSurfaceData(float2 uv, out SurfaceData outSurfaceData)
{
    half4 albedoAlpha = SampleAlbedoAlpha(uv, TEXTURE2D_ARGS(_BaseMap, sampler_BaseMap));
    half4 mark = SampleAlbedoAlpha(uv, TEXTURE2D_ARGS(_PbrMask, sampler_PbrMask));

    outSurfaceData.alpha = albedoAlpha.a;


    outSurfaceData.albedo = albedoAlpha.rgb;
    outSurfaceData.smoothness = _Smoothness * mark.g;
    outSurfaceData.metallic = _Metallic * mark.r;
    outSurfaceData.specular = half3(0.0, 0.0, 0.0);

    outSurfaceData.smoothness = _Smoothness;
    outSurfaceData.normalTS = half3(0, 0, 1);
    outSurfaceData.occlusion = mark.g;
    outSurfaceData.emission = 0.0;

    outSurfaceData.clearCoatMask = half(0.0);
    outSurfaceData.clearCoatSmoothness = half(0.0);
 

 
}*/


inline void InitializeStandardLitSurfaceData(float2 uv, out SurfaceData outSurfaceData)
{
    half4 albedoAlpha = SampleAlbedoAlpha(uv, TEXTURE2D_ARGS(_BaseMap, sampler_BaseMap)) * _BaseColor;

   

 
    //���Ͷ�
    half luminance = 0.2125 * albedoAlpha.r + 0.7154 * albedoAlpha.g + 0.0721 * albedoAlpha.b;  //��������ص�����ֵ
    half3 luminanceColor = half3(luminance, luminance, luminance);  //�������Ͷ�Ϊ0����ɫ
    half3 finalColor = lerp(luminanceColor, albedoAlpha.rgb, _Saturation);
 
    //contrast
    half3 avgColor = half3(0.5, 0.5, 0.5);
    finalColor = lerp(avgColor, finalColor, _Contrast);


    
    //����
    finalColor =  finalColor.rgb * _Brightness;

 

    half4 mark = SampleAlbedoAlpha(uv, TEXTURE2D_ARGS(_PbrMask, sampler_PbrMask));
    outSurfaceData.alpha = albedoAlpha.a;

    half4 specGloss = SampleMetallicSpecGloss(uv, albedoAlpha.a);
   

    outSurfaceData.albedo = finalColor.rgb;
    outSurfaceData.smoothness = _Smoothness;
    outSurfaceData.metallic = _Metallic * mark.r;
    outSurfaceData.specular = half3(0.0, 0.0, 0.0);

    outSurfaceData.smoothness = _Smoothness;
    outSurfaceData.normalTS = half3(0, 0, 1);
    outSurfaceData.occlusion = mark.g;

     half4 emissionTex = SampleAlbedoAlpha(uv, TEXTURE2D_ARGS(_EmissionTex, sampler_EmissionTex));
    outSurfaceData.emission = albedoAlpha.rgb * mark.a* _Emission * _EmissionColor.rgb*emissionTex.rgb;

 
    outSurfaceData.clearCoatMask = half(0.0);
    outSurfaceData.clearCoatSmoothness = half(0.0);
 

 
}
#endif // UNIVERSAL_INPUT_SURFACE_PBR_INCLUDED
