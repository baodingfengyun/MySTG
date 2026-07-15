#ifndef UNIVERSAL_CARTOON_INPUT_INCLUDED
#define UNIVERSAL_CARTOOT_INPUT_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/CommonMaterial.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/SurfaceInput.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/ParallaxMapping.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DBuffer.hlsl"

 

// NOTE: Do not ifdef the properties here as SRP batcher can not handle different layouts.
CBUFFER_START(UnityPerMaterial)
float4 _BaseMap_ST;
float _Tiling;
float _Offset;
float _NormalScale;
float _Mettallic;
float _Smoothness;
			
float4 _LightAreaMultColor;
 

float _ShadowFeather;
float _ShadowRange;
float4 _FnlColor;
float _Fnl;



 
half4 _BaseColor;
half _Cutoff;
half _EdgeThickness;
float _saturationFactor;
float _brightnessFactor;
half4 _EdgeColor;
float _SpecPower;
/*half4 _SpecColor;
half4 _EmissionColor;

half _Smoothness;
half _Metallic;
half _BumpScale;
half _Parallax;
half _OcclusionStrength;
half _ClearCoatMask;
half _ClearCoatSmoothness;
half _DetailAlbedoMapScale;
half _DetailNormalMapScale;*/

half4 _BackLightColor;
float _BackLightOffset;
float backLightEdge;


half4 _BackLightColor2;
float _BackLightOffset2;
float backLightEdge2;
CBUFFER_END

 

//TEXTURE2D(_BaseMap);
TEXTURE2D(_NormalMap);
TEXTURE2D(_PbrMask);
SAMPLER(sampler_MainTex);
SAMPLER(sampler_PbrMask);
SAMPLER(sampler_NormalMap);

 
 

inline void InitializeStandardLitSurfaceData(float2 uv, out SurfaceData outSurfaceData)
{
    half4 albedoAlpha = SampleAlbedoAlpha(uv, TEXTURE2D_ARGS(_BaseMap, sampler_BaseMap));
    half4 mark = SampleAlbedoAlpha(uv, TEXTURE2D_ARGS(_PbrMask, sampler_PbrMask));
    
    outSurfaceData.alpha = albedoAlpha.a;

 
    outSurfaceData.albedo = albedoAlpha.rgb ;
    outSurfaceData.smoothness = _Smoothness*mark.g;
    outSurfaceData.metallic = _Mettallic*mark.r;
    outSurfaceData.specular = half3(0.0, 0.0, 0.0);

    outSurfaceData.smoothness = _Smoothness   ;
    outSurfaceData.normalTS = half3(0,0,1);
    outSurfaceData.occlusion = mark.g;
    outSurfaceData.emission = 0.0;

    outSurfaceData.clearCoatMask       = half(0.0);
    outSurfaceData.clearCoatSmoothness = half(0.0);

 
}

#endif // UNIVERSAL_INPUT_SURFACE_PBR_INCLUDED
