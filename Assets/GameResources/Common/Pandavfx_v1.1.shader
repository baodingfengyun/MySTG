// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "VFX/Pandavfx_v1.1"
{
	Properties
	{
		[HideInInspector] _EmissionColor("Emission Color", Color) = (1,1,1,1)
		[HideInInspector] _AlphaCutoff("Alpha Cutoff ", Range(0, 1)) = 0.5
		[Enum(UnityEngine.Rendering.CullMode)]_Cullmode("Cullmode", Float) = 0
		[Enum(UnityEngine.Rendering.CompareFunction)]_Ztest("Ztest", Float) = 4
		[Enum(UnityEngine.Rendering.BlendMode)]_Scr("Scr", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)]_Dst("Dst", Float) = 10
		_MainTex("MainTex", 2D) = "white" {}
		_MainAlpha("MainAlpha", Range( 0 , 100)) = 1
		[HDR]_MainColor("MainColor", Color) = (1,1,1,1)
		_MainTex_Uspeed("MainTex_Uspeed", Float) = 0
		_MainTex_Vspeed("MainTex_Vspeed", Float) = 0
		_MaskTex("MaskTex", 2D) = "white" {}
		_DistortTex("DistortTex", 2D) = "white" {}
		[Enum(off,0,on,1)]_DistortMainTex("DistortMainTex", Float) = 1
		[Enum(off,0,on,1)]_DistortMask("DistortMask", Float) = 0
		[Enum(off,0,on,1)]_DistortDisTex("DistortDisTex", Float) = 0
		_DistortFactor("DistortFactor", Range( 0 , 1)) = 0
		_DistortTex_Uspeed("DistortTex_Uspeed", Float) = 0
		_DistortTex_Vspeed("DistortTex_Vspeed", Float) = 0
		_DissloveTex("DissloveTex", 2D) = "white" {}
		_DIssloveFactor("DIssloveFactor", Range( 0 , 2)) = 0.5077091
		_DIssloveWide("DIssloveWide", Range( 0 , 1)) = 0.02352943
		_DIssloveSoft("DIssloveSoft", Range( 0 , 1)) = 0.8235294
		[HDR]_DIssloveColor("DIssloveColor", Color) = (1,1,1,1)
		_DisTex_Uspeed("DisTex_Uspeed", Float) = 0
		_DisTex_Vspeed("DisTex_Vspeed", Float) = 0
		_VTOTex("VTOTex", 2D) = "white" {}
		_VTOFactor("VTOFactor", Float) = 0
		_VTOTex_Uspeed("VTOTex_Uspeed", Float) = 0
		_VTOTex_Vspeed("VTOTex_Vspeed", Float) = 0
		_VTOMaskTex("VTOMaskTex", 2D) = "white" {}
		_fnl_power("fnl_power", Range( 1 , 10)) = 1
		_fnl_sacle("fnl_sacle", Range( 0 , 1)) = 0
		[HDR]_fnl_color("fnl_color", Color) = (1,1,1,0)
		_softFacotr("softFacotr", Range( 0 , 20)) = 1
		_DepthfadeFactor("DepthfadeFactor", Float) = 1
		[Toggle]_MainTex_ar("MainTex_a/r", Float) = 0
		[Toggle]_CustomdataMainTexUV("CustomdataMainTexUV", Float) = 0
		_MainAlphaPower("MainAlphaPower", Range( 1 , 10)) = 1
		[Toggle]_MaskAlphaRA("MaskAlphaRA", Float) = 0
		[Toggle]_CustomdataMaskUV("CustomdataMaskUV", Float) = 0
		_Mask_scale("Mask_scale", Float) = 1
		[Toggle]_AlphaAdd("AlphaAdd", Float) = 0
		_Mask_rotat("Mask_rotat", Range( 0 , 360)) = 0
		_MainTex_rotat("MainTex_rotat", Range( 0 , 360)) = 0
		_VTOR("VTOR", Range( 0 , 360)) = 0
		_VTOMaskR("VTOMaskR", Range( 0 , 360)) = 0
		_ScreenR("ScreenR", Range( 0 , 360)) = 0
		_ScreenMaskR("ScreenMaskR", Range( 0 , 360)) = 0
		_DIssolve_rotat("DIssolve_rotat", Range( 0 , 360)) = 0
		[Toggle]_CustomdataDis("CustomdataDis", Float) = 0
		[Toggle]_FNLfanxiangkaiguan("FNLfanxiangkaiguan", Float) = 0
		[Toggle]_FNLkaiguan("FNLkaiguan", Float) = 0
		[Toggle]_ToggleSwitch0("Toggle Switch0", Float) = 0
		[Toggle]_Depthfadeon("Depthfadeon", Float) = 0
		_ScreenDistortMask("ScreenDistortMask", 2D) = "white" {}
		_ScreenDistortTex("ScreenDistortTex", 2D) = "white" {}
		_ScreenDistortTexScale("ScreenDistortTexScale", Range( 0 , 1)) = 0
		[Toggle]_CustomdataScreenUV("CustomdataScreenUV", Float) = 0
		[Toggle]_screenalphaon("screenalphaon", Float) = 0
		[Toggle]_Screencoloron("Screencoloron", Float) = 0
		[Toggle]_screenVTOon("screenVTOon", Float) = 0
		_ScreenU("ScreenU", Float) = 0
		_ScreenV("ScreenV", Float) = 0
		_Mask_Uspeed("Mask_Uspeed", Float) = 0
		_Mask_Vspeed("Mask_Vspeed", Float) = 0
		[Toggle]_soft_sting("soft_sting", Float) = 0
		[Toggle]_sot_sting_A("sot_sting_A", Float) = 0
		[Toggle]_MaintexCV("MaintexCV", Float) = 0
		[Toggle]_MaintexC("MaintexC", Float) = 0
		[Toggle]_MaskCV("MaskCV", Float) = 0
		[Toggle]_MaskC("MaskC", Float) = 0
		[Toggle]_DissolveCV("DissolveCV", Float) = 0
		[Toggle]_DissolveC("DissolveC", Float) = 0
		[Toggle]_DissolveAR("DissolveAR", Float) = 1
		[Toggle]_VTOAR("VTOAR", Float) = 1
		[Toggle]_VTOMaskAR("VTOMaskAR", Float) = 1
		[Toggle]_VTOMaskCV("VTOMaskCV", Float) = 0
		[Toggle]_VTOMaskC("VTOMaskC", Float) = 0
		[Toggle]_ScreenMaskCV("ScreenMaskCV", Float) = 0
		[Toggle]_ScreenMaskC("ScreenMaskC", Float) = 0
		[Toggle]_ScreenMaskAR("ScreenMaskAR", Float) = 1
		[Toggle]_ScreenAR("ScreenAR", Float) = 1
		[Toggle]_VTOC("VTOC", Float) = 0
		[Toggle]_VTOCV("VTOCV", Float) = 0
		[Toggle]_ScreenCV("ScreenCV", Float) = 0
		[Toggle]_ScreenC("ScreenC", Float) = 0
		_qubaohedu("qubaohedu", Range( 0 , 1)) = 0
		[HDR]_DepthColor("DepthColor", Color) = (0,0,0,0)
		[Enum(Option1,0,Option2,1)]_DepthF("DepthF", Float) = 0
		_Zwrite("Zwrite", Float) = 0
		_Dir("Dir", Vector) = (0,0,0,0)
		_AddTex("AddTex", 2D) = "white" {}
		_AddTexLerp("AddTexLerp", Range( 0 , 1)) = 0
		[HDR]_BackFaceColor("BackFaceColor", Color) = (1,1,1,0)
		[Toggle]_IfMaskColor("IfMaskColor", Float) = 0
		[ASEEnd][Enum(Option1,0,Option2,1)]_CustomDistort("CustomDistort", Float) = 0

		//_TessPhongStrength( "Tess Phong Strength", Range( 0, 1 ) ) = 0.5
		//_TessValue( "Tess Max Tessellation", Range( 1, 32 ) ) = 16
		//_TessMin( "Tess Min Distance", Float ) = 10
		//_TessMax( "Tess Max Distance", Float ) = 25
		//_TessEdgeLength ( "Tess Edge length", Range( 2, 50 ) ) = 16
		//_TessMaxDisp( "Tess Max Displacement", Float ) = 25
	}

	SubShader
	{
		LOD 0

		
		Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Transparent" "Queue"="Transparent" }
		
		Cull [_Cullmode]
		AlphaToMask Off
		HLSLINCLUDE
		#pragma target 5.0

		#pragma prefer_hlslcc gles
		#pragma exclude_renderers d3d11_9x 

		#ifndef ASE_TESS_FUNCS
		#define ASE_TESS_FUNCS
		float4 FixedTess( float tessValue )
		{
			return tessValue;
		}
		
		float CalcDistanceTessFactor (float4 vertex, float minDist, float maxDist, float tess, float4x4 o2w, float3 cameraPos )
		{
			float3 wpos = mul(o2w,vertex).xyz;
			float dist = distance (wpos, cameraPos);
			float f = clamp(1.0 - (dist - minDist) / (maxDist - minDist), 0.01, 1.0) * tess;
			return f;
		}

		float4 CalcTriEdgeTessFactors (float3 triVertexFactors)
		{
			float4 tess;
			tess.x = 0.5 * (triVertexFactors.y + triVertexFactors.z);
			tess.y = 0.5 * (triVertexFactors.x + triVertexFactors.z);
			tess.z = 0.5 * (triVertexFactors.x + triVertexFactors.y);
			tess.w = (triVertexFactors.x + triVertexFactors.y + triVertexFactors.z) / 3.0f;
			return tess;
		}

		float CalcEdgeTessFactor (float3 wpos0, float3 wpos1, float edgeLen, float3 cameraPos, float4 scParams )
		{
			float dist = distance (0.5 * (wpos0+wpos1), cameraPos);
			float len = distance(wpos0, wpos1);
			float f = max(len * scParams.y / (edgeLen * dist), 1.0);
			return f;
		}

		float DistanceFromPlane (float3 pos, float4 plane)
		{
			float d = dot (float4(pos,1.0f), plane);
			return d;
		}

		bool WorldViewFrustumCull (float3 wpos0, float3 wpos1, float3 wpos2, float cullEps, float4 planes[6] )
		{
			float4 planeTest;
			planeTest.x = (( DistanceFromPlane(wpos0, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[0]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.y = (( DistanceFromPlane(wpos0, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[1]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.z = (( DistanceFromPlane(wpos0, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[2]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.w = (( DistanceFromPlane(wpos0, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[3]) > -cullEps) ? 1.0f : 0.0f );
			return !all (planeTest);
		}

		float4 DistanceBasedTess( float4 v0, float4 v1, float4 v2, float tess, float minDist, float maxDist, float4x4 o2w, float3 cameraPos )
		{
			float3 f;
			f.x = CalcDistanceTessFactor (v0,minDist,maxDist,tess,o2w,cameraPos);
			f.y = CalcDistanceTessFactor (v1,minDist,maxDist,tess,o2w,cameraPos);
			f.z = CalcDistanceTessFactor (v2,minDist,maxDist,tess,o2w,cameraPos);

			return CalcTriEdgeTessFactors (f);
		}

		float4 EdgeLengthBasedTess( float4 v0, float4 v1, float4 v2, float edgeLength, float4x4 o2w, float3 cameraPos, float4 scParams )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;
			tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
			tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
			tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
			tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			return tess;
		}

		float4 EdgeLengthBasedTessCull( float4 v0, float4 v1, float4 v2, float edgeLength, float maxDisplacement, float4x4 o2w, float3 cameraPos, float4 scParams, float4 planes[6] )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;

			if (WorldViewFrustumCull(pos0, pos1, pos2, maxDisplacement, planes))
			{
				tess = 0.0f;
			}
			else
			{
				tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
				tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
				tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
				tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			}
			return tess;
		}
		#endif //ASE_TESS_FUNCS
		ENDHLSL

		
		Pass
		{
			
			Name "Forward"
			Tags { "LightMode"="UniversalForward" }
			
			Blend [_Scr] [_Dst]
			ZWrite [_Zwrite]
			ZTest [_Ztest]
			Offset 0,0
			ColorMask RGBA
			

			HLSLPROGRAM
			
			#define _RECEIVE_SHADOWS_OFF 1
			#define ASE_SRP_VERSION 70401
			#define REQUIRE_DEPTH_TEXTURE 1
			#define REQUIRE_OPAQUE_TEXTURE 1

			
			#pragma vertex vert
			#pragma fragment frag

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

			#if ASE_SRP_VERSION <= 70108
			#define REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR
			#endif

			#define ASE_NEEDS_VERT_NORMAL
			#define ASE_NEEDS_FRAG_WORLD_POSITION
			#define ASE_NEEDS_VERT_POSITION
			#define ASE_NEEDS_FRAG_COLOR


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 worldPos : TEXCOORD0;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 shadowCoord : TEXCOORD1;
				#endif
				#ifdef ASE_FOG
				float fogFactor : TEXCOORD2;
				#endif
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_texcoord4 : TEXCOORD4;
				float4 ase_color : COLOR;
				float4 ase_texcoord5 : TEXCOORD5;
				float4 ase_texcoord6 : TEXCOORD6;
				float4 ase_texcoord7 : TEXCOORD7;
				float4 ase_texcoord8 : TEXCOORD8;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _DepthColor;
			float4 _MaskTex_ST;
			float4 _fnl_color;
			float4 _VTOTex_ST;
			float4 _ScreenDistortTex_ST;
			float4 _DIssloveColor;
			float4 _DissloveTex_ST;
			float4 _MainTex_ST;
			float4 _ScreenDistortMask_ST;
			float4 _MainColor;
			float4 _BackFaceColor;
			float4 _DistortTex_ST;
			float4 _VTOMaskTex_ST;
			float3 _Dir;
			float _MainAlphaPower;
			float _DepthF;
			float _DepthfadeFactor;
			float _Depthfadeon;
			float _DissolveCV;
			float _FNLfanxiangkaiguan;
			float _DIssloveWide;
			float _DIssolve_rotat;
			float _DistortDisTex;
			float _DisTex_Vspeed;
			float _DisTex_Uspeed;
			float _DissolveC;
			float _softFacotr;
			float _Ztest;
			float _fnl_sacle;
			float _FNLkaiguan;
			float _ScreenMaskCV;
			float _ScreenMaskR;
			float _ScreenMaskC;
			float _ScreenMaskAR;
			float _ScreenCV;
			float _ScreenR;
			float _ScreenV;
			float _ScreenU;
			float _ScreenC;
			float _ScreenAR;
			float _ScreenDistortTexScale;
			float _CustomdataScreenUV;
			float _IfMaskColor;
			float _AddTexLerp;
			float _soft_sting;
			float _fnl_power;
			float _DissolveAR;
			float _Screencoloron;
			float _DIssloveSoft;
			float _MainAlpha;
			float _CustomdataDis;
			float _AlphaAdd;
			float _VTOMaskCV;
			float _VTOMaskR;
			float _VTOMaskC;
			float _VTOMaskAR;
			float _VTOFactor;
			float _ToggleSwitch0;
			float _VTOCV;
			float _Mask_scale;
			float _VTOR;
			float _VTOTex_Uspeed;
			float _VTOC;
			float _VTOAR;
			float _screenVTOon;
			float _Scr;
			float _Cullmode;
			float _Dst;
			float _Zwrite;
			float _VTOTex_Vspeed;
			float _MaskAlphaRA;
			float _MaskC;
			float _Mask_Uspeed;
			float _sot_sting_A;
			float _qubaohedu;
			float _MaintexCV;
			float _MainTex_rotat;
			float _DistortMainTex;
			float _CustomdataMainTexUV;
			float _MainTex_Vspeed;
			float _MainTex_Uspeed;
			float _MaintexC;
			float _MainTex_ar;
			float _MaskCV;
			float _Mask_rotat;
			float _CustomdataMaskUV;
			float _DistortMask;
			float _DistortTex_Vspeed;
			float _DistortTex_Uspeed;
			float _CustomDistort;
			float _DistortFactor;
			float _Mask_Vspeed;
			float _DIssloveFactor;
			float _screenalphaon;
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END
			sampler2D _VTOTex;
			sampler2D _VTOMaskTex;
			sampler2D _MaskTex;
			sampler2D _DistortTex;
			sampler2D _MainTex;
			sampler2D _DissloveTex;
			uniform float4 _CameraDepthTexture_TexelSize;
			sampler2D _AddTex;
			sampler2D _ScreenDistortTex;
			sampler2D _ScreenDistortMask;


			inline float4 ASE_ComputeGrabScreenPos( float4 pos )
			{
				#if UNITY_UV_STARTS_AT_TOP
				float scale = -1.0;
				#else
				float scale = 1.0;
				#endif
				float4 o = pos;
				o.y = pos.w * 0.5f;
				o.y = ( pos.y - o.y ) * _ProjectionParams.x * scale + o.y;
				return o;
			}
			
			
			VertexOutput VertexFunction ( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				float2 appendResult76 = (float2(_VTOTex_Uspeed , _VTOTex_Vspeed));
				float2 uv_VTOTex = v.ase_texcoord.xy * _VTOTex_ST.xy + _VTOTex_ST.zw;
				float2 panner77 = ( 1.0 * _Time.y * appendResult76 + uv_VTOTex);
				float cos287 = cos( ( ( ( _VTOR / 360.0 ) * PI ) * 2.0 ) );
				float sin287 = sin( ( ( ( _VTOR / 360.0 ) * PI ) * 2.0 ) );
				float2 rotator287 = mul( panner77 - float2( 0.5,0.5 ) , float2x2( cos287 , -sin287 , sin287 , cos287 )) + float2( 0.5,0.5 );
				float2 break367 = rotator287;
				float2 break366 = rotator287;
				float clampResult278 = clamp( break366.x , 0.0 , 1.0 );
				float clampResult368 = clamp( break366.y , 0.0 , 1.0 );
				float2 appendResult370 = (float2((( _VTOC )?( clampResult278 ):( break367.x )) , (( _VTOCV )?( clampResult368 ):( break367.y ))));
				float4 tex2DNode72 = tex2Dlod( _VTOTex, float4( appendResult370, 0, 0.0) );
				float4 texCoord85 = v.ase_texcoord1;
				texCoord85.xy = v.ase_texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
				float2 uv_VTOMaskTex = v.ase_texcoord.xy * _VTOMaskTex_ST.xy + _VTOMaskTex_ST.zw;
				float cos263 = cos( ( ( ( _VTOMaskR / 360.0 ) * PI ) * 2.0 ) );
				float sin263 = sin( ( ( ( _VTOMaskR / 360.0 ) * PI ) * 2.0 ) );
				float2 rotator263 = mul( uv_VTOMaskTex - float2( 0.5,0.5 ) , float2x2( cos263 , -sin263 , sin263 , cos263 )) + float2( 0.5,0.5 );
				float2 break372 = rotator263;
				float2 break371 = rotator263;
				float clampResult257 = clamp( break371.x , 0.0 , 1.0 );
				float clampResult373 = clamp( break371.y , 0.0 , 1.0 );
				float2 appendResult375 = (float2((( _VTOMaskC )?( clampResult257 ):( break372.x )) , (( _VTOMaskCV )?( clampResult373 ):( break372.y ))));
				float4 tex2DNode81 = tex2Dlod( _VTOMaskTex, float4( appendResult375, 0, 0.0) );
				float3 VTO82 = ( (( _VTOAR )?( tex2DNode72.r ):( tex2DNode72.a )) * v.ase_normal * (( _ToggleSwitch0 )?( texCoord85.w ):( _VTOFactor )) * (( _VTOMaskAR )?( tex2DNode81.r ):( tex2DNode81.a )) );
				float3 temp_cast_0 = (0.0).xxx;
				
				float3 ase_worldNormal = TransformObjectToWorldNormal(v.ase_normal);
				o.ase_texcoord6.xyz = ase_worldNormal;
				float3 vertexPos88 = v.vertex.xyz;
				float4 ase_clipPos88 = TransformObjectToHClip((vertexPos88).xyz);
				float4 screenPos88 = ComputeScreenPos(ase_clipPos88);
				o.ase_texcoord7 = screenPos88;
				float4 ase_clipPos = TransformObjectToHClip((v.vertex).xyz);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord8 = screenPos;
				
				o.ase_texcoord3.xy = v.ase_texcoord.xy;
				o.ase_texcoord4 = v.ase_texcoord2;
				o.ase_color = v.ase_color;
				o.ase_texcoord5 = v.ase_texcoord1;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord3.zw = 0;
				o.ase_texcoord6.w = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = (( _screenVTOon )?( temp_cast_0 ):( VTO82 ));
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif
				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );
				float4 positionCS = TransformWorldToHClip( positionWS );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				o.worldPos = positionWS;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				VertexPositionInputs vertexInput = (VertexPositionInputs)0;
				vertexInput.positionWS = positionWS;
				vertexInput.positionCS = positionCS;
				o.shadowCoord = GetShadowCoord( vertexInput );
				#endif
				#ifdef ASE_FOG
				o.fogFactor = ComputeFogFactor( positionCS.z );
				#endif
				o.clipPos = positionCS;
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_color : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_texcoord1 = v.ase_texcoord1;
				o.ase_texcoord2 = v.ase_texcoord2;
				o.ase_color = v.ase_color;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_texcoord1 = patch[0].ase_texcoord1 * bary.x + patch[1].ase_texcoord1 * bary.y + patch[2].ase_texcoord1 * bary.z;
				o.ase_texcoord2 = patch[0].ase_texcoord2 * bary.x + patch[1].ase_texcoord2 * bary.y + patch[2].ase_texcoord2 * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			half4 frag ( VertexOutput IN , FRONT_FACE_TYPE ase_vface : FRONT_FACE_SEMANTIC ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 WorldPosition = IN.worldPos;
				#endif
				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = IN.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif
				float2 appendResult222 = (float2(_Mask_Uspeed , _Mask_Vspeed));
				float2 uv_MaskTex = IN.ase_texcoord3.xy * _MaskTex_ST.xy + _MaskTex_ST.zw;
				float2 temp_cast_0 = (0.0).xx;
				float4 texCoord451 = IN.ase_texcoord4;
				texCoord451.xy = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float lerpResult450 = lerp( _DistortFactor , texCoord451.w , _CustomDistort);
				float2 appendResult58 = (float2(_DistortTex_Uspeed , _DistortTex_Vspeed));
				float2 uv_DistortTex = IN.ase_texcoord3.xy * _DistortTex_ST.xy + _DistortTex_ST.zw;
				float2 panner59 = ( 1.0 * _Time.y * appendResult58 + uv_DistortTex);
				float4 tex2DNode54 = tex2D( _DistortTex, panner59 );
				float2 appendResult61 = (float2(tex2DNode54.r , tex2DNode54.g));
				float2 DistortUV60 = ( lerpResult450 * appendResult61 );
				float2 lerpResult447 = lerp( temp_cast_0 , DistortUV60 , _DistortMask);
				float2 panner219 = ( 1.0 * _Time.y * appendResult222 + ( uv_MaskTex + lerpResult447 ));
				float2 temp_cast_1 = (0.0).xx;
				float2 texCoord134 = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float cos149 = cos( ( ( ( _Mask_rotat / 360.0 ) * PI ) * 2.0 ) );
				float sin149 = sin( ( ( ( _Mask_rotat / 360.0 ) * PI ) * 2.0 ) );
				float2 rotator149 = mul( ( panner219 + (( _CustomdataMaskUV )?( texCoord134 ):( temp_cast_1 )) ) - float2( 0.5,0.5 ) , float2x2( cos149 , -sin149 , sin149 , cos149 )) + float2( 0.5,0.5 );
				float2 break362 = rotator149;
				float2 break361 = rotator149;
				float clampResult247 = clamp( break361.x , 0.0 , 1.0 );
				float clampResult363 = clamp( break361.y , 0.0 , 1.0 );
				float2 appendResult365 = (float2((( _MaskC )?( clampResult247 ):( break362.x )) , (( _MaskCV )?( clampResult363 ):( break362.y ))));
				float4 tex2DNode52 = tex2D( _MaskTex, appendResult365 );
				float MaskAlpha136 = ( _Mask_scale * (( _MaskAlphaRA )?( tex2DNode52.r ):( tex2DNode52.a )) );
				float2 appendResult14 = (float2(_MainTex_Uspeed , _MainTex_Vspeed));
				float2 temp_cast_2 = (0.0).xx;
				float2 texCoord16 = IN.ase_texcoord5.xy * float2( 1,1 ) + float2( 0,0 );
				float2 uv_MainTex = IN.ase_texcoord3.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float2 temp_cast_3 = (0.0).xx;
				float2 lerpResult118 = lerp( temp_cast_3 , DistortUV60 , _DistortMainTex);
				float2 panner11 = ( 1.0 * _Time.y * appendResult14 + ( (( _CustomdataMainTexUV )?( texCoord16 ):( temp_cast_2 )) + uv_MainTex + lerpResult118 ));
				float cos158 = cos( ( ( ( _MainTex_rotat / 360.0 ) * PI ) * 2.0 ) );
				float sin158 = sin( ( ( ( _MainTex_rotat / 360.0 ) * PI ) * 2.0 ) );
				float2 rotator158 = mul( panner11 - float2( 0.5,0.5 ) , float2x2( cos158 , -sin158 , sin158 , cos158 )) + float2( 0.5,0.5 );
				float2 break351 = rotator158;
				float2 break352 = rotator158;
				float clampResult245 = clamp( break352.x , 0.0 , 1.0 );
				float clampResult353 = clamp( break352.y , 0.0 , 1.0 );
				float2 appendResult355 = (float2((( _MaintexC )?( clampResult245 ):( break351.x )) , (( _MaintexCV )?( clampResult353 ):( break351.y ))));
				float4 tex2DNode1 = tex2D( _MainTex, appendResult355 );
				float MainTexAlpha37 = ( IN.ase_color.a * (( _MainTex_ar )?( tex2DNode1.r ):( tex2DNode1.a )) * _MainColor.a * _MainAlpha );
				float4 texCoord51 = IN.ase_texcoord5;
				texCoord51.xy = IN.ase_texcoord5.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult48 = (float2(_DisTex_Uspeed , _DisTex_Vspeed));
				float2 uv_DissloveTex = IN.ase_texcoord3.xy * _DissloveTex_ST.xy + _DissloveTex_ST.zw;
				float2 temp_cast_4 = (0.0).xx;
				float2 lerpResult122 = lerp( temp_cast_4 , DistortUV60 , _DistortDisTex);
				float2 panner49 = ( 1.0 * _Time.y * appendResult48 + ( uv_DissloveTex + lerpResult122 ));
				float cos162 = cos( ( ( ( _DIssolve_rotat / 360.0 ) * PI ) * 2.0 ) );
				float sin162 = sin( ( ( ( _DIssolve_rotat / 360.0 ) * PI ) * 2.0 ) );
				float2 rotator162 = mul( panner49 - float2( 0.5,0.5 ) , float2x2( cos162 , -sin162 , sin162 , cos162 )) + float2( 0.5,0.5 );
				float2 break357 = rotator162;
				float2 break356 = rotator162;
				float clampResult250 = clamp( break356.x , 0.0 , 1.0 );
				float clampResult358 = clamp( break356.y , 0.0 , 1.0 );
				float2 appendResult360 = (float2((( _DissolveC )?( clampResult250 ):( break357.x )) , (( _DissolveCV )?( clampResult358 ):( break357.y ))));
				float4 tex2DNode25 = tex2D( _DissloveTex, appendResult360 );
				float smoothstepResult27 = smoothstep( ( (( _CustomdataDis )?( texCoord51.z ):( _DIssloveFactor )) - _DIssloveSoft ) , (( _CustomdataDis )?( texCoord51.z ):( _DIssloveFactor )) , (( _DissolveAR )?( tex2DNode25.r ):( tex2DNode25.a )));
				float temp_output_233_0 = step( ( (( _CustomdataDis )?( texCoord51.z ):( _DIssloveFactor )) - _DIssloveWide ) , (( _DissolveAR )?( tex2DNode25.r ):( tex2DNode25.a )) );
				float DisAplha42 = (( _sot_sting_A )?( temp_output_233_0 ):( smoothstepResult27 ));
				float3 ase_worldViewDir = ( _WorldSpaceCameraPos.xyz - WorldPosition );
				ase_worldViewDir = normalize(ase_worldViewDir);
				float3 ase_worldNormal = IN.ase_texcoord6.xyz;
				float dotResult106 = dot( ase_worldViewDir , ase_worldNormal );
				float softedge111 = pow( saturate( abs( dotResult106 ) ) , _softFacotr );
				float4 screenPos88 = IN.ase_texcoord7;
				float4 ase_screenPosNorm88 = screenPos88 / screenPos88.w;
				ase_screenPosNorm88.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm88.z : ase_screenPosNorm88.z * 0.5 + 0.5;
				float screenDepth88 = LinearEyeDepth(SHADERGRAPH_SAMPLE_SCENE_DEPTH( ase_screenPosNorm88.xy ),_ZBufferParams);
				float distanceDepth88 = saturate( ( screenDepth88 - LinearEyeDepth( ase_screenPosNorm88.z,_ZBufferParams ) ) / ( _DepthfadeFactor ) );
				float lerpResult413 = lerp( distanceDepth88 , 1.0 , _DepthF);
				float temp_output_409_0 = ( 1.0 - saturate( distanceDepth88 ) );
				float lerpResult416 = lerp( 0.0 , temp_output_409_0 , _DepthF);
				float MainAlpha142 = pow( saturate( ( ( MaskAlpha136 * MainTexAlpha37 * DisAplha42 * (( _FNLfanxiangkaiguan )?( softedge111 ):( 1.0 )) * (( _Depthfadeon )?( lerpResult413 ):( 1.0 )) ) + lerpResult416 ) ) , _MainAlphaPower );
				float4 DepthColor412 = ( temp_output_409_0 * _DepthColor );
				float4 lerpResult422 = lerp( float4( 0,0,0,0 ) , DepthColor412 , _DepthF);
				float3 normalizeResult407 = normalize( ( ase_worldViewDir + _Dir ) );
				float fresnelNdotV91 = dot( ase_worldNormal, normalizeResult407 );
				float fresnelNode91 = ( 0.0 + _fnl_sacle * pow( 1.0 - fresnelNdotV91, _fnl_power ) );
				float switchResult438 = (((ase_vface>0)?(saturate( fresnelNode91 )):(0.0)));
				float4 fnlColor97 = ( switchResult438 * _fnl_color * IN.ase_color );
				float4 lerpResult429 = lerp( tex2DNode1 , tex2D( _AddTex, appendResult355 ) , _AddTexLerp);
				float4 temp_cast_5 = (1.0).xxxx;
				float4 MaskColor439 = tex2DNode52;
				float4 MainColornoparticle224 = ( _MainColor * lerpResult429 * (( _IfMaskColor )?( MaskColor439 ):( temp_cast_5 )) );
				float4 lerpResult230 = lerp( MainColornoparticle224 , _DIssloveColor , _DIssloveColor.a);
				float4 lerpResult33 = lerp( lerpResult230 , MainColornoparticle224 , smoothstepResult27);
				float temp_output_234_0 = ( temp_output_233_0 - step( (( _CustomdataDis )?( texCoord51.z ):( _DIssloveFactor )) , (( _DissolveAR )?( tex2DNode25.r ):( tex2DNode25.a )) ) );
				float4 lerpResult244 = lerp( MainColornoparticle224 , ( lerpResult230 * temp_output_234_0 ) , temp_output_234_0);
				float4 DisColor40 = ( IN.ase_color * (( _soft_sting )?( lerpResult244 ):( lerpResult33 )) );
				float4 texCoord193 = IN.ase_texcoord4;
				texCoord193.xy = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult214 = (float2(_ScreenU , _ScreenV));
				float2 uv_ScreenDistortTex = IN.ase_texcoord3.xy * _ScreenDistortTex_ST.xy + _ScreenDistortTex_ST.zw;
				float2 panner210 = ( 1.0 * _Time.y * appendResult214 + uv_ScreenDistortTex);
				float cos292 = cos( ( ( ( _ScreenR / 360.0 ) * PI ) * 2.0 ) );
				float sin292 = sin( ( ( ( _ScreenR / 360.0 ) * PI ) * 2.0 ) );
				float2 rotator292 = mul( panner210 - float2( 0.5,0.5 ) , float2x2( cos292 , -sin292 , sin292 , cos292 )) + float2( 0.5,0.5 );
				float2 break377 = rotator292;
				float2 break376 = rotator292;
				float clampResult293 = clamp( break376.x , 0.0 , 1.0 );
				float clampResult378 = clamp( break376.y , 0.0 , 1.0 );
				float2 appendResult380 = (float2((( _ScreenC )?( clampResult293 ):( break377.x )) , (( _ScreenCV )?( clampResult378 ):( break377.y ))));
				float4 tex2DNode185 = tex2D( _ScreenDistortTex, appendResult380 );
				float2 uv_ScreenDistortMask = IN.ase_texcoord3.xy * _ScreenDistortMask_ST.xy + _ScreenDistortMask_ST.zw;
				float cos269 = cos( ( ( ( _ScreenMaskR / 360.0 ) * PI ) * 2.0 ) );
				float sin269 = sin( ( ( ( _ScreenMaskR / 360.0 ) * PI ) * 2.0 ) );
				float2 rotator269 = mul( uv_ScreenDistortMask - float2( 0.5,0.5 ) , float2x2( cos269 , -sin269 , sin269 , cos269 )) + float2( 0.5,0.5 );
				float2 break382 = rotator269;
				float2 break381 = rotator269;
				float clampResult271 = clamp( break381.x , 0.0 , 1.0 );
				float clampResult383 = clamp( break381.y , 0.0 , 1.0 );
				float2 appendResult385 = (float2((( _ScreenMaskC )?( clampResult271 ):( break382.x )) , (( _ScreenMaskCV )?( clampResult383 ):( break382.y ))));
				float4 tex2DNode215 = tex2D( _ScreenDistortMask, appendResult385 );
				float4 screenPos = IN.ase_texcoord8;
				float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( screenPos );
				float4 ase_grabScreenPosNorm = ase_grabScreenPos / ase_grabScreenPos.w;
				float4 fetchOpaqueVal182 = float4( SHADERGRAPH_SAMPLE_SCENE_COLOR( ( ( (( _CustomdataScreenUV )?( texCoord193.z ):( _ScreenDistortTexScale )) * (( _ScreenAR )?( tex2DNode185.r ):( tex2DNode185.a )) * (( _ScreenMaskAR )?( tex2DNode215.r ):( tex2DNode215.a )) ) + ase_grabScreenPosNorm ).xy ), 1.0 );
				float4 GrabScreen192 = fetchOpaqueVal182;
				float4 temp_output_145_0 = ( (( _AlphaAdd )?( MainAlpha142 ):( 1.0 )) * ( lerpResult422 + (( _Screencoloron )?( ( GrabScreen192 + fnlColor97 ) ):( ( (( _FNLkaiguan )?( fnlColor97 ):( fnlColor97 )) + DisColor40 ) )) ) );
				float4 switchResult433 = (((ase_vface>0)?(temp_output_145_0):(( temp_output_145_0 * _BackFaceColor ))));
				float3 desaturateInitialColor299 = switchResult433.rgb;
				float desaturateDot299 = dot( desaturateInitialColor299, float3( 0.299, 0.587, 0.114 ));
				float3 desaturateVar299 = lerp( desaturateInitialColor299, desaturateDot299.xxx, _qubaohedu );
				float3 zong329 = desaturateVar299;
				
				float depthfade201 = (( _Depthfadeon )?( lerpResult413 ):( 1.0 ));
				
				float3 BakedAlbedo = 0;
				float3 BakedEmission = 0;
				float3 Color = zong329;
				float Alpha = (( _screenalphaon )?( ( 1.0 * depthfade201 * IN.ase_color.a * softedge111 ) ):( MainAlpha142 ));
				float AlphaClipThreshold = 0.5;
				float AlphaClipThresholdShadow = 0.5;

				#ifdef _ALPHATEST_ON
					clip( Alpha - AlphaClipThreshold );
				#endif

				#ifdef LOD_FADE_CROSSFADE
					LODDitheringTransition( IN.clipPos.xyz, unity_LODFade.x );
				#endif

				#ifdef ASE_FOG
					Color = MixFog( Color, IN.fogFactor );
				#endif

				return half4( Color, Alpha );
			}

			ENDHLSL
		}

		
		Pass
		{
			
			Name "DepthOnly"
			Tags { "LightMode"="DepthOnly" }

			ZWrite On
			ColorMask 0
			AlphaToMask Off

			HLSLPROGRAM
			
			#define _RECEIVE_SHADOWS_OFF 1
			#define ASE_SRP_VERSION 70401
			#define REQUIRE_DEPTH_TEXTURE 1

			
			#pragma vertex vert
			#pragma fragment frag

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"

			#define ASE_NEEDS_VERT_NORMAL
			#define ASE_NEEDS_FRAG_WORLD_POSITION
			#define ASE_NEEDS_VERT_POSITION
			#define ASE_NEEDS_FRAG_COLOR


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 worldPos : TEXCOORD0;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 shadowCoord : TEXCOORD1;
				#endif
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_color : COLOR;
				float4 ase_texcoord4 : TEXCOORD4;
				float4 ase_texcoord5 : TEXCOORD5;
				float4 ase_texcoord6 : TEXCOORD6;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _DepthColor;
			float4 _MaskTex_ST;
			float4 _fnl_color;
			float4 _VTOTex_ST;
			float4 _ScreenDistortTex_ST;
			float4 _DIssloveColor;
			float4 _DissloveTex_ST;
			float4 _MainTex_ST;
			float4 _ScreenDistortMask_ST;
			float4 _MainColor;
			float4 _BackFaceColor;
			float4 _DistortTex_ST;
			float4 _VTOMaskTex_ST;
			float3 _Dir;
			float _MainAlphaPower;
			float _DepthF;
			float _DepthfadeFactor;
			float _Depthfadeon;
			float _DissolveCV;
			float _FNLfanxiangkaiguan;
			float _DIssloveWide;
			float _DIssolve_rotat;
			float _DistortDisTex;
			float _DisTex_Vspeed;
			float _DisTex_Uspeed;
			float _DissolveC;
			float _softFacotr;
			float _Ztest;
			float _fnl_sacle;
			float _FNLkaiguan;
			float _ScreenMaskCV;
			float _ScreenMaskR;
			float _ScreenMaskC;
			float _ScreenMaskAR;
			float _ScreenCV;
			float _ScreenR;
			float _ScreenV;
			float _ScreenU;
			float _ScreenC;
			float _ScreenAR;
			float _ScreenDistortTexScale;
			float _CustomdataScreenUV;
			float _IfMaskColor;
			float _AddTexLerp;
			float _soft_sting;
			float _fnl_power;
			float _DissolveAR;
			float _Screencoloron;
			float _DIssloveSoft;
			float _MainAlpha;
			float _CustomdataDis;
			float _AlphaAdd;
			float _VTOMaskCV;
			float _VTOMaskR;
			float _VTOMaskC;
			float _VTOMaskAR;
			float _VTOFactor;
			float _ToggleSwitch0;
			float _VTOCV;
			float _Mask_scale;
			float _VTOR;
			float _VTOTex_Uspeed;
			float _VTOC;
			float _VTOAR;
			float _screenVTOon;
			float _Scr;
			float _Cullmode;
			float _Dst;
			float _Zwrite;
			float _VTOTex_Vspeed;
			float _MaskAlphaRA;
			float _MaskC;
			float _Mask_Uspeed;
			float _sot_sting_A;
			float _qubaohedu;
			float _MaintexCV;
			float _MainTex_rotat;
			float _DistortMainTex;
			float _CustomdataMainTexUV;
			float _MainTex_Vspeed;
			float _MainTex_Uspeed;
			float _MaintexC;
			float _MainTex_ar;
			float _MaskCV;
			float _Mask_rotat;
			float _CustomdataMaskUV;
			float _DistortMask;
			float _DistortTex_Vspeed;
			float _DistortTex_Uspeed;
			float _CustomDistort;
			float _DistortFactor;
			float _Mask_Vspeed;
			float _DIssloveFactor;
			float _screenalphaon;
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END
			sampler2D _VTOTex;
			sampler2D _VTOMaskTex;
			sampler2D _MaskTex;
			sampler2D _DistortTex;
			sampler2D _MainTex;
			sampler2D _DissloveTex;
			uniform float4 _CameraDepthTexture_TexelSize;


			
			VertexOutput VertexFunction( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				float2 appendResult76 = (float2(_VTOTex_Uspeed , _VTOTex_Vspeed));
				float2 uv_VTOTex = v.ase_texcoord.xy * _VTOTex_ST.xy + _VTOTex_ST.zw;
				float2 panner77 = ( 1.0 * _Time.y * appendResult76 + uv_VTOTex);
				float cos287 = cos( ( ( ( _VTOR / 360.0 ) * PI ) * 2.0 ) );
				float sin287 = sin( ( ( ( _VTOR / 360.0 ) * PI ) * 2.0 ) );
				float2 rotator287 = mul( panner77 - float2( 0.5,0.5 ) , float2x2( cos287 , -sin287 , sin287 , cos287 )) + float2( 0.5,0.5 );
				float2 break367 = rotator287;
				float2 break366 = rotator287;
				float clampResult278 = clamp( break366.x , 0.0 , 1.0 );
				float clampResult368 = clamp( break366.y , 0.0 , 1.0 );
				float2 appendResult370 = (float2((( _VTOC )?( clampResult278 ):( break367.x )) , (( _VTOCV )?( clampResult368 ):( break367.y ))));
				float4 tex2DNode72 = tex2Dlod( _VTOTex, float4( appendResult370, 0, 0.0) );
				float4 texCoord85 = v.ase_texcoord1;
				texCoord85.xy = v.ase_texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
				float2 uv_VTOMaskTex = v.ase_texcoord.xy * _VTOMaskTex_ST.xy + _VTOMaskTex_ST.zw;
				float cos263 = cos( ( ( ( _VTOMaskR / 360.0 ) * PI ) * 2.0 ) );
				float sin263 = sin( ( ( ( _VTOMaskR / 360.0 ) * PI ) * 2.0 ) );
				float2 rotator263 = mul( uv_VTOMaskTex - float2( 0.5,0.5 ) , float2x2( cos263 , -sin263 , sin263 , cos263 )) + float2( 0.5,0.5 );
				float2 break372 = rotator263;
				float2 break371 = rotator263;
				float clampResult257 = clamp( break371.x , 0.0 , 1.0 );
				float clampResult373 = clamp( break371.y , 0.0 , 1.0 );
				float2 appendResult375 = (float2((( _VTOMaskC )?( clampResult257 ):( break372.x )) , (( _VTOMaskCV )?( clampResult373 ):( break372.y ))));
				float4 tex2DNode81 = tex2Dlod( _VTOMaskTex, float4( appendResult375, 0, 0.0) );
				float3 VTO82 = ( (( _VTOAR )?( tex2DNode72.r ):( tex2DNode72.a )) * v.ase_normal * (( _ToggleSwitch0 )?( texCoord85.w ):( _VTOFactor )) * (( _VTOMaskAR )?( tex2DNode81.r ):( tex2DNode81.a )) );
				float3 temp_cast_0 = (0.0).xxx;
				
				float3 ase_worldNormal = TransformObjectToWorldNormal(v.ase_normal);
				o.ase_texcoord5.xyz = ase_worldNormal;
				float3 vertexPos88 = v.vertex.xyz;
				float4 ase_clipPos88 = TransformObjectToHClip((vertexPos88).xyz);
				float4 screenPos88 = ComputeScreenPos(ase_clipPos88);
				o.ase_texcoord6 = screenPos88;
				
				o.ase_texcoord2.xy = v.ase_texcoord.xy;
				o.ase_texcoord3 = v.ase_texcoord2;
				o.ase_color = v.ase_color;
				o.ase_texcoord4 = v.ase_texcoord1;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord2.zw = 0;
				o.ase_texcoord5.w = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = (( _screenVTOon )?( temp_cast_0 ):( VTO82 ));
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				o.worldPos = positionWS;
				#endif

				o.clipPos = TransformWorldToHClip( positionWS );
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = o.clipPos;
					o.shadowCoord = GetShadowCoord( vertexInput );
				#endif
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_color : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_texcoord1 = v.ase_texcoord1;
				o.ase_texcoord2 = v.ase_texcoord2;
				o.ase_color = v.ase_color;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_texcoord1 = patch[0].ase_texcoord1 * bary.x + patch[1].ase_texcoord1 * bary.y + patch[2].ase_texcoord1 * bary.z;
				o.ase_texcoord2 = patch[0].ase_texcoord2 * bary.x + patch[1].ase_texcoord2 * bary.y + patch[2].ase_texcoord2 * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			half4 frag(VertexOutput IN  ) : SV_TARGET
			{
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 WorldPosition = IN.worldPos;
				#endif
				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = IN.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif

				float2 appendResult222 = (float2(_Mask_Uspeed , _Mask_Vspeed));
				float2 uv_MaskTex = IN.ase_texcoord2.xy * _MaskTex_ST.xy + _MaskTex_ST.zw;
				float2 temp_cast_0 = (0.0).xx;
				float4 texCoord451 = IN.ase_texcoord3;
				texCoord451.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float lerpResult450 = lerp( _DistortFactor , texCoord451.w , _CustomDistort);
				float2 appendResult58 = (float2(_DistortTex_Uspeed , _DistortTex_Vspeed));
				float2 uv_DistortTex = IN.ase_texcoord2.xy * _DistortTex_ST.xy + _DistortTex_ST.zw;
				float2 panner59 = ( 1.0 * _Time.y * appendResult58 + uv_DistortTex);
				float4 tex2DNode54 = tex2D( _DistortTex, panner59 );
				float2 appendResult61 = (float2(tex2DNode54.r , tex2DNode54.g));
				float2 DistortUV60 = ( lerpResult450 * appendResult61 );
				float2 lerpResult447 = lerp( temp_cast_0 , DistortUV60 , _DistortMask);
				float2 panner219 = ( 1.0 * _Time.y * appendResult222 + ( uv_MaskTex + lerpResult447 ));
				float2 temp_cast_1 = (0.0).xx;
				float2 texCoord134 = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float cos149 = cos( ( ( ( _Mask_rotat / 360.0 ) * PI ) * 2.0 ) );
				float sin149 = sin( ( ( ( _Mask_rotat / 360.0 ) * PI ) * 2.0 ) );
				float2 rotator149 = mul( ( panner219 + (( _CustomdataMaskUV )?( texCoord134 ):( temp_cast_1 )) ) - float2( 0.5,0.5 ) , float2x2( cos149 , -sin149 , sin149 , cos149 )) + float2( 0.5,0.5 );
				float2 break362 = rotator149;
				float2 break361 = rotator149;
				float clampResult247 = clamp( break361.x , 0.0 , 1.0 );
				float clampResult363 = clamp( break361.y , 0.0 , 1.0 );
				float2 appendResult365 = (float2((( _MaskC )?( clampResult247 ):( break362.x )) , (( _MaskCV )?( clampResult363 ):( break362.y ))));
				float4 tex2DNode52 = tex2D( _MaskTex, appendResult365 );
				float MaskAlpha136 = ( _Mask_scale * (( _MaskAlphaRA )?( tex2DNode52.r ):( tex2DNode52.a )) );
				float2 appendResult14 = (float2(_MainTex_Uspeed , _MainTex_Vspeed));
				float2 temp_cast_2 = (0.0).xx;
				float2 texCoord16 = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float2 uv_MainTex = IN.ase_texcoord2.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float2 temp_cast_3 = (0.0).xx;
				float2 lerpResult118 = lerp( temp_cast_3 , DistortUV60 , _DistortMainTex);
				float2 panner11 = ( 1.0 * _Time.y * appendResult14 + ( (( _CustomdataMainTexUV )?( texCoord16 ):( temp_cast_2 )) + uv_MainTex + lerpResult118 ));
				float cos158 = cos( ( ( ( _MainTex_rotat / 360.0 ) * PI ) * 2.0 ) );
				float sin158 = sin( ( ( ( _MainTex_rotat / 360.0 ) * PI ) * 2.0 ) );
				float2 rotator158 = mul( panner11 - float2( 0.5,0.5 ) , float2x2( cos158 , -sin158 , sin158 , cos158 )) + float2( 0.5,0.5 );
				float2 break351 = rotator158;
				float2 break352 = rotator158;
				float clampResult245 = clamp( break352.x , 0.0 , 1.0 );
				float clampResult353 = clamp( break352.y , 0.0 , 1.0 );
				float2 appendResult355 = (float2((( _MaintexC )?( clampResult245 ):( break351.x )) , (( _MaintexCV )?( clampResult353 ):( break351.y ))));
				float4 tex2DNode1 = tex2D( _MainTex, appendResult355 );
				float MainTexAlpha37 = ( IN.ase_color.a * (( _MainTex_ar )?( tex2DNode1.r ):( tex2DNode1.a )) * _MainColor.a * _MainAlpha );
				float4 texCoord51 = IN.ase_texcoord4;
				texCoord51.xy = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult48 = (float2(_DisTex_Uspeed , _DisTex_Vspeed));
				float2 uv_DissloveTex = IN.ase_texcoord2.xy * _DissloveTex_ST.xy + _DissloveTex_ST.zw;
				float2 temp_cast_4 = (0.0).xx;
				float2 lerpResult122 = lerp( temp_cast_4 , DistortUV60 , _DistortDisTex);
				float2 panner49 = ( 1.0 * _Time.y * appendResult48 + ( uv_DissloveTex + lerpResult122 ));
				float cos162 = cos( ( ( ( _DIssolve_rotat / 360.0 ) * PI ) * 2.0 ) );
				float sin162 = sin( ( ( ( _DIssolve_rotat / 360.0 ) * PI ) * 2.0 ) );
				float2 rotator162 = mul( panner49 - float2( 0.5,0.5 ) , float2x2( cos162 , -sin162 , sin162 , cos162 )) + float2( 0.5,0.5 );
				float2 break357 = rotator162;
				float2 break356 = rotator162;
				float clampResult250 = clamp( break356.x , 0.0 , 1.0 );
				float clampResult358 = clamp( break356.y , 0.0 , 1.0 );
				float2 appendResult360 = (float2((( _DissolveC )?( clampResult250 ):( break357.x )) , (( _DissolveCV )?( clampResult358 ):( break357.y ))));
				float4 tex2DNode25 = tex2D( _DissloveTex, appendResult360 );
				float smoothstepResult27 = smoothstep( ( (( _CustomdataDis )?( texCoord51.z ):( _DIssloveFactor )) - _DIssloveSoft ) , (( _CustomdataDis )?( texCoord51.z ):( _DIssloveFactor )) , (( _DissolveAR )?( tex2DNode25.r ):( tex2DNode25.a )));
				float temp_output_233_0 = step( ( (( _CustomdataDis )?( texCoord51.z ):( _DIssloveFactor )) - _DIssloveWide ) , (( _DissolveAR )?( tex2DNode25.r ):( tex2DNode25.a )) );
				float DisAplha42 = (( _sot_sting_A )?( temp_output_233_0 ):( smoothstepResult27 ));
				float3 ase_worldViewDir = ( _WorldSpaceCameraPos.xyz - WorldPosition );
				ase_worldViewDir = normalize(ase_worldViewDir);
				float3 ase_worldNormal = IN.ase_texcoord5.xyz;
				float dotResult106 = dot( ase_worldViewDir , ase_worldNormal );
				float softedge111 = pow( saturate( abs( dotResult106 ) ) , _softFacotr );
				float4 screenPos88 = IN.ase_texcoord6;
				float4 ase_screenPosNorm88 = screenPos88 / screenPos88.w;
				ase_screenPosNorm88.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm88.z : ase_screenPosNorm88.z * 0.5 + 0.5;
				float screenDepth88 = LinearEyeDepth(SHADERGRAPH_SAMPLE_SCENE_DEPTH( ase_screenPosNorm88.xy ),_ZBufferParams);
				float distanceDepth88 = saturate( ( screenDepth88 - LinearEyeDepth( ase_screenPosNorm88.z,_ZBufferParams ) ) / ( _DepthfadeFactor ) );
				float lerpResult413 = lerp( distanceDepth88 , 1.0 , _DepthF);
				float temp_output_409_0 = ( 1.0 - saturate( distanceDepth88 ) );
				float lerpResult416 = lerp( 0.0 , temp_output_409_0 , _DepthF);
				float MainAlpha142 = pow( saturate( ( ( MaskAlpha136 * MainTexAlpha37 * DisAplha42 * (( _FNLfanxiangkaiguan )?( softedge111 ):( 1.0 )) * (( _Depthfadeon )?( lerpResult413 ):( 1.0 )) ) + lerpResult416 ) ) , _MainAlphaPower );
				float depthfade201 = (( _Depthfadeon )?( lerpResult413 ):( 1.0 ));
				
				float Alpha = (( _screenalphaon )?( ( 1.0 * depthfade201 * IN.ase_color.a * softedge111 ) ):( MainAlpha142 ));
				float AlphaClipThreshold = 0.5;

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				#ifdef LOD_FADE_CROSSFADE
					LODDitheringTransition( IN.clipPos.xyz, unity_LODFade.x );
				#endif
				return 0;
			}
			ENDHLSL
		}

	
	}
	CustomEditor "CommonGUInew"
	
	
}
/*ASEBEGIN
Version=18912
1920;166;1920;853;3403.917;-793.9817;1.6;True;False
Node;AmplifyShaderEditor.CommentaryNode;62;-3347.297,2128.215;Inherit;False;2016.547;443.7441;Distort;11;60;63;61;54;59;57;58;56;55;451;452;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;55;-3329.034,2316.119;Inherit;False;Property;_DistortTex_Uspeed;DistortTex_Uspeed;15;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;56;-3327.034,2407.821;Inherit;False;Property;_DistortTex_Vspeed;DistortTex_Vspeed;16;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;58;-3094.335,2391.42;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;57;-3339.297,2173.215;Inherit;False;0;54;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;59;-2934.335,2367.419;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;54;-2636.58,2338.733;Inherit;True;Property;_DistortTex;DistortTex;10;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;451;-2460.975,2144.778;Inherit;False;2;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;64;-2516.163,2051.689;Inherit;False;Property;_DistortFactor;DistortFactor;14;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;452;-2347.221,2321.31;Inherit;False;Property;_CustomDistort;CustomDistort;103;1;[Enum];Create;True;0;2;Option1;0;Option2;1;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;450;-2130.938,2123.255;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;61;-2107.568,2372.338;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;63;-1953.381,2266.614;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;60;-1742.401,2262.237;Inherit;False;DistortUV;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;44;-4261.663,771.2585;Inherit;False;3194.44;1209.388;Disslove;48;42;242;241;225;237;233;40;234;235;236;29;28;231;226;227;33;230;229;35;27;25;30;163;162;161;49;51;48;160;71;47;45;122;46;177;121;159;70;120;244;249;250;251;356;357;358;359;360;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;446;-1262.755,2133.842;Inherit;False;Constant;_Float3;Float 3;25;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;144;-1001.831,2244.154;Inherit;False;2122.18;664.3225;Mask;26;136;140;141;135;52;149;133;154;138;153;139;134;179;151;219;220;221;222;247;248;362;361;363;364;365;439;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;449;-1251.034,2315.682;Inherit;False;Property;_DistortMask;DistortMask;12;1;[Enum];Create;True;0;2;off;0;on;1;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;121;-3995.703,1225.121;Inherit;False;Property;_DistortDisTex;DistortDisTex;13;1;[Enum];Create;True;0;2;off;0;on;1;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;448;-1272.271,2226.585;Inherit;False;60;DistortUV;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;159;-3574.336,1300.598;Inherit;False;Property;_DIssolve_rotat;DIssolve_rotat;50;0;Create;True;0;0;0;False;0;False;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;120;-3979.703,1049.122;Inherit;False;Constant;_Float2;Float 2;25;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;70;-3990.457,1133.199;Inherit;False;60;DistortUV;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;21;-3604.987,-238.151;Inherit;False;2422.041;867.2671;Main;34;36;6;224;223;37;7;9;5;125;8;1;158;157;11;156;14;17;118;15;127;178;155;245;246;351;352;353;354;355;428;429;441;442;443;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;151;-671.3868,2813.503;Inherit;False;Property;_Mask_rotat;Mask_rotat;43;0;Create;True;0;0;0;False;0;False;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;447;-985.8973,2126.32;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;220;-887.5154,2422.571;Inherit;False;Property;_Mask_Uspeed;Mask_Uspeed;65;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;221;-887.5154,2523.571;Inherit;False;Property;_Mask_Vspeed;Mask_Vspeed;66;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;132;-1193.12,2001.698;Inherit;False;0;52;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;47;-3864.717,1420.782;Inherit;False;Property;_DisTex_Vspeed;DisTex_Vspeed;23;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;67;-4002.593,119.8649;Inherit;False;Constant;_Float1;Float 1;25;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;46;-3866.717,1340.78;Inherit;False;Property;_DisTex_Uspeed;DisTex_Uspeed;22;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;119;-4018.593,295.8648;Inherit;False;Property;_DistortMainTex;DistortMainTex;11;1;[Enum];Create;True;0;2;off;0;on;1;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;45;-3775.212,916.3879;Inherit;False;0;25;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;16;-4098.854,-29.20539;Inherit;False;1;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;177;-3301.325,1375.254;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;360;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-4029.306,-149.831;Inherit;False;Constant;_Float0;Float 0;8;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;65;-4020.1,202.1548;Inherit;False;60;DistortUV;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;155;-3567.166,574.7791;Inherit;False;Property;_MainTex_rotat;MainTex_rotat;44;0;Create;True;0;0;0;False;0;False;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;122;-3691.703,1065.122;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;15;-3776.458,3.308107;Inherit;False;0;1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;445;-784.1443,2054.115;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-3883.855,566.27;Inherit;False;Property;_MainTex_Vspeed;MainTex_Vspeed;8;0;Create;True;0;0;0;False;0;False;0;0.09;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;179;-355.8823,2806.264;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;360;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;127;-3821.114,-133.9071;Inherit;False;Property;_CustomdataMainTexUV;CustomdataMainTexUV;37;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PiNode;160;-3182.336,1273.898;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;178;-3280.098,548.0523;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;360;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;134;-973.8312,2780.476;Inherit;False;2;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;139;-932.5632,2654.654;Inherit;False;Constant;_Float6;Float 6;42;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-3885.855,486.2697;Inherit;False;Property;_MainTex_Uspeed;MainTex_Uspeed;7;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;118;-3714.593,135.8648;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;222;-684.5156,2441.571;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;48;-3643.717,1427.782;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;71;-3489.95,992.9172;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PiNode;153;-281.3866,2680.503;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PiNode;156;-3229.166,424.7786;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;138;-732.3574,2653.603;Inherit;False;Property;_CustomdataMaskUV;CustomdataMaskUV;40;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;49;-3371.549,1135.291;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;14;-3676.855,517.27;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;17;-3492.944,-15.22491;Inherit;False;3;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;219;-542.5157,2288.571;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;161;-3040.336,1372.898;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;133;-310.1891,2391.025;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;11;-3372.794,220.9552;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;157;-3087.166,523.7791;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;154;-139.3866,2779.503;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;162;-3186.581,1130.945;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RotatorNode;158;-2965.166,225.7787;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RotatorNode;149;-114.7904,2484.66;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.BreakToComponentsNode;356;-3148.899,861.754;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.ClampOpNode;358;-3034.899,970.754;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;361;-57.26644,2332.503;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.BreakToComponentsNode;357;-2960.899,1134.754;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.ClampOpNode;250;-3003.904,802.7614;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;352;-2818.67,-6.433899;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.CommentaryNode;114;-2967.72,4511.804;Inherit;False;1527;468.6843;softedge;8;228;111;109;108;110;106;107;105;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ClampOpNode;245;-2692.346,-5.284332;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;259;-3199.963,3951.754;Inherit;False;Property;_VTOMaskR;VTOMaskR;47;0;Create;True;0;0;0;False;0;False;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;363;106.7336,2381.503;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;362;80.73355,2505.503;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.ClampOpNode;353;-2691.67,117.5661;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;351;-2752.67,291.5661;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.ClampOpNode;247;96.56003,2247.441;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;107;-2968.72,4756.485;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;283;-3760,3328;Inherit;False;Property;_VTOR;VTOR;46;0;Create;True;0;0;0;False;0;False;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;105;-2943.89,4562.8;Inherit;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ToggleSwitchNode;249;-2847.549,857.3066;Inherit;False;Property;_DissolveC;DissolveC;74;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;359;-2854.599,987.454;Inherit;False;Property;_DissolveCV;DissolveCV;73;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;354;-2517.67,272.5661;Inherit;False;Property;_MaintexCV;MaintexCV;69;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;83;-2824.27,3278.22;Inherit;False;1558.463;887.9036;VTO;25;72;78;79;82;80;252;81;255;258;262;263;261;257;366;367;368;369;370;371;372;373;374;375;277;278;;1,1,1,1;0;0
Node;AmplifyShaderEditor.DotProductOpNode;106;-2733,4584.547;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;51;-2780.95,1377.267;Inherit;False;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;28;-2788.746,1295.616;Inherit;False;Property;_DIssloveFactor;DIssloveFactor;18;0;Create;True;0;0;0;False;0;False;0.5077091;0.797;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;364;274.7336,2385.503;Inherit;False;Property;_MaskCV;MaskCV;71;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;248;258.56,2264.441;Inherit;False;Property;_MaskC;MaskC;72;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;74;-4404,3249;Inherit;False;Property;_VTOTex_Vspeed;VTOTex_Vspeed;28;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;284;-3488,3376;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;360;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;73;-4404,3169;Inherit;False;Property;_VTOTex_Uspeed;VTOTex_Uspeed;26;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;260;-2927.388,4000.395;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;360;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;360;-2533.342,831.5352;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ToggleSwitchNode;246;-2513.457,149.4351;Inherit;False;Property;_MaintexC;MaintexC;70;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;75;-4404,3041;Inherit;False;0;72;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;29;-2542.046,1519.216;Inherit;False;Property;_DIssloveSoft;DIssloveSoft;20;0;Create;True;0;0;0;False;0;False;0.8235294;0.542;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;163;-2474.48,1344.895;Inherit;False;Property;_CustomdataDis;CustomdataDis;51;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;25;-2651.212,1070.704;Inherit;True;Property;_DissloveTex;DissloveTex;17;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;235;-2516.168,1638.414;Inherit;False;Property;_DIssloveWide;DIssloveWide;19;0;Create;True;0;0;0;False;0;False;0.02352943;0.542;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;365;499.7335,2318.503;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PiNode;262;-2769.064,3977.055;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;228;-2565.35,4585.361;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;76;-4164,3233;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PiNode;285;-3360,3312;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;355;-2310.67,229.5661;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;236;-2169.168,1575.414;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;286;-3216,3408;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;77;-4020,3217;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;261;-2627.064,4076.055;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;110;-2487.823,4859.285;Inherit;False;Property;_softFacotr;softFacotr;34;0;Create;True;0;0;0;False;0;False;1;0;0;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;172;-996.4691,-218.3116;Inherit;False;1472.581;1156.105;alpha;25;116;142;128;129;126;130;43;168;137;39;115;169;88;89;174;175;201;408;409;410;412;413;414;415;416;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;1;-2175.418,174.4658;Inherit;True;Property;_MainTex;MainTex;4;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;256;-2895.839,3805.428;Inherit;False;0;81;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;30;-2290.229,1216.817;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;52;104.2547,2668.34;Inherit;True;Property;_MaskTex;MaskTex;9;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ToggleSwitchNode;251;-2315.964,1107.066;Inherit;False;Property;_DissolveAR;DissolveAR;75;0;Create;True;0;0;0;False;0;False;1;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;108;-2442.423,4587.485;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;109;-2217.723,4616.484;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;233;-1995.168,1662.414;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;287;-3184,3216;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;89;-1024.777,671.9241;Float;False;Property;_DepthfadeFactor;DepthfadeFactor;35;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;263;-2634.309,3798.1;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.VertexColorNode;5;-2042.036,-188.151;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;141;380.8255,2468.907;Inherit;False;Property;_Mask_scale;Mask_scale;41;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;116;-1008.777,511.924;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;8;-2338.951,-163.1268;Inherit;False;Property;_MainColor;MainColor;6;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,1;1,0.6179246,0.6179246,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ToggleSwitchNode;125;-1834.34,239.7968;Inherit;False;Property;_MainTex_ar;MainTex_a/r;36;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;27;-2038.44,1150.336;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;135;444.3535,2657.68;Inherit;False;Property;_MaskAlphaRA;MaskAlphaRA;39;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-1659.634,477.8288;Inherit;False;Property;_MainAlpha;MainAlpha;5;0;Create;True;0;0;0;False;0;False;1;1;0;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;111;-1973.077,4609.98;Inherit;False;softedge;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;371;-2463.077,3718.135;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.DepthFade;88;-814.7766,591.9241;Inherit;False;True;True;False;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-1594.177,118.102;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;366;-2771.8,3416.8;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;175;-636.3925,418.0108;Inherit;False;Constant;_Float8;Float 8;48;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;242;-1726.253,1420.629;Inherit;False;Property;_sot_sting_A;sot_sting_A;68;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;414;-821.1987,788.2821;Inherit;False;Property;_DepthF;DepthF;96;1;[Enum];Create;True;0;2;Option1;0;Option2;1;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;140;636.8255,2603.906;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;37;-1390.115,113.1548;Inherit;False;MainTexAlpha;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;373;-2331.077,3853.135;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;257;-2339.738,3707.128;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;115;-946.4691,331.7197;Inherit;False;111;softedge;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;169;-939.1066,251.0325;Inherit;False;Constant;_Float5;Float 5;47;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;368;-2611.8,3512.8;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;367;-2787.8,3576.8;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.BreakToComponentsNode;372;-2473.077,3917.135;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.LerpOp;413;-516.4715,561.4623;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;408;-566.3372,688.3004;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;42;-1536.144,1195.263;Inherit;False;DisAplha;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;136;818.8151,2617.955;Inherit;False;MaskAlpha;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;278;-2613.1,3362.4;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;258;-2217.738,3748.129;Inherit;False;Property;_VTOMaskC;VTOMaskC;79;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;409;-399.3361,704.3004;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;43;-812.1348,155.2178;Inherit;False;42;DisAplha;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;369;-2403.8,3592.8;Inherit;False;Property;_VTOCV;VTOCV;87;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;168;-739.0899,301.8241;Inherit;False;Property;_FNLfanxiangkaiguan;FNLfanxiangkaiguan;52;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;277;-2454.9,3437.699;Inherit;False;Property;_VTOC;VTOC;84;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;374;-2296.077,3986.135;Inherit;False;Property;_VTOMaskCV;VTOMaskCV;78;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;174;-302.5825,515.1591;Inherit;False;Property;_Depthfadeon;Depthfadeon;55;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;137;-820.5864,-83.81155;Inherit;False;136;MaskAlpha;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;39;-830.444,28.27603;Inherit;False;37;MainTexAlpha;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;370;-2243.82,3463.285;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;416;-399.4585,312.2271;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;375;-2134.077,3872.135;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;130;-440.9518,48.2608;Inherit;False;5;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;85;-2541.919,4245.759;Inherit;False;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;415;-249.5124,164.7684;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;80;-2506.456,4163.018;Inherit;False;Property;_VTOFactor;VTOFactor;25;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;72;-2111.581,3492.715;Inherit;True;Property;_VTOTex;VTOTex;24;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;81;-2003.331,3989.402;Inherit;True;Property;_VTOMaskTex;VTOMaskTex;30;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ToggleSwitchNode;171;-2310.147,4202.297;Inherit;False;Property;_ToggleSwitch0;Toggle Switch0;54;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;129;-187.7582,303.1335;Inherit;False;Property;_MainAlphaPower;MainAlphaPower;38;0;Create;True;0;0;0;False;0;False;1;1;1;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;126;-114.7178,25.97701;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;255;-1720.738,3952.128;Inherit;False;Property;_VTOMaskAR;VTOMaskAR;77;0;Create;True;0;0;0;False;0;False;1;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;79;-1989.932,3688.127;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ToggleSwitchNode;252;-1835.249,3371.589;Inherit;False;Property;_VTOAR;VTOAR;76;0;Create;True;0;0;0;False;0;False;1;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;78;-1663.406,3520.041;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.PowerNode;128;52.97631,27.5736;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;201;29.45018,561.212;Inherit;False;depthfade;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;216;1322.981,767.6484;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;217;1326.807,973.2303;Inherit;False;111;softedge;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;82;-1489.807,3514.53;Inherit;False;VTO;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;202;1332.807,689.6834;Inherit;False;201;depthfade;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;142;237.4843,22.41043;Inherit;False;MainAlpha;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;200;1622.253,1058.955;Inherit;False;Constant;_Float10;Float 10;54;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;143;1360.273,543.2316;Inherit;False;142;MainAlpha;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;10;-3130.546,-636.0404;Inherit;False;1063.897;312.1366;Comment;5;2;3;4;123;421;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;203;1550.29,704.4694;Inherit;False;4;4;0;FLOAT;1;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;99;-2760.045,2679.036;Inherit;False;1729;481;fnl;10;94;91;92;97;95;93;96;407;425;438;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;209;-1304.82,-1757.95;Inherit;False;1961.509;930.4128;Comment;23;183;192;182;184;186;194;185;187;193;215;270;272;275;293;294;378;379;380;271;381;383;384;385;;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;84;1597.108,876.8381;Inherit;False;82;VTO;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ToggleSwitchNode;275;-373.115,-1347.641;Inherit;False;Property;_ScreenAR;ScreenAR;83;0;Create;True;0;0;0;False;0;False;1;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;337;-3169.449,3008.135;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;-1,-1,-1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ClampOpNode;293;-1160.097,-1660.076;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;267;-1726.991,-1038.045;Inherit;False;0;215;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;291;-1628.85,-1306.097;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;382;-1306.439,-1015.643;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SamplerNode;428;-2173.518,434.4887;Inherit;True;Property;_AddTex;AddTex;99;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;443;-2361.426,119.9547;Inherit;False;439;MaskColor;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.ToggleSwitchNode;294;-1009.097,-1600.076;Inherit;False;Property;_ScreenC;ScreenC;89;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;289;-1889.839,-1303.741;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;360;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;425;-2746.809,2761.864;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;264;-1998.318,-776.4194;Inherit;False;Property;_ScreenMaskR;ScreenMaskR;49;0;Create;True;0;0;0;False;0;False;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;93;-2727.045,2948.036;Inherit;False;Property;_fnl_power;fnl_power;31;0;Create;True;0;0;0;False;0;False;1;0;1;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;435;2247.965,-554.2939;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;387;-5246.647,2713.723;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;360;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;376;-1324.403,-1655.368;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.StepOpNode;231;-2009.168,1407.414;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;272;-497.2246,-1008.079;Inherit;False;Property;_ScreenMaskAR;ScreenMaskAR;82;0;Create;True;0;0;0;False;0;False;1;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;229;-2340.078,1023.661;Inherit;False;224;MainColornoparticle;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.ToggleSwitchNode;384;-1057.439,-984.6431;Inherit;False;Property;_ScreenMaskCV;ScreenMaskCV;80;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;377;-1321.403,-1480.368;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RegisterLocalVarNode;412;50.60144,728.6154;Inherit;False;DepthColor;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-3080.546,-586.0395;Float;False;Property;_Scr;Scr;2;1;[Enum];Create;True;0;1;Option1;0;1;UnityEngine.Rendering.BlendMode;True;0;False;5;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;431;-1904.946,3031.343;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RotatorNode;292;-1592.095,-1494.051;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;2;-2938.145,-585.0394;Float;False;Property;_Cullmode;Cullmode;0;1;[Enum];Create;True;0;1;Option1;0;1;UnityEngine.Rendering.CullMode;True;0;False;0;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;95;-1674.153,2739.009;Inherit;False;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FresnelNode;91;-2453.657,2735.832;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;390;-5486.647,2473.723;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-3080.546,-492.0387;Float;False;Property;_Dst;Dst;3;1;[Enum];Create;True;0;1;Option1;0;1;UnityEngine.Rendering.BlendMode;True;0;False;10;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;395;-4942.647,2553.723;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;288;-2173.85,-1376.397;Inherit;False;Property;_ScreenR;ScreenR;48;0;Create;True;0;0;0;False;0;False;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;380;-817.4026,-1522.368;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;424;1789.111,-398.2992;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;411;-433.3986,836.6154;Inherit;False;Property;_DepthColor;DepthColor;95;1;[HDR];Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;193;-705.647,-1582.636;Inherit;False;2;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;184;-24.86218,-1213.492;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;234;-1762.277,1640.718;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;215;-784.1361,-1027.377;Inherit;True;Property;_ScreenDistortMask;ScreenDistortMask;56;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;230;-2019.152,856.0782;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ToggleSwitchNode;379;-1019.403,-1456.368;Inherit;False;Property;_ScreenCV;ScreenCV;88;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SwitchByFaceNode;438;-1925.709,2703.812;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalizeNode;407;-2614.789,2762.568;Inherit;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ToggleSwitchNode;441;-2177.426,68.95471;Inherit;False;Property;_IfMaskColor;IfMaskColor;102;0;Create;True;0;0;0;False;0;False;0;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ScreenColorNode;182;138.1378,-1071.492;Inherit;False;Global;_GrabScreen0;Grab Screen 0;49;0;Create;True;0;0;0;False;0;False;Object;-1;False;False;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;271;-1190.991,-1241.045;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;268;-1453.318,-706.1191;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;336;-4186.735,2863.191;Inherit;False;Property;_NormalScale;NormalScale;94;0;Create;True;0;0;0;False;0;False;0;0;-5;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;96;-1912.068,2810.558;Inherit;False;Property;_fnl_color;fnl_color;33;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;97;-1253.371,2731.632;Inherit;False;fnlColor;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;244;-1334.085,1353.998;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ToggleSwitchNode;333;-3004.748,2784.286;Inherit;False;Property;_VW;VW;92;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WorldNormalVector;340;-3244.689,2686.868;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;410;-139.3986,735.6154;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;301;2192.915,-68.51029;Inherit;False;Property;_qubaohedu;qubaohedu;90;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;36;-1439.788,-71.3447;Inherit;False;MainColor;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;399;-4414.647,2377.723;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;402;-4046.669,2520.208;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;391;-5726.647,2281.723;Inherit;False;0;332;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ToggleSwitchNode;170;1032.233,-435.8027;Inherit;False;Property;_FNLkaiguan;FNLkaiguan;53;0;Create;True;0;0;0;False;0;False;0;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;383;-1195.439,-1099.643;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PiNode;393;-5118.647,2649.723;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;388;-5726.647,2409.723;Inherit;False;Property;_NormalU;NormalU;27;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;381;-1334.439,-1119.643;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;226;-1465.772,922.4979;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PiNode;290;-1770.85,-1405.097;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;187;-750.6973,-1707.95;Float;False;Property;_ScreenDistortTexScale;ScreenDistortTexScale;58;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;330;-3206.376,2846.304;Inherit;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;386;-5518.647,2665.723;Inherit;False;Property;_NormalR;NormalR;45;0;Create;True;0;0;0;False;0;False;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;213;-2229,-1545;Inherit;False;Property;_ScreenV;ScreenV;64;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;436;1954.965,-422.2939;Inherit;False;Property;_BackFaceColor;BackFaceColor;101;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;165;1293.806,-254.1787;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;223;-1870.595,44.16895;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;214;-2037,-1593;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;212;-2229,-1625;Inherit;False;Property;_ScreenU;ScreenU;63;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;432;1510.866,-565.2027;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;205;1448.031,97.77708;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;331;-3871.483,3009.137;Inherit;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.GetLocalVarNode;423;581.6509,167.1114;Inherit;False;412;DepthColor;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;385;-843.4395,-1160.643;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DesaturateOpNode;299;2416.632,-212.7947;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;186;-175.4025,-1365.391;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;398;-4414.647,2569.723;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;199;1798.253,963.9552;Inherit;False;Property;_screenVTOon;screenVTOon;62;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;429;-1840.518,431.4887;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;101;658.5066,-421.9693;Inherit;False;97;fnlColor;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;389;-5726.647,2489.723;Inherit;False;Property;_NormalV;NormalV;29;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PiNode;266;-1595.318,-805.1191;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GrabScreenPosition;183;-192.0403,-1056.102;Inherit;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;131;890.3591,12.70316;Inherit;False;40;DisColor;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.BreakToComponentsNode;397;-4574.647,2473.723;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.LerpOp;422;873.1873,144.1573;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;185;-666.2136,-1375.086;Inherit;True;Property;_ScreenDistortTex;ScreenDistortTex;57;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;427;-2954.909,2900.164;Inherit;False;Property;_Dir;Dir;98;0;Create;True;0;0;0;False;0;False;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;92;-2745.609,2861.619;Inherit;False;Property;_fnl_sacle;fnl_sacle;32;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;270;-1048.991,-1152.045;Inherit;False;Property;_ScreenMaskC;ScreenMaskC;81;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;332;-3691.301,2680.355;Inherit;True;Property;_NormalTex;NormalTex;91;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;black;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ToggleSwitchNode;400;-4222.648,2425.723;Inherit;False;Property;_NormalC;NormalC;85;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;394;-4974.647,2745.723;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;335;-2937.301,2681.355;Inherit;False;Property;_WN;WN;93;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-1704.605,-136.0835;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ToggleSwitchNode;241;-1284.313,1201.915;Inherit;False;Property;_soft_sting;soft_sting;67;0;Create;True;0;0;0;False;0;False;0;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;35;-2325.143,825.1823;Inherit;False;Property;_DIssloveColor;DIssloveColor;21;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,1;1,0.4987022,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;225;-2040.934,1040.991;Inherit;False;224;MainColornoparticle;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;211;-2069,-1769;Inherit;False;0;185;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ToggleSwitchNode;194;-457.0467,-1587.537;Inherit;False;Property;_CustomdataScreenUV;CustomdataScreenUV;59;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;265;-1714.307,-703.7632;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;360;False;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;269;-1416.563,-894.0732;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;392;-5342.647,2457.723;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SaturateNode;94;-2150.722,2736.296;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;396;-4590.647,2633.723;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.GetLocalVarNode;206;1223.154,190.1606;Inherit;False;97;fnlColor;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;430;-2172.518,639.4887;Inherit;False;Property;_AddTexLerp;AddTexLerp;100;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;421;-2790.148,-492.306;Inherit;False;Property;_Zwrite;Zwrite;97;0;Create;True;0;0;0;True;0;False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;227;-1710.963,816.2093;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldNormalVector;334;-3221.301,2506.355;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ToggleSwitchNode;146;1706.276,-627.2061;Inherit;False;Property;_AlphaAdd;AlphaAdd;42;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;401;-4206.648,2649.723;Inherit;False;Property;_NormalCV;NormalCV;86;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;145;1953.126,-562.5305;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;192;432.6888,-1070.801;Inherit;False;GrabScreen;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;237;-1497.964,1411.179;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;40;-1260.911,923.1755;Inherit;False;DisColor;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;33;-1664.205,1028.553;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;224;-1606.453,26.34498;Inherit;False;MainColornoparticle;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;439;461.626,2771.751;Inherit;False;MaskColor;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;148;1291.218,-600.6449;Inherit;False;142;MainAlpha;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SwitchByFaceNode;433;2382.295,-664.0635;Inherit;False;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PannerNode;210;-1813,-1625;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;329;2604.484,-210.3639;Inherit;False;zong;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ClampOpNode;378;-1169.403,-1537.368;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;442;-2325.426,31.95471;Inherit;False;Constant;_Float9;Float 9;103;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;123;-2794.601,-583.2109;Float;False;Property;_Ztest;Ztest;1;1;[Enum];Create;True;0;1;Option1;0;1;UnityEngine.Rendering.CompareFunction;True;0;False;4;4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;147;1522.276,-694.2061;Inherit;False;Constant;_Float7;Float 7;44;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;196;1240.031,65.77709;Inherit;False;192;GrabScreen;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.ToggleSwitchNode;195;1625.227,-207.2373;Inherit;False;Property;_Screencoloron;Screencoloron;61;0;Create;True;0;0;0;False;0;False;0;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ToggleSwitchNode;197;1796.736,544.8234;Inherit;False;Property;_screenalphaon;screenalphaon;60;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;457;2928.156,-79.93816;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;Meta;0;4;Meta;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;True;17;d3d9;d3d11;glcore;gles;gles3;metal;vulkan;xbox360;xboxone;xboxseries;ps4;playstation;psp2;n3ds;wiiu;switch;nomrt;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=Meta;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;456;2928.156,-79.93816;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;DepthOnly;0;3;DepthOnly;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;True;17;d3d9;d3d11;glcore;gles;gles3;metal;vulkan;xbox360;xboxone;xboxseries;ps4;playstation;psp2;n3ds;wiiu;switch;nomrt;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;False;False;True;False;False;False;False;0;False;-1;False;False;False;False;False;False;False;False;False;True;1;False;-1;False;False;True;1;LightMode=DepthOnly;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;455;2928.156,-79.93816;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;ShadowCaster;0;2;ShadowCaster;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;True;17;d3d9;d3d11;glcore;gles;gles3;metal;vulkan;xbox360;xboxone;xboxseries;ps4;playstation;psp2;n3ds;wiiu;switch;nomrt;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;False;-1;True;3;False;-1;False;True;1;LightMode=ShadowCaster;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;453;2928.156,-79.93816;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;ExtraPrePass;0;0;ExtraPrePass;5;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;True;17;d3d9;d3d11;glcore;gles;gles3;metal;vulkan;xbox360;xboxone;xboxseries;ps4;playstation;psp2;n3ds;wiiu;switch;nomrt;0;False;True;1;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;True;True;True;True;0;False;-1;False;False;False;False;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;False;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;0;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;454;2928.156,-79.93816;Float;False;True;-1;2;CommonGUInew;0;3;VFX/Pandavfx_v1.1;2992e84f91cbeb14eab234972e07ea9d;True;Forward;0;1;Forward;8;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;True;True;0;True;2;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;7;True;17;d3d9;d3d11;glcore;gles;gles3;metal;vulkan;xbox360;xboxone;xboxseries;ps4;playstation;psp2;n3ds;wiiu;switch;nomrt;0;True;True;1;5;True;3;10;True;4;0;1;False;-1;10;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;-1;False;False;False;False;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;True;2;True;421;True;3;True;123;True;False;0;False;-1;0;False;-1;True;1;LightMode=UniversalForward;False;False;0;;0;0;Standard;22;Surface;1;  Blend;0;Two Sided;1;Cast Shadows;0;  Use Shadow Threshold;0;Receive Shadows;0;GPU Instancing;0;LOD CrossFade;0;Built-in Fog;0;DOTS Instancing;0;Meta Pass;0;Extra Pre Pass;0;Tessellation;0;  Phong;0;  Strength;0.5,False,-1;  Type;0;  Tess;16,False,-1;  Min;10,False,-1;  Max;25,False,-1;  Edge Length;16,False,-1;  Max Displacement;25,False,-1;Vertex Position,InvertActionOnDeselection;1;0;5;False;True;False;True;False;False;;False;0
WireConnection;58;0;55;0
WireConnection;58;1;56;0
WireConnection;59;0;57;0
WireConnection;59;2;58;0
WireConnection;54;1;59;0
WireConnection;450;0;64;0
WireConnection;450;1;451;4
WireConnection;450;2;452;0
WireConnection;61;0;54;1
WireConnection;61;1;54;2
WireConnection;63;0;450;0
WireConnection;63;1;61;0
WireConnection;60;0;63;0
WireConnection;447;0;446;0
WireConnection;447;1;448;0
WireConnection;447;2;449;0
WireConnection;177;0;159;0
WireConnection;122;0;120;0
WireConnection;122;1;70;0
WireConnection;122;2;121;0
WireConnection;445;0;132;0
WireConnection;445;1;447;0
WireConnection;179;0;151;0
WireConnection;127;0;20;0
WireConnection;127;1;16;0
WireConnection;160;0;177;0
WireConnection;178;0;155;0
WireConnection;118;0;67;0
WireConnection;118;1;65;0
WireConnection;118;2;119;0
WireConnection;222;0;220;0
WireConnection;222;1;221;0
WireConnection;48;0;46;0
WireConnection;48;1;47;0
WireConnection;71;0;45;0
WireConnection;71;1;122;0
WireConnection;153;0;179;0
WireConnection;156;0;178;0
WireConnection;138;0;139;0
WireConnection;138;1;134;0
WireConnection;49;0;71;0
WireConnection;49;2;48;0
WireConnection;14;0;12;0
WireConnection;14;1;13;0
WireConnection;17;0;127;0
WireConnection;17;1;15;0
WireConnection;17;2;118;0
WireConnection;219;0;445;0
WireConnection;219;2;222;0
WireConnection;161;0;160;0
WireConnection;133;0;219;0
WireConnection;133;1;138;0
WireConnection;11;0;17;0
WireConnection;11;2;14;0
WireConnection;157;0;156;0
WireConnection;154;0;153;0
WireConnection;162;0;49;0
WireConnection;162;2;161;0
WireConnection;158;0;11;0
WireConnection;158;2;157;0
WireConnection;149;0;133;0
WireConnection;149;2;154;0
WireConnection;356;0;162;0
WireConnection;358;0;356;1
WireConnection;361;0;149;0
WireConnection;357;0;162;0
WireConnection;250;0;356;0
WireConnection;352;0;158;0
WireConnection;245;0;352;0
WireConnection;363;0;361;1
WireConnection;362;0;149;0
WireConnection;353;0;352;1
WireConnection;351;0;158;0
WireConnection;247;0;361;0
WireConnection;249;0;357;0
WireConnection;249;1;250;0
WireConnection;359;0;357;1
WireConnection;359;1;358;0
WireConnection;354;0;351;1
WireConnection;354;1;353;0
WireConnection;106;0;105;0
WireConnection;106;1;107;0
WireConnection;364;0;362;1
WireConnection;364;1;363;0
WireConnection;248;0;362;0
WireConnection;248;1;247;0
WireConnection;284;0;283;0
WireConnection;260;0;259;0
WireConnection;360;0;249;0
WireConnection;360;1;359;0
WireConnection;246;0;351;0
WireConnection;246;1;245;0
WireConnection;163;0;28;0
WireConnection;163;1;51;3
WireConnection;25;1;360;0
WireConnection;365;0;248;0
WireConnection;365;1;364;0
WireConnection;262;0;260;0
WireConnection;228;0;106;0
WireConnection;76;0;73;0
WireConnection;76;1;74;0
WireConnection;285;0;284;0
WireConnection;355;0;246;0
WireConnection;355;1;354;0
WireConnection;236;0;163;0
WireConnection;236;1;235;0
WireConnection;286;0;285;0
WireConnection;77;0;75;0
WireConnection;77;2;76;0
WireConnection;261;0;262;0
WireConnection;1;1;355;0
WireConnection;30;0;163;0
WireConnection;30;1;29;0
WireConnection;52;1;365;0
WireConnection;251;0;25;4
WireConnection;251;1;25;1
WireConnection;108;0;228;0
WireConnection;109;0;108;0
WireConnection;109;1;110;0
WireConnection;233;0;236;0
WireConnection;233;1;251;0
WireConnection;287;0;77;0
WireConnection;287;2;286;0
WireConnection;263;0;256;0
WireConnection;263;2;261;0
WireConnection;125;0;1;4
WireConnection;125;1;1;1
WireConnection;27;0;251;0
WireConnection;27;1;30;0
WireConnection;27;2;163;0
WireConnection;135;0;52;4
WireConnection;135;1;52;1
WireConnection;111;0;109;0
WireConnection;371;0;263;0
WireConnection;88;1;116;0
WireConnection;88;0;89;0
WireConnection;7;0;5;4
WireConnection;7;1;125;0
WireConnection;7;2;8;4
WireConnection;7;3;9;0
WireConnection;366;0;287;0
WireConnection;242;0;27;0
WireConnection;242;1;233;0
WireConnection;140;0;141;0
WireConnection;140;1;135;0
WireConnection;37;0;7;0
WireConnection;373;0;371;1
WireConnection;257;0;371;0
WireConnection;368;0;366;1
WireConnection;367;0;287;0
WireConnection;372;0;263;0
WireConnection;413;0;88;0
WireConnection;413;1;175;0
WireConnection;413;2;414;0
WireConnection;408;0;88;0
WireConnection;42;0;242;0
WireConnection;136;0;140;0
WireConnection;278;0;366;0
WireConnection;258;0;372;0
WireConnection;258;1;257;0
WireConnection;409;0;408;0
WireConnection;369;0;367;1
WireConnection;369;1;368;0
WireConnection;168;0;169;0
WireConnection;168;1;115;0
WireConnection;277;0;367;0
WireConnection;277;1;278;0
WireConnection;374;0;372;1
WireConnection;374;1;373;0
WireConnection;174;0;175;0
WireConnection;174;1;413;0
WireConnection;370;0;277;0
WireConnection;370;1;369;0
WireConnection;416;1;409;0
WireConnection;416;2;414;0
WireConnection;375;0;258;0
WireConnection;375;1;374;0
WireConnection;130;0;137;0
WireConnection;130;1;39;0
WireConnection;130;2;43;0
WireConnection;130;3;168;0
WireConnection;130;4;174;0
WireConnection;415;0;130;0
WireConnection;415;1;416;0
WireConnection;72;1;370;0
WireConnection;81;1;375;0
WireConnection;171;0;80;0
WireConnection;171;1;85;4
WireConnection;126;0;415;0
WireConnection;255;0;81;4
WireConnection;255;1;81;1
WireConnection;252;0;72;4
WireConnection;252;1;72;1
WireConnection;78;0;252;0
WireConnection;78;1;79;0
WireConnection;78;2;171;0
WireConnection;78;3;255;0
WireConnection;128;0;126;0
WireConnection;128;1;129;0
WireConnection;201;0;174;0
WireConnection;82;0;78;0
WireConnection;142;0;128;0
WireConnection;203;1;202;0
WireConnection;203;2;216;4
WireConnection;203;3;217;0
WireConnection;275;0;185;4
WireConnection;275;1;185;1
WireConnection;337;0;331;0
WireConnection;293;0;376;0
WireConnection;291;0;290;0
WireConnection;382;0;269;0
WireConnection;428;1;355;0
WireConnection;294;0;377;0
WireConnection;294;1;293;0
WireConnection;289;0;288;0
WireConnection;425;0;330;0
WireConnection;425;1;427;0
WireConnection;435;0;145;0
WireConnection;435;1;436;0
WireConnection;387;0;386;0
WireConnection;376;0;292;0
WireConnection;231;0;163;0
WireConnection;231;1;251;0
WireConnection;272;0;215;4
WireConnection;272;1;215;1
WireConnection;384;0;382;1
WireConnection;384;1;383;0
WireConnection;377;0;292;0
WireConnection;412;0;410;0
WireConnection;292;0;210;0
WireConnection;292;2;291;0
WireConnection;95;0;438;0
WireConnection;95;1;96;0
WireConnection;95;2;431;0
WireConnection;91;4;407;0
WireConnection;91;2;92;0
WireConnection;91;3;93;0
WireConnection;390;0;388;0
WireConnection;390;1;389;0
WireConnection;395;0;392;0
WireConnection;395;2;394;0
WireConnection;380;0;294;0
WireConnection;380;1;379;0
WireConnection;424;0;422;0
WireConnection;424;1;195;0
WireConnection;184;0;186;0
WireConnection;184;1;183;0
WireConnection;234;0;233;0
WireConnection;234;1;231;0
WireConnection;215;1;385;0
WireConnection;230;0;229;0
WireConnection;230;1;35;0
WireConnection;230;2;35;4
WireConnection;379;0;377;1
WireConnection;379;1;378;0
WireConnection;438;0;94;0
WireConnection;407;0;425;0
WireConnection;441;0;442;0
WireConnection;441;1;443;0
WireConnection;182;0;184;0
WireConnection;271;0;381;0
WireConnection;268;0;266;0
WireConnection;97;0;95;0
WireConnection;244;0;225;0
WireConnection;244;1;237;0
WireConnection;244;2;234;0
WireConnection;333;0;330;0
WireConnection;333;1;337;0
WireConnection;340;0;332;0
WireConnection;410;0;409;0
WireConnection;410;1;411;0
WireConnection;36;0;6;0
WireConnection;399;0;397;0
WireConnection;402;0;400;0
WireConnection;402;1;401;0
WireConnection;170;0;101;0
WireConnection;170;1;101;0
WireConnection;383;0;381;1
WireConnection;393;0;387;0
WireConnection;381;0;269;0
WireConnection;226;0;227;0
WireConnection;226;1;241;0
WireConnection;290;0;289;0
WireConnection;165;0;170;0
WireConnection;165;1;131;0
WireConnection;223;0;8;0
WireConnection;223;1;429;0
WireConnection;223;2;441;0
WireConnection;214;0;212;0
WireConnection;214;1;213;0
WireConnection;432;0;148;0
WireConnection;205;0;196;0
WireConnection;205;1;206;0
WireConnection;385;0;270;0
WireConnection;385;1;384;0
WireConnection;299;0;433;0
WireConnection;299;1;301;0
WireConnection;186;0;194;0
WireConnection;186;1;275;0
WireConnection;186;2;272;0
WireConnection;398;0;397;1
WireConnection;199;0;84;0
WireConnection;199;1;200;0
WireConnection;429;0;1;0
WireConnection;429;1;428;0
WireConnection;429;2;430;0
WireConnection;266;0;265;0
WireConnection;397;0;395;0
WireConnection;422;1;423;0
WireConnection;422;2;414;0
WireConnection;185;1;380;0
WireConnection;270;0;382;0
WireConnection;270;1;271;0
WireConnection;332;1;402;0
WireConnection;332;5;336;0
WireConnection;400;0;396;0
WireConnection;400;1;399;0
WireConnection;394;0;393;0
WireConnection;335;0;334;0
WireConnection;335;1;340;0
WireConnection;6;0;5;0
WireConnection;6;1;429;0
WireConnection;6;2;8;0
WireConnection;241;0;33;0
WireConnection;241;1;244;0
WireConnection;194;0;187;0
WireConnection;194;1;193;3
WireConnection;265;0;264;0
WireConnection;269;0;267;0
WireConnection;269;2;268;0
WireConnection;392;0;391;0
WireConnection;392;2;390;0
WireConnection;94;0;91;0
WireConnection;396;0;395;0
WireConnection;146;0;147;0
WireConnection;146;1;148;0
WireConnection;401;0;396;1
WireConnection;401;1;398;0
WireConnection;145;0;146;0
WireConnection;145;1;424;0
WireConnection;192;0;182;0
WireConnection;237;0;230;0
WireConnection;237;1;234;0
WireConnection;40;0;226;0
WireConnection;33;0;230;0
WireConnection;33;1;225;0
WireConnection;33;2;27;0
WireConnection;224;0;223;0
WireConnection;439;0;52;0
WireConnection;433;0;145;0
WireConnection;433;1;435;0
WireConnection;210;0;211;0
WireConnection;210;2;214;0
WireConnection;329;0;299;0
WireConnection;378;0;376;1
WireConnection;195;0;165;0
WireConnection;195;1;205;0
WireConnection;197;0;143;0
WireConnection;197;1;203;0
WireConnection;454;2;329;0
WireConnection;454;3;197;0
WireConnection;454;5;199;0
ASEEND*/
//CHKSM=512B5990D8F3C43316E3C81351A25CB7F2440B51