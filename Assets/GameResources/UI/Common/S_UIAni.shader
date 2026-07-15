// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Effect1/UIANI UI"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255

		_ColorMask ("Color Mask", Float) = 15

		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
		[Enum(On,1,Off,0)]_Float2("深度模式", Float) = 0
		[Enum(AlphaBlend,10,Additive,1)]_Dst1("混合模式", Float) = 10
		[Enum(UnityEngine.Rendering.CullMode)]_CullMode("开启双面", Float) = 2
		[HDR]_MainColor("第一层贴图颜色", Color) = (1,1,1,1)
		_Maintex("第一层贴图", 2D) = "white" {}
		_Tex1Rotator("第一层贴图旋转", Range( 0 , 1)) = 0
		_Maintex1("第一层颜色渐变图", 2D) = "white" {}
		_Tex1Rotator1("第一层贴图渐变旋转", Range( 0 , 1)) = 0
		_MainTex_U1("第一层贴图流动_U", Float) = 0
		_MainTex_V1("第一层贴图流动_V", Float) = 0
		[Toggle(_OPENTEXPASS_ON)] _OpenTexPass("使用R贴图通道（Alpha）", Float) = 0
		[Toggle(_USEPARTICLECUSTOM_ON)] _UseParticleCustom("使用粒子自定义属性XY", Float) = 0
		[Header(Mask Mode)]_TexMask1("遮罩1", 2D) = "white" {}
		_MaskRotator1("遮罩旋转", Range( 0 , 1)) = 0
		_MaskSpeedU1("遮罩流动速度U", Float) = 0
		_MaskSpeedV1("遮罩流动速度V", Float) = 0
		[Toggle(_USEMASKPARTICLECUSTOM_ON)] _UseMaskParticleCustom("使用粒子自定义属性ZW控制速度", Float) = 0
		_TexMask2("遮罩2", 2D) = "white" {}
		_MaskRotator2("遮罩旋转", Range( 0 , 1)) = 0
		_MaskSpeedU2("遮罩流动速度U", Float) = 0
		_MaskSpeedV2("遮罩流动速度V", Float) = 0
		[Header(Noise Mode)]_DisturbanceTex1("扰动贴图", 2D) = "white" {}
		_DisturbanceTexMask("扰动贴图遮罩", 2D) = "white" {}
		_DisturbanceScale("扰动强度", Float) = 0
		_DisturbanceTex_U("扰动流动_U", Float) = 0
		_DisturbanceTex_V("扰动流动_V", Float) = 0
		[Header(Dissslve Mode)]_DissolveTex1("溶解贴图", 2D) = "white" {}
		_DissolveRotator1("溶解贴图旋转", Range( 0 , 1)) = 0
		_DissolveTex_U2("溶解贴图流动_U", Float) = 0
		_DissolveTex_V2("溶解贴图流动_V", Float) = 0
		[Toggle]_UseRadialUV("开启极坐标", Float) = 0
		_DissolveIntensityCustom1z1("溶解强度", Range( 0 , 1)) = 0
		_SoftaDissolve1("软硬边强度", Range( 0 , 1)) = 0
		_DissolveWidth("溶解沟边宽度", Range( 0 , 1)) = 0.1
		[HDR]_DissolveWidthColor("溶解沟边颜色", Color) = (1,1,1,0)
		_DissolveDisturbance("溶解贴图受扰动的强度", Float) = 0
		[Toggle(_USEPARTICLEDISSOLVE_ON)] _UseParticleDissolve("开启粒子自定义属性2溶解X", Float) = 0
		[Toggle(_USEDISSOLVEMASK_ON)] _UseDissolveMask("开启溶解贴图遮罩", Float) = 0
		_DissolveMaskTex("溶解遮罩（需要开启上方开关才有作用）", 2D) = "white" {}
		[Toggle]_UseMaskRadialUV1("开启极坐标", Float) = 0
		[Toggle(_USEADD_ON)] _UseAdd("使用遮罩Add", Float) = 0
		_DissMaskValua("溶解遮罩强度(使用Add才起作用)", Range( 0 , 1)) = 0
		_DissolveMaskTex_U("溶解遮罩贴图流动_U", Float) = 0
		_DissolveMaskTex_V("溶解遮罩贴图流动_V", Float) = 0
		[Header(Depth   Fade)]_AlphaScale("Alpha强度", Float) = 1
		[Toggle(_OPENDEPTH_ON)] _OpenDepth("开启软粒子和摄像机衰减", Float) = 0
		_DepthFade1("DepthFade", Float) = 0.1
		_CamDepthFade("CamDepthFade", Float) = 1
		[HideInInspector]_DissolveMaskTex_ST("DissolveMaskTex_ST", Vector) = (0,0,0,0)
		[HideInInspector]_DissolveTex1_ST("DissolveTex1_ST", Vector) = (1,1,0,0)

	}

	SubShader
	{
		LOD 0

		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }
		
		


		Cull [_CullMode]
		Lighting Off
		ZWrite [_Float2]
		ZTest [unity_GUIZTestMode]
		Blend SrcAlpha [_Dst1], SrcAlpha [_Dst1]
		ColorMask [_ColorMask]

		
		Pass
		{
			Name "Default"
		CGPROGRAM
			
			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 4.5

			#include "UnityCG.cginc"
			#include "UnityUI.cginc"

			#pragma multi_compile __ UNITY_UI_CLIP_RECT
			#pragma multi_compile __ UNITY_UI_ALPHACLIP
			
			#include "UnityShaderVariables.cginc"
			#define ASE_NEEDS_FRAG_COLOR
			#define ASE_NEEDS_VERT_POSITION
			#pragma shader_feature_local _USEDISSOLVEMASK_ON
			#pragma shader_feature_local _USEADD_ON
			#pragma shader_feature_local _USEPARTICLEDISSOLVE_ON
			#pragma shader_feature_local _USEPARTICLECUSTOM_ON
			#pragma shader_feature_local _OPENTEXPASS_ON
			#pragma shader_feature_local _OPENDEPTH_ON
			#pragma shader_feature_local _USEMASKPARTICLECUSTOM_ON

			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
				float4 worldPosition : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord3 : TEXCOORD3;
			};
			
			uniform fixed4 _Color;
			uniform fixed4 _TextureSampleAdd;
			uniform float4 _ClipRect;
			uniform sampler2D _MainTex;
			uniform float _Dst1;
			uniform float _Float2;
			uniform float _CullMode;
			uniform half _SoftaDissolve1;
			uniform sampler2D _DissolveTex1;
			uniform half _DissolveTex_U2;
			uniform half _DissolveTex_V2;
			uniform float _UseRadialUV;
			uniform float4 _DissolveTex1_ST;
			sampler2D _Sampler60262;
			uniform sampler2D _DisturbanceTex1;
			uniform half _DisturbanceTex_U;
			uniform half _DisturbanceTex_V;
			uniform float4 _DisturbanceTex1_ST;
			uniform sampler2D _DisturbanceTexMask;
			uniform float4 _DisturbanceTexMask_ST;
			uniform float _DissolveDisturbance;
			uniform float _DissolveRotator1;
			uniform sampler2D _DissolveMaskTex;
			uniform half _DissolveMaskTex_U;
			uniform half _DissolveMaskTex_V;
			uniform float _UseMaskRadialUV1;
			uniform float4 _DissolveMaskTex_ST;
			sampler2D _Sampler60273;
			uniform float _DissMaskValua;
			uniform half _DissolveIntensityCustom1z1;
			uniform float _DissolveWidth;
			uniform float4 _DissolveWidthColor;
			uniform sampler2D _Maintex1;
			uniform float4 _Maintex1_ST;
			uniform float _Tex1Rotator1;
			uniform sampler2D _Maintex;
			uniform half _MainTex_U1;
			uniform half _MainTex_V1;
			uniform float4 _Maintex_ST;
			uniform float _Tex1Rotator;
			uniform float _DisturbanceScale;
			uniform half4 _MainColor;
			UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
			uniform float4 _CameraDepthTexture_TexelSize;
			uniform half _DepthFade1;
			uniform float _CamDepthFade;
			uniform float _AlphaScale;
			uniform sampler2D _TexMask1;
			uniform float4 _TexMask1_ST;
			uniform float _MaskRotator1;
			uniform half _MaskSpeedU1;
			uniform half _MaskSpeedV1;
			uniform sampler2D _TexMask2;
			uniform float4 _TexMask2_ST;
			uniform float _MaskRotator2;
			uniform half _MaskSpeedU2;
			uniform half _MaskSpeedV2;

			
			v2f vert( appdata_t IN  )
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID( IN );
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
				UNITY_TRANSFER_INSTANCE_ID(IN, OUT);
				OUT.worldPosition = IN.vertex;
				float4 ase_clipPos = UnityObjectToClipPos(IN.vertex);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				OUT.ase_texcoord2 = screenPos;
				float3 objectToViewPos = UnityObjectToViewPos(IN.vertex.xyz);
				float eyeDepth = -objectToViewPos.z;
				OUT.ase_texcoord3.x = eyeDepth;
				
				
				//setting value to unused interpolator channels and avoid initialization warnings
				OUT.ase_texcoord3.yzw = 0;
				
				OUT.worldPosition.xyz +=  float3( 0, 0, 0 ) ;
				OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

				OUT.texcoord = IN.texcoord;
				
				OUT.color = IN.color * _Color;
				return OUT;
			}

			fixed4 frag(v2f IN  ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				float temp_output_120_0 = ( 1.0 - _SoftaDissolve1 );
				float2 appendResult105 = (float2(_DissolveTex_U2 , _DissolveTex_V2));
				float2 uv_DissolveTex1 = IN.texcoord.xy * _DissolveTex1_ST.xy + _DissolveTex1_ST.zw;
				float2 temp_output_1_0_g3 = float2( 1,1 );
				float2 texCoord80_g3 = IN.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult10_g3 = (float2(( (temp_output_1_0_g3).x * texCoord80_g3.x ) , ( texCoord80_g3.y * (temp_output_1_0_g3).y )));
				float2 temp_output_11_0_g3 = float2( 0,0 );
				float2 texCoord81_g3 = IN.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner18_g3 = ( ( (temp_output_11_0_g3).x * _Time.y ) * float2( 1,0 ) + texCoord81_g3);
				float2 panner19_g3 = ( ( _Time.y * (temp_output_11_0_g3).y ) * float2( 0,1 ) + texCoord81_g3);
				float2 appendResult24_g3 = (float2((panner18_g3).x , (panner19_g3).y));
				float2 appendResult269 = (float2(_DissolveTex1_ST.x , _DissolveTex1_ST.y));
				float2 temp_output_47_0_g3 = appendResult105;
				float2 texCoord78_g3 = IN.texcoord.xy * float2( 2,2 ) + float2( 0,0 );
				float2 temp_output_31_0_g3 = ( texCoord78_g3 - float2( 1,1 ) );
				float2 appendResult39_g3 = (float2(frac( ( atan2( (temp_output_31_0_g3).x , (temp_output_31_0_g3).y ) / 6.28318548202515 ) ) , length( temp_output_31_0_g3 )));
				float2 panner54_g3 = ( ( (temp_output_47_0_g3).x * _Time.y ) * float2( 1,0 ) + appendResult39_g3);
				float2 panner55_g3 = ( ( _Time.y * (temp_output_47_0_g3).y ) * float2( 0,1 ) + appendResult39_g3);
				float2 appendResult58_g3 = (float2((panner54_g3).x , (panner55_g3).y));
				float2 appendResult44 = (float2(_DisturbanceTex_U , _DisturbanceTex_V));
				float2 uv_DisturbanceTex1 = IN.texcoord.xy * _DisturbanceTex1_ST.xy + _DisturbanceTex1_ST.zw;
				float4 tex2DNode48 = tex2D( _DisturbanceTex1, ( ( _Time.y * appendResult44 ) + uv_DisturbanceTex1 ) );
				float2 uv_DisturbanceTexMask = IN.texcoord.xy * _DisturbanceTexMask_ST.xy + _DisturbanceTexMask_ST.zw;
				float Disturbance66 = ( tex2DNode48.r * tex2D( _DisturbanceTexMask, uv_DisturbanceTexMask ).r );
				float2 temp_cast_0 = (Disturbance66).xx;
				float2 lerpResult126 = lerp( ( ( _Time.y * appendResult105 ) + (( _UseRadialUV )?( ( ( (tex2D( _Sampler60262, ( appendResult10_g3 + appendResult24_g3 ) )).rg * 1.0 ) + ( appendResult269 * appendResult58_g3 ) ) ):( uv_DissolveTex1 )) ) , temp_cast_0 , _DissolveDisturbance);
				float cos245 = cos( ( ( _DissolveRotator1 * UNITY_PI ) * 2.0 ) );
				float sin245 = sin( ( ( _DissolveRotator1 * UNITY_PI ) * 2.0 ) );
				float2 rotator245 = mul( lerpResult126 - float2( 0.5,0.5 ) , float2x2( cos245 , -sin245 , sin245 , cos245 )) + float2( 0.5,0.5 );
				float4 tex2DNode110 = tex2D( _DissolveTex1, rotator245 );
				float2 appendResult164 = (float2(_DissolveMaskTex_U , _DissolveMaskTex_V));
				float2 uv_DissolveMaskTex = IN.texcoord.xy * _DissolveMaskTex_ST.xy + _DissolveMaskTex_ST.zw;
				float2 temp_output_1_0_g2 = float2( 1,1 );
				float2 texCoord80_g2 = IN.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult10_g2 = (float2(( (temp_output_1_0_g2).x * texCoord80_g2.x ) , ( texCoord80_g2.y * (temp_output_1_0_g2).y )));
				float2 temp_output_11_0_g2 = float2( 0,0 );
				float2 texCoord81_g2 = IN.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner18_g2 = ( ( (temp_output_11_0_g2).x * _Time.y ) * float2( 1,0 ) + texCoord81_g2);
				float2 panner19_g2 = ( ( _Time.y * (temp_output_11_0_g2).y ) * float2( 0,1 ) + texCoord81_g2);
				float2 appendResult24_g2 = (float2((panner18_g2).x , (panner19_g2).y));
				float2 appendResult271 = (float2(_DissolveMaskTex_ST.x , _DissolveMaskTex_ST.y));
				float2 temp_output_47_0_g2 = appendResult164;
				float2 temp_output_31_0_g2 = ( ( float2( 2,2 ) * uv_DissolveMaskTex ) - float2( 1,1 ) );
				float2 appendResult39_g2 = (float2(frac( ( atan2( (temp_output_31_0_g2).x , (temp_output_31_0_g2).y ) / 6.28318548202515 ) ) , length( temp_output_31_0_g2 )));
				float2 panner54_g2 = ( ( (temp_output_47_0_g2).x * _Time.y ) * float2( 1,0 ) + appendResult39_g2);
				float2 panner55_g2 = ( ( _Time.y * (temp_output_47_0_g2).y ) * float2( 0,1 ) + appendResult39_g2);
				float2 appendResult58_g2 = (float2((panner54_g2).x , (panner55_g2).y));
				float2 panner161 = ( 1.0 * _Time.y * appendResult164 + (( _UseMaskRadialUV1 )?( ( ( (tex2D( _Sampler60273, ( appendResult10_g2 + appendResult24_g2 ) )).rg * 1.0 ) + ( appendResult271 * appendResult58_g2 ) ) ):( uv_DissolveMaskTex )));
				float4 tex2DNode169 = tex2D( _DissolveMaskTex, panner161 );
				#ifdef _USEADD_ON
				float staticSwitch258 = ( tex2DNode169.r * _DissMaskValua );
				#else
				float staticSwitch258 = tex2DNode169.r;
				#endif
				#ifdef _USEADD_ON
				float staticSwitch257 = ( tex2DNode110.r + staticSwitch258 );
				#else
				float staticSwitch257 = ( tex2DNode110.r * staticSwitch258 );
				#endif
				float temp_output_250_0 = saturate( staticSwitch257 );
				#ifdef _USEDISSOLVEMASK_ON
				float staticSwitch167 = temp_output_250_0;
				#else
				float staticSwitch167 = tex2DNode110.r;
				#endif
				float4 texCoord123 = IN.worldPosition.xyzw;
				texCoord123.xy = IN.worldPosition.xyzw.xy * float2( 1,1 ) + float2( 0,0 );
				#ifdef _USEPARTICLEDISSOLVE_ON
				float staticSwitch125 = texCoord123.z;
				#else
				float staticSwitch125 = _DissolveIntensityCustom1z1;
				#endif
				float clampResult118 = clamp( ( ( staticSwitch167 + 1.0 ) - ( staticSwitch125 * 2.0 ) ) , 0.0 , 1.0 );
				float smoothstepResult119 = smoothstep( 0.0 , temp_output_120_0 , clampResult118);
				float smoothstepResult144 = smoothstep( 0.0 , ( temp_output_120_0 + _DissolveWidth ) , clampResult118);
				float4 DissolveWidth148 = ( ( smoothstepResult119 - smoothstepResult144 ) * _DissolveWidthColor );
				float2 uv_Maintex1 = IN.texcoord.xy * _Maintex1_ST.xy + _Maintex1_ST.zw;
				float cos182 = cos( ( ( _Tex1Rotator1 * UNITY_PI ) * 2.0 ) );
				float sin182 = sin( ( ( _Tex1Rotator1 * UNITY_PI ) * 2.0 ) );
				float2 rotator182 = mul( uv_Maintex1 - float2( 0.5,0.5 ) , float2x2( cos182 , -sin182 , sin182 , cos182 )) + float2( 0.5,0.5 );
				float2 appendResult134 = (float2(_MainTex_U1 , _MainTex_V1));
				float4 texCoord55 = float4(IN.texcoord.xy,0,0);
				texCoord55.xy = float4(IN.texcoord.xy,0,0).xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult56 = (float2(texCoord55.z , texCoord55.w));
				#ifdef _USEPARTICLECUSTOM_ON
				float2 staticSwitch63 = appendResult56;
				#else
				float2 staticSwitch63 = ( _Time.y * appendResult134 );
				#endif
				float2 uv_Maintex = IN.texcoord.xy * _Maintex_ST.xy + _Maintex_ST.zw;
				float cos101 = cos( ( ( _Tex1Rotator * UNITY_PI ) * 2.0 ) );
				float sin101 = sin( ( ( _Tex1Rotator * UNITY_PI ) * 2.0 ) );
				float2 rotator101 = mul( uv_Maintex - float2( 0.5,0.5 ) , float2x2( cos101 , -sin101 , sin101 , cos101 )) + float2( 0.5,0.5 );
				float4 tex2DNode5 = tex2D( _Maintex, ( staticSwitch63 + rotator101 + ( Disturbance66 * _DisturbanceScale ) ) );
				#ifdef _OPENTEXPASS_ON
				float staticSwitch38 = tex2DNode5.r;
				#else
				float staticSwitch38 = tex2DNode5.a;
				#endif
				float4 screenPos = IN.ase_texcoord2;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float screenDepth18 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
				float distanceDepth18 = abs( ( screenDepth18 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _DepthFade1 ) );
				float eyeDepth = IN.ase_texcoord3.x;
				float cameraDepthFade22 = (( eyeDepth -_ProjectionParams.y - 0.0 ) / _CamDepthFade);
				#ifdef _OPENDEPTH_ON
				float staticSwitch27 = ( distanceDepth18 * cameraDepthFade22 );
				#else
				float staticSwitch27 = 1.0;
				#endif
				float2 uv_TexMask1 = IN.texcoord.xy * _TexMask1_ST.xy + _TexMask1_ST.zw;
				float cos84 = cos( ( ( _MaskRotator1 * 2.0 ) * UNITY_PI ) );
				float sin84 = sin( ( ( _MaskRotator1 * 2.0 ) * UNITY_PI ) );
				float2 rotator84 = mul( uv_TexMask1 - float2( 0.5,0.5 ) , float2x2( cos84 , -sin84 , sin84 , cos84 )) + float2( 0.5,0.5 );
				float2 appendResult77 = (float2(_MaskSpeedU1 , _MaskSpeedV1));
				float4 texCoord87 = IN.worldPosition.xyzw;
				texCoord87.xy = IN.worldPosition.xyzw.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult95 = (float2(texCoord87.x , texCoord87.y));
				#ifdef _USEMASKPARTICLECUSTOM_ON
				float2 staticSwitch83 = appendResult95;
				#else
				float2 staticSwitch83 = ( appendResult77 * _Time.y );
				#endif
				float2 uv_TexMask2 = IN.texcoord.xy * _TexMask2_ST.xy + _TexMask2_ST.zw;
				float cos235 = cos( ( ( _MaskRotator2 * UNITY_PI ) * 2.0 ) );
				float sin235 = sin( ( ( _MaskRotator2 * UNITY_PI ) * 2.0 ) );
				float2 rotator235 = mul( uv_TexMask2 - float2( 0.5,0.5 ) , float2x2( cos235 , -sin235 , sin235 , cos235 )) + float2( 0.5,0.5 );
				float2 appendResult227 = (float2(_MaskSpeedU2 , _MaskSpeedV2));
				float Mask240 = tex2D( _TexMask2, ( rotator235 + ( appendResult227 * _Time.y ) ) ).r;
				float Mask90 = ( tex2D( _TexMask1, ( rotator84 + staticSwitch83 ) ).r * Mask240 );
				float Dissolve137 = smoothstepResult119;
				float4 appendResult290 = (float4((( DissolveWidth148 + float4( ( ( (tex2D( _Maintex1, rotator182 )).rgb * (tex2DNode5).rgb ) * (_MainColor).rgb * (IN.color).rgb ) , 0.0 ) )).rgb , saturate( ( staticSwitch38 * _MainColor.a * IN.color.a * saturate( staticSwitch27 ) * _AlphaScale * Mask90 * Dissolve137 ) )));
				
				half4 color = appendResult290;
				
				#ifdef UNITY_UI_CLIP_RECT
                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif
				
				#ifdef UNITY_UI_ALPHACLIP
				clip (color.a - 0.001);
				#endif

				return color;
			}
		ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18912
138;56;2284;1314;510.8121;769.0848;1;True;True
Node;AmplifyShaderEditor.CommentaryNode;39;-4087.059,-1828.11;Inherit;False;1856.753;851.4685;UV流动组件;13;43;66;92;48;47;45;46;44;42;41;175;176;177;扰动贴图;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;42;-3912.554,-1683.214;Half;False;Property;_DisturbanceTex_U;扰动流动_U;26;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;41;-3906.387,-1593.412;Half;False;Property;_DisturbanceTex_V;扰动流动_V;27;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;43;-3762.953,-1698.131;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;44;-3739.73,-1623.012;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;45;-3617.199,-1534.354;Inherit;False;0;48;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;46;-3607.986,-1646.693;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;153;-5969.986,-872.4254;Inherit;False;3805.357;1608.116;Comment;48;248;158;169;161;164;162;163;246;148;137;150;138;152;119;144;118;146;147;120;113;114;121;111;125;167;112;116;123;110;245;244;126;243;128;129;109;107;242;105;106;103;104;250;251;252;257;258;275;溶解贴图;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector4Node;272;-7091.438,-572.6039;Inherit;False;Property;_DissolveMaskTex_ST;DissolveMaskTex_ST;50;1;[HideInInspector];Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;268;-6239.23,-1294.725;Inherit;False;Property;_DissolveTex1_ST;DissolveTex1_ST;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;1,1,0,0;0.2,0.1,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;163;-5562.481,-8.262547;Half;False;Property;_DissolveMaskTex_V;溶解遮罩贴图流动_V;45;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;104;-5745.521,-724.3585;Half;False;Property;_DissolveTex_V2;溶解贴图流动_V;31;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;47;-3370.203,-1656.658;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;103;-5745.521,-788.3585;Half;False;Property;_DissolveTex_U2;溶解贴图流动_U;30;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;162;-5555.64,-86.36958;Half;False;Property;_DissolveMaskTex_U;溶解遮罩贴图流动_U;44;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;160;-6677.672,-258.6844;Inherit;False;0;169;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;177;-3430.462,-1363.607;Inherit;False;0;175;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;269;-6005.23,-1262.725;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;164;-5354.639,-35.36955;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;274;-6649.836,-419.2036;Inherit;False;2;2;0;FLOAT2;2,2;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;48;-3162.08,-1686.093;Inherit;True;Property;_DisturbanceTex1;扰动贴图;22;0;Create;False;0;0;0;False;1;Header(Noise Mode);False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;271;-6851.038,-543.8036;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;105;-5569.52,-740.3585;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;175;-3139.754,-1414.708;Inherit;True;Property;_DisturbanceTexMask;扰动贴图遮罩;24;0;Create;False;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;273;-6480.639,-634.205;Inherit;False;RadialUVDistortion;-1;;2;051d65e7699b41a4c800363fd0e822b2;0;7;60;SAMPLER2D;_Sampler60273;False;1;FLOAT2;1,1;False;11;FLOAT2;0,0;False;65;FLOAT;1;False;68;FLOAT2;1,1;False;47;FLOAT2;1,1;False;29;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;262;-5626.831,-1333.926;Inherit;False;RadialUVDistortion;-1;;3;051d65e7699b41a4c800363fd0e822b2;0;7;60;SAMPLER2D;_Sampler60262;False;1;FLOAT2;1,1;False;11;FLOAT2;0,0;False;65;FLOAT;1;False;68;FLOAT2;1,1;False;47;FLOAT2;1,1;False;29;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleTimeNode;106;-5569.52,-820.3585;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;176;-2558.336,-1655.451;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;108;-6463.817,-1020.158;Inherit;False;0;110;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;66;-2403.187,-1662.075;Inherit;False;Disturbance;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;275;-5891.992,-251.9908;Inherit;False;Property;_UseMaskRadialUV1;开启极坐标;41;0;Create;False;0;0;0;False;0;False;0;True;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ToggleSwitchNode;270;-5220.401,-1122.666;Inherit;False;Property;_UseRadialUV;开启极坐标;32;0;Create;False;0;0;0;False;0;False;0;True;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;242;-5549.663,-437.4594;Inherit;False;Property;_DissolveRotator1;溶解贴图旋转;29;0;Create;False;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;107;-5393.52,-772.3585;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;128;-5317.812,-656.897;Inherit;False;66;Disturbance;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;161;-5202.973,-237.0364;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;129;-5336.845,-539.9528;Inherit;False;Property;_DissolveDisturbance;溶解贴图受扰动的强度;37;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;109;-5265.521,-756.3585;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PiNode;243;-5265.663,-433.4594;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;126;-5113.315,-762.1757;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;169;-5006.558,-267.0937;Inherit;True;Property;_DissolveMaskTex;溶解遮罩（需要开启上方开关才有作用）;40;0;Create;False;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;244;-5077.663,-433.4594;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;252;-4887.937,-42.47904;Inherit;False;Property;_DissMaskValua;溶解遮罩强度(使用Add才起作用);43;0;Create;False;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;251;-4676.843,-168.6205;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;245;-4901.141,-763.5923;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StaticSwitch;258;-4540.405,-240.7013;Inherit;False;Property;_UseAdd2;使用遮罩Add;42;0;Create;False;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Reference;257;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;110;-4677.583,-792.5199;Inherit;True;Property;_DissolveTex1;溶解贴图;28;0;Create;False;0;0;0;False;1;Header(Dissslve Mode);False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;158;-4361.391,-676.2952;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;248;-4395.099,-578.3658;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;223;-1556.513,2137.485;Inherit;False;1868.652;673.9489;Comment;13;240;238;237;235;234;232;231;230;229;227;226;225;224;Mask2;1,1,1,1;0;0
Node;AmplifyShaderEditor.StaticSwitch;257;-4185.149,-673.1864;Inherit;False;Property;_UseAdd;使用遮罩Add;42;0;Create;False;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;88;-1587.254,1380.123;Inherit;False;1868.652;673.9489;Comment;16;87;86;85;84;83;82;81;80;79;78;77;76;75;74;95;241;Mask1;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;226;-1506.513,2368.199;Inherit;False;Property;_MaskRotator2;遮罩旋转;19;0;Create;False;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;250;-4004.754,-665.8798;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;116;-4286.959,-341.3465;Half;False;Property;_DissolveIntensityCustom1z1;溶解强度;33;0;Create;False;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;123;-4207.393,-258.9806;Inherit;False;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;135;-1790.225,-724.6868;Inherit;False;878.5349;481.4579;UV流动组件;8;56;55;131;132;134;130;133;63;第一层贴图;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;131;-1785.225,-436.0291;Half;False;Property;_MainTex_U1;第一层贴图流动_U;9;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;98;-1813.842,417.6943;Inherit;False;Property;_Tex1Rotator;第一层贴图旋转;6;0;Create;False;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;125;-3989.393,-343.9805;Inherit;False;Property;_UseParticleDissolve;开启粒子自定义属性2溶解X;38;0;Create;False;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;132;-1784.058,-364.2289;Half;False;Property;_MainTex_V1;第一层贴图流动_V;10;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;112;-3815.124,-605.3485;Half;False;Constant;_Float1;Float 1;26;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;75;-1560.083,1789.852;Half;False;Property;_MaskSpeedV1;遮罩流动速度V;16;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;74;-1561.25,1716.052;Half;False;Property;_MaskSpeedU1;遮罩流动速度U;15;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;76;-1537.254,1610.837;Inherit;False;Property;_MaskRotator1;遮罩旋转;14;0;Create;False;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;224;-1529.342,2547.214;Half;False;Property;_MaskSpeedV2;遮罩流动速度V;21;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PiNode;230;-1222.513,2372.199;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;225;-1530.509,2473.414;Half;False;Property;_MaskSpeedU2;遮罩流动速度U;20;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;167;-3885.456,-767.6999;Inherit;False;Property;_UseDissolveMask;开启溶解贴图遮罩;39;0;Create;False;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;121;-3381.551,-428.7367;Half;False;Property;_SoftaDissolve1;软硬边强度;34;0;Create;False;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;111;-3640.124,-762.3485;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;136;-1630.133,-231.2662;Inherit;False;401.3949;247.7871;扰动强度;3;94;93;73;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;178;-822.4338,-510.3116;Inherit;False;Property;_Tex1Rotator1;第一层贴图渐变旋转;8;0;Create;False;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PiNode;99;-1529.842,421.6943;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;55;-1553.236,-674.6868;Inherit;False;0;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;134;-1569.399,-385.829;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;227;-1333.686,2515.613;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;231;-1034.514,2372.199;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;229;-1371.486,2626.225;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;78;-1402.227,1868.863;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;82;-1252.255,1613.837;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;232;-1228.868,2196.315;Inherit;False;0;238;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;133;-1586.657,-456.7084;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;77;-1364.427,1758.251;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;87;-1228.009,1891.837;Inherit;False;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;114;-3547.721,-390.3486;Inherit;False;2;2;0;FLOAT;2;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;130;-1420.657,-409.5088;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.OneMinusNode;120;-3227.987,-518.8805;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;94;-1540.687,-99.47916;Inherit;False;Property;_DisturbanceScale;扰动强度;25;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PiNode;179;-538.4338,-506.3116;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;100;-1341.842,421.6943;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;73;-1580.133,-181.1074;Inherit;False;66;Disturbance;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;25;-1536.218,42.48825;Inherit;False;0;5;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;147;-3386.35,-282.3468;Inherit;False;Property;_DissolveWidth;溶解沟边宽度;35;0;Create;False;0;0;0;False;0;False;0.1;0.1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;235;-893.5143,2205.199;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;56;-1340.236,-600.6868;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;80;-1183.683,1757.572;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PiNode;79;-1100.254,1616.837;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;81;-1260.909,1448.054;Inherit;False;0;86;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;113;-3439.84,-762.4359;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;95;-1007.88,1915.521;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;234;-1152.942,2514.934;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;29;-1433.938,655.4794;Inherit;False;1091;378.9999;开启虚化效果;8;27;23;28;18;22;21;17;30;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleAddOpNode;146;-3087.35,-338.3468;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;180;-677.1411,-750.8442;Inherit;False;0;172;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;63;-1206.69,-441.7592;Inherit;False;Property;_UseParticleCustom;使用粒子自定义属性XY;12;0;Create;False;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RotatorNode;101;-1214.842,49.69434;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;181;-350.434,-506.3116;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;237;-670.4181,2202.074;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;93;-1392.738,-175.2662;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;83;-951.809,1750.863;Inherit;False;Property;_UseMaskParticleCustom;使用粒子自定义属性ZW控制速度;17;0;Create;False;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RotatorNode;84;-924.255,1447.837;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ClampOpNode;118;-3278.673,-759.6965;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;53;-917.624,28.91605;Inherit;False;3;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-1351.954,827.9194;Half;False;Property;_DepthFade1;DepthFade;48;0;Create;False;0;0;0;False;0;False;0.1;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;85;-701.1588,1444.712;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SmoothstepOpNode;119;-3000.208,-755.0635;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;182;-251.4339,-744.3116;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SmoothstepOpNode;144;-2971.35,-387.3469;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-1369.938,936.4789;Inherit;False;Property;_CamDepthFade;CamDepthFade;49;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;238;-390.3609,2172.884;Inherit;True;Property;_TexMask2;遮罩2;18;0;Create;False;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;5;-777.5,0.5;Inherit;True;Property;_Maintex;第一层贴图;5;0;Create;False;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;152;-2721.128,-370.9873;Inherit;False;Property;_DissolveWidthColor;溶解沟边颜色;36;1;[HDR];Create;False;0;0;0;False;0;False;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;172;-810.899,-210.2883;Inherit;True;Property;_Maintex1;第一层颜色渐变图;7;0;Create;False;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;138;-2751.35,-610.3466;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;240;-7.606096,2202.323;Inherit;False;Mask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;18;-1180.222,808.5375;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;86;-421.4017,1415.522;Inherit;True;Property;_TexMask1;遮罩1;13;0;Create;False;0;0;0;False;1;Header(Mask Mode);False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CameraDepthFade;22;-1160.939,910.479;Inherit;False;3;2;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;10;-853.5139,475.1183;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;171;-501.899,-2.2883;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ComponentMaskNode;174;-503.899,-208.2883;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;7;-1043.43,298.6281;Half;False;Property;_MainColor;第一层贴图颜色;4;1;[HDR];Create;False;0;0;0;False;0;False;1,1,1,1;8,8,8,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;150;-2543.128,-608.9872;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;241;156.6771,1444.807;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;28;-949.9382,710.4794;Inherit;False;Constant;_Float0;Float 0;7;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-912.9382,803.4794;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;137;-2744.35,-761.3466;Inherit;False;Dissolve;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;27;-798.9382,719.4794;Inherit;False;Property;_OpenDepth;开启软粒子和摄像机衰减;47;0;Create;False;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;90;344.0531,1441.761;Inherit;False;Mask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;173;-279.899,-23.2883;Inherit;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ComponentMaskNode;8;-756.3556,305.965;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ComponentMaskNode;11;-676.901,469.5051;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;148;-2374.445,-614.186;Inherit;False;DissolveWidth;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;155;-93.30469,-194.7664;Inherit;False;148;DissolveWidth;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;154;-209.1047,815.2336;Inherit;False;137;Dissolve;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-66.3,-23.8;Inherit;True;3;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;97;-248.0904,674.0714;Inherit;False;90;Mask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;51.37416,520.4098;Inherit;False;Property;_AlphaScale;Alpha强度;46;0;Create;False;0;0;0;False;1;Header(Depth   Fade);False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;38;-366.1175,358.8899;Inherit;False;Property;_OpenTexPass;使用R贴图通道（Alpha）;11;0;Create;False;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;30;-583.1414,747.8208;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-113.2,430.1999;Inherit;False;7;7;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;157;158.6741,-51.02115;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ComponentMaskNode;289;416.1879,-5.084839;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SaturateNode;24;50.04063,425.3167;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;287;406.7224,306.3164;Inherit;False;Constant;_Float3;Float 3;53;0;Create;True;0;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;282;-3779.213,-955.8679;Inherit;False;Property;_Keyword0;Keyword 0;52;0;Create;True;0;0;0;False;0;False;0;0;0;True;;KeywordEnum;3;Key0;Key1;Key2;Create;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;279;-4666.854,-1171.052;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;284;-4353.213,-1131.868;Inherit;True;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;14;279.8548,-222.0909;Inherit;False;Property;_CullMode;开启双面;3;1;[Enum];Create;False;0;0;1;UnityEngine.Rendering.CullMode;True;0;False;2;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;92;-2775.726,-1661.312;Inherit;False;Property;_DisturbanceRGB;扰动贴图RGB通道;23;0;Create;False;0;0;0;False;0;False;0;0;0;True;;KeywordEnum;3;R;G;B;Create;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;286;667.4563,-399.2176;Inherit;False;Property;_Float2;深度模式;1;1;[Enum];Create;False;0;2;On;1;Off;0;0;True;0;False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;15;281.1729,-306.0909;Inherit;False;Property;_Dst1;混合模式;2;1;[Enum];Create;False;0;2;AlphaBlend;10;Additive;1;0;True;0;False;10;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;283;-4887.213,-1102.868;Inherit;False;Constant;_Vector2;Vector 2;52;0;Create;True;0;0;0;False;0;False;2,2;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.ScaleAndOffsetNode;278;-5844.014,-1279.133;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;266;-6197.628,-1128.525;Inherit;False;2;2;0;FLOAT2;2,2;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;246;-3633.349,-192.3276;Inherit;False;3;0;FLOAT;0.48;False;1;FLOAT;1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;285;-4578.213,-1013.868;Inherit;False;Constant;_Vector3;Vector 3;52;0;Create;True;0;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.DynamicAppendNode;290;695.1879,-14.08484;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;288;904.2395,-12.7527;Float;False;True;-1;2;ASEMaterialInspector;0;7;Effect1/UIANI UI;5056123faa0c79b47ab6ad7e8bf059a4;True;Default;0;0;Default;2;True;True;2;5;False;-1;0;True;15;2;5;False;-1;10;True;15;False;False;False;False;False;False;False;False;False;False;False;True;True;2;True;14;False;True;True;True;True;True;0;True;-9;False;False;False;False;False;False;False;True;False;0;True;-5;255;True;-8;255;True;-7;0;True;-4;0;True;-6;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;True;2;True;286;True;0;True;-11;False;True;5;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;CanUseSpriteAtlas=True;False;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;5;False;0;;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;44;0;42;0
WireConnection;44;1;41;0
WireConnection;46;0;43;0
WireConnection;46;1;44;0
WireConnection;47;0;46;0
WireConnection;47;1;45;0
WireConnection;269;0;268;1
WireConnection;269;1;268;2
WireConnection;164;0;162;0
WireConnection;164;1;163;0
WireConnection;274;1;160;0
WireConnection;48;1;47;0
WireConnection;271;0;272;1
WireConnection;271;1;272;2
WireConnection;105;0;103;0
WireConnection;105;1;104;0
WireConnection;175;1;177;0
WireConnection;273;68;271;0
WireConnection;273;47;164;0
WireConnection;273;29;274;0
WireConnection;262;68;269;0
WireConnection;262;47;105;0
WireConnection;176;0;48;1
WireConnection;176;1;175;1
WireConnection;66;0;176;0
WireConnection;275;0;160;0
WireConnection;275;1;273;0
WireConnection;270;0;108;0
WireConnection;270;1;262;0
WireConnection;107;0;106;0
WireConnection;107;1;105;0
WireConnection;161;0;275;0
WireConnection;161;2;164;0
WireConnection;109;0;107;0
WireConnection;109;1;270;0
WireConnection;243;0;242;0
WireConnection;126;0;109;0
WireConnection;126;1;128;0
WireConnection;126;2;129;0
WireConnection;169;1;161;0
WireConnection;244;0;243;0
WireConnection;251;0;169;1
WireConnection;251;1;252;0
WireConnection;245;0;126;0
WireConnection;245;2;244;0
WireConnection;258;1;169;1
WireConnection;258;0;251;0
WireConnection;110;1;245;0
WireConnection;158;0;110;1
WireConnection;158;1;258;0
WireConnection;248;0;110;1
WireConnection;248;1;258;0
WireConnection;257;1;158;0
WireConnection;257;0;248;0
WireConnection;250;0;257;0
WireConnection;125;1;116;0
WireConnection;125;0;123;3
WireConnection;230;0;226;0
WireConnection;167;1;110;1
WireConnection;167;0;250;0
WireConnection;111;0;167;0
WireConnection;111;1;112;0
WireConnection;99;0;98;0
WireConnection;134;0;131;0
WireConnection;134;1;132;0
WireConnection;227;0;225;0
WireConnection;227;1;224;0
WireConnection;231;0;230;0
WireConnection;82;0;76;0
WireConnection;77;0;74;0
WireConnection;77;1;75;0
WireConnection;114;0;125;0
WireConnection;130;0;133;0
WireConnection;130;1;134;0
WireConnection;120;0;121;0
WireConnection;179;0;178;0
WireConnection;100;0;99;0
WireConnection;235;0;232;0
WireConnection;235;2;231;0
WireConnection;56;0;55;3
WireConnection;56;1;55;4
WireConnection;80;0;77;0
WireConnection;80;1;78;0
WireConnection;79;0;82;0
WireConnection;113;0;111;0
WireConnection;113;1;114;0
WireConnection;95;0;87;1
WireConnection;95;1;87;2
WireConnection;234;0;227;0
WireConnection;234;1;229;0
WireConnection;146;0;120;0
WireConnection;146;1;147;0
WireConnection;63;1;130;0
WireConnection;63;0;56;0
WireConnection;101;0;25;0
WireConnection;101;2;100;0
WireConnection;181;0;179;0
WireConnection;237;0;235;0
WireConnection;237;1;234;0
WireConnection;93;0;73;0
WireConnection;93;1;94;0
WireConnection;83;1;80;0
WireConnection;83;0;95;0
WireConnection;84;0;81;0
WireConnection;84;2;79;0
WireConnection;118;0;113;0
WireConnection;53;0;63;0
WireConnection;53;1;101;0
WireConnection;53;2;93;0
WireConnection;85;0;84;0
WireConnection;85;1;83;0
WireConnection;119;0;118;0
WireConnection;119;2;120;0
WireConnection;182;0;180;0
WireConnection;182;2;181;0
WireConnection;144;0;118;0
WireConnection;144;2;146;0
WireConnection;238;1;237;0
WireConnection;5;1;53;0
WireConnection;172;1;182;0
WireConnection;138;0;119;0
WireConnection;138;1;144;0
WireConnection;240;0;238;1
WireConnection;18;0;17;0
WireConnection;86;1;85;0
WireConnection;22;0;21;0
WireConnection;171;0;5;0
WireConnection;174;0;172;0
WireConnection;150;0;138;0
WireConnection;150;1;152;0
WireConnection;241;0;86;1
WireConnection;241;1;240;0
WireConnection;23;0;18;0
WireConnection;23;1;22;0
WireConnection;137;0;119;0
WireConnection;27;1;28;0
WireConnection;27;0;23;0
WireConnection;90;0;241;0
WireConnection;173;0;174;0
WireConnection;173;1;171;0
WireConnection;8;0;7;0
WireConnection;11;0;10;0
WireConnection;148;0;150;0
WireConnection;12;0;173;0
WireConnection;12;1;8;0
WireConnection;12;2;11;0
WireConnection;38;1;5;4
WireConnection;38;0;5;1
WireConnection;30;0;27;0
WireConnection;13;0;38;0
WireConnection;13;1;7;4
WireConnection;13;2;10;4
WireConnection;13;3;30;0
WireConnection;13;4;31;0
WireConnection;13;5;97;0
WireConnection;13;6;154;0
WireConnection;157;0;155;0
WireConnection;157;1;12;0
WireConnection;289;0;157;0
WireConnection;24;0;13;0
WireConnection;282;1;110;1
WireConnection;282;0;250;0
WireConnection;279;0;283;0
WireConnection;284;0;279;0
WireConnection;284;1;285;0
WireConnection;92;1;48;1
WireConnection;92;0;48;2
WireConnection;92;2;48;3
WireConnection;278;1;269;0
WireConnection;266;1;108;0
WireConnection;246;2;125;0
WireConnection;290;0;289;0
WireConnection;290;3;24;0
WireConnection;288;0;290;0
ASEEND*/
//CHKSM=0D21EDF92D8F2E18E0F62D08316F70CA305A3AC9