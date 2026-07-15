Shader "ShiYue/ParticleEffect/UVRollBlend" {
    Properties {
        _AmitTex ("AmitTex", 2D) = "white" {}
        _Color ("Color", Color) = (0.5,0.5,0.5,1)
        _Brighteen ("Brighteen", Range(0, 10)) = 1
        _Uspeed ("U speed", Range(-10, 10)) = 1
        _VSpeed ("V Speed", Range(-10, 10)) = 1
        _Mask ("Mask", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "RenderPipeline" = "UniversalPipeline"
        }

        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="UniversalForward"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl" 
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            TEXTURE2D (_AmitTex);SAMPLER(sampler_AmitTex);
            TEXTURE2D (_Mask);SAMPLER(sampler_Mask);

            CBUFFER_START(UnityPerMaterial)
            float4 _AmitTex_ST;
            float4 _Mask_ST;
            float4 _Color;
            float _Brighteen;
            float _Uspeed;
            float _VSpeed;
            CBUFFER_END

            // Global
            float4 _TimeEditor;
            
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = TransformObjectToHClip(v.vertex.xyz );
                return o;
            }
            half4 frag(VertexOutput i) : COLOR {
                
                half4 node_9350 = _Time + _TimeEditor;
                half2 node_6726 = (i.uv0+(node_9350.g*float2(_Uspeed,_VSpeed)));
                half4 _AmitTex_var = SAMPLE_TEXTURE2D(_AmitTex,sampler_AmitTex,TRANSFORM_TEX(node_6726, _AmitTex));
                half4 _Mask_var = SAMPLE_TEXTURE2D(_Mask,sampler_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                half3 emissive = (_Brighteen*(_AmitTex_var.rgb*i.vertexColor.rgb*_Color.rgb));
                return half4(emissive,_AmitTex_var.a * i.vertexColor.a * _Color.a * _Mask_var.r);
            }
            ENDHLSL
        }
    }
}
