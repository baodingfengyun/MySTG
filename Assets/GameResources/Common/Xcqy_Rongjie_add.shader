// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33209,y:32712,varname:node_9361,prsc:2|emission-5708-OUT,alpha-1822-OUT;n:type:ShaderForge.SFN_Tex2d,id:8311,x:32562,y:32703,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_8311,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:6407,x:32562,y:32513,ptovrint:False,ptlb:color,ptin:_color,varname:node_6407,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_VertexColor,id:1893,x:32562,y:32878,varname:node_1893,prsc:2;n:type:ShaderForge.SFN_Multiply,id:5708,x:32982,y:32601,varname:node_5708,prsc:2|A-6407-RGB,B-8311-RGB,C-1893-RGB,D-4523-OUT,E-2654-RGB;n:type:ShaderForge.SFN_Tex2d,id:6377,x:31831,y:32570,ptovrint:False,ptlb:rongjietietu,ptin:_rongjietietu,varname:_Texture_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_ValueProperty,id:3884,x:31844,y:32816,ptovrint:False,ptlb:soft_Value,ptin:_soft_Value,varname:node_3884,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:5621,x:32133,y:32631,varname:node_5621,prsc:2|A-6377-R,B-3884-OUT;n:type:ShaderForge.SFN_TexCoord,id:2329,x:31779,y:32907,varname:node_2329,prsc:2,uv:1,uaff:False;n:type:ShaderForge.SFN_Vector1,id:9393,x:31855,y:33058,varname:node_9393,prsc:2,v1:-1.5;n:type:ShaderForge.SFN_Lerp,id:80,x:32124,y:32918,varname:node_80,prsc:2|A-3884-OUT,B-9393-OUT,T-2329-U;n:type:ShaderForge.SFN_Subtract,id:159,x:32353,y:32901,varname:node_159,prsc:2|A-5621-OUT,B-80-OUT;n:type:ShaderForge.SFN_Clamp01,id:4523,x:32562,y:33128,varname:node_4523,prsc:2|IN-159-OUT;n:type:ShaderForge.SFN_Multiply,id:1822,x:32964,y:32898,varname:node_1822,prsc:2|A-6407-A,B-8311-A,C-1893-A,D-4523-OUT,E-2654-A;n:type:ShaderForge.SFN_Tex2d,id:2654,x:32597,y:32319,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_2654,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;proporder:8311-6407-6377-3884-2654;pass:END;sub:END;*/

Shader "XianXia/Effect/ShaderForge_Rongjie_add" {
    Properties {
        _Texture ("Texture", 2D) = "white" {}
        [HDR]_color ("color", Color) = (0.5,0.5,0.5,1)
        _rongjietietu ("rongjietietu", 2D) = "white" {}
        _soft_Value ("soft_Value", Float ) = 0
        _Mask ("Mask", 2D) = "white" {}
        //[HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            //Name "FORWARD"
            //Tags {
            //    "LightMode"="ForwardBase"
            //}
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            //#define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            //#pragma multi_compile_fwdbase
            //#pragma multi_compile_fog
            //#pragma only_renderers d3d9 d3d11 glcore gles 
            //#pragma target 3.0
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float4 _color;
            uniform sampler2D _rongjietietu; uniform float4 _rongjietietu_ST;
            uniform float _soft_Value;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
                float4 uv1 : TEXCOORD1;
                float4 vertexColor : COLOR;
                //UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
				o.uv0.xy = TRANSFORM_TEX(v.texcoord0, _Texture);
				o.uv0.zw = TRANSFORM_TEX(v.texcoord0, _rongjietietu);
				o.uv1.xy = TRANSFORM_TEX(v.texcoord0, _Mask);
                o.uv1.zw = v.texcoord1;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                //UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
			float4 frag(VertexOutput i) : COLOR{
                float4 _Texture_var = tex2D(_Texture, i.uv0.xy);
                float4 _rongjietietu_var = tex2D(_rongjietietu, i.uv0.zw);
                float node_4523 = saturate(((_rongjietietu_var.r*_soft_Value)-lerp(_soft_Value,(-1.5), i.uv1.z)));
                float4 _Mask_var = tex2D(_Mask, i.uv1.xy);
                float3 emissive = (_color.rgb*_Texture_var.rgb*i.vertexColor.rgb*node_4523*_Mask_var.rgb);
                float4 finalRGBA = float4(emissive,(_color.a*_Texture_var.a*i.vertexColor.a*node_4523*_Mask_var.a));
                //UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    //FallBack "Diffuse"
    //CustomEditor "ShaderForgeMaterialInspector"
}
