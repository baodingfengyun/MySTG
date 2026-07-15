// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33209,y:32712,varname:node_9361,prsc:2|emission-5708-OUT,alpha-1822-OUT;n:type:ShaderForge.SFN_Tex2d,id:8311,x:32586,y:32697,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_8311,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:6407,x:32562,y:32513,ptovrint:False,ptlb:color,ptin:_color,varname:node_6407,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_VertexColor,id:1893,x:32562,y:32878,varname:node_1893,prsc:2;n:type:ShaderForge.SFN_Multiply,id:5708,x:32982,y:32601,varname:node_5708,prsc:2|A-6407-RGB,B-8311-RGB,C-1893-RGB,D-4523-OUT,E-2654-RGB;n:type:ShaderForge.SFN_Tex2d,id:6377,x:31831,y:32570,ptovrint:False,ptlb:rongjietietu,ptin:_rongjietietu,varname:_Texture_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-3685-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3884,x:31844,y:32816,ptovrint:False,ptlb:soft_Value,ptin:_soft_Value,varname:node_3884,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:5621,x:32133,y:32631,varname:node_5621,prsc:2|A-6377-R,B-3884-OUT;n:type:ShaderForge.SFN_TexCoord,id:2329,x:31779,y:32907,varname:node_2329,prsc:2,uv:1,uaff:False;n:type:ShaderForge.SFN_Vector1,id:9393,x:31855,y:33058,varname:node_9393,prsc:2,v1:-1.5;n:type:ShaderForge.SFN_Lerp,id:80,x:32124,y:32918,varname:node_80,prsc:2|A-3884-OUT,B-9393-OUT,T-2329-U;n:type:ShaderForge.SFN_Subtract,id:159,x:32353,y:32901,varname:node_159,prsc:2|A-5621-OUT,B-80-OUT;n:type:ShaderForge.SFN_Clamp01,id:4523,x:32562,y:33118,varname:node_4523,prsc:2|IN-159-OUT;n:type:ShaderForge.SFN_Multiply,id:1822,x:32964,y:32898,varname:node_1822,prsc:2|A-6407-A,B-8311-A,C-1893-A,D-4523-OUT,E-2654-A;n:type:ShaderForge.SFN_Tex2d,id:2654,x:32597,y:32319,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_2654,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_TexCoord,id:7983,x:31436,y:32664,varname:node_7983,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ValueProperty,id:7224,x:31052,y:32355,ptovrint:False,ptlb:rju,ptin:_rju,varname:node_3544,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:9051,x:31030,y:32676,ptovrint:False,ptlb:rjv,ptin:_rjv,varname:node_3880,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Time,id:1635,x:31030,y:32457,varname:node_1635,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7144,x:31272,y:32321,varname:node_7144,prsc:2|A-7224-OUT,B-1635-T;n:type:ShaderForge.SFN_Multiply,id:7583,x:31288,y:32582,varname:node_7583,prsc:2|A-1635-T,B-9051-OUT;n:type:ShaderForge.SFN_Append,id:368,x:31469,y:32376,varname:node_368,prsc:2|A-7144-OUT,B-7583-OUT;n:type:ShaderForge.SFN_Add,id:3685,x:31654,y:32493,varname:node_3685,prsc:2|A-368-OUT,B-7983-UVOUT;proporder:8311-6407-6377-3884-2654-7224-9051;pass:END;sub:END;*/

Shader "ShiYue/LegacyParticleEffect/ShaderForge_Rongjie_blend" {
    Properties {
        _Texture ("Texture", 2D) = "white" {}
        [HDR]_color ("color", Color) = (0.5,0.5,0.5,1)
        _rongjietietu ("rongjietietu", 2D) = "white" {}
        _soft_Value ("soft_Value", Float ) = 0
        _Mask ("Mask", 2D) = "white" {}
        _rju ("rju", Float ) = 0
        _rjv ("rjv", Float ) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
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
            #define UNITY_PASS_FORWARDBASE
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl" 
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #pragma multi_compile_fog
            TEXTURE2D (_Texture);SAMPLER(sampler_Texture);
            TEXTURE2D (_rongjietietu);SAMPLER(sampler_rongjietietu);
            TEXTURE2D (_Mask);SAMPLER(sampler_Mask);
            
            CBUFFER_START(UnityPerMaterial)
            float4 _Texture_ST;
            half4 _color;
            float4 _rongjietietu_ST;
            float _soft_Value;
            float4 _Mask_ST;
            float _rju;
            float _rjv;
            CBUFFER_END
            
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                half4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
                half4 vertexColor : COLOR;
                float fogCoord : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.vertexColor = v.vertexColor;
                o.normalDir = TransformObjectToWorldNormal(v.normal);
                o.posWorld = mul(UNITY_MATRIX_M, v.vertex);
                o.pos = TransformObjectToHClip( v.vertex.xyz );
                o.fogCoord = ComputeFogFactor(o.pos.z);
                return o;
            }
            half4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
////// Lighting:
////// Emissive:
                float4 _Texture_var = SAMPLE_TEXTURE2D(_Texture,sampler_Texture,TRANSFORM_TEX(i.uv0, _Texture));
                float4 node_1635 = _Time;
                float2 node_3685 = (float2((_rju*node_1635.g),(node_1635.g*_rjv))+i.uv0);
                float4 _rongjietietu_var = SAMPLE_TEXTURE2D(_rongjietietu,sampler_rongjietietu,TRANSFORM_TEX(node_3685, _rongjietietu));
                float node_4523 = saturate(((_rongjietietu_var.r*_soft_Value)-lerp(_soft_Value,(-1.5),i.uv1.r)));
                float4 _Mask_var = SAMPLE_TEXTURE2D(_Mask,sampler_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                half3 emissive = (_color.rgb*_Texture_var.rgb*i.vertexColor.rgb*node_4523*_Mask_var.rgb);
                half3 finalColor = emissive;
                half4 finalRGBA = half4(finalColor,(_color.a*_Texture_var.a*i.vertexColor.a*node_4523*_Mask_var.a));
                finalRGBA.rgb = MixFog(finalRGBA.rgb,i.fogCoord);
                return finalRGBA;
            }
            ENDHLSL
        }
        /*Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl" 
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl" 
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = TransformObjectToHClip( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDHLSL
        }*/
    }
    FallBack "Hidden/Universal Render Pipeline/FallbackError"
    CustomEditor "ShaderForgeMaterialInspector"
}
