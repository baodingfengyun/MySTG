Shader "ShiYue/ParticleEffect/DissolveBlend"
{
    Properties
    {
        [Header(Base)]
        [Toggle]_Tip1("提示：Custom.X和Custom.Y可控制主帖图的移动", Float) = 0
        _MainTex("主贴图", 2D) = "white" {}
        [HDR]_Color("主颜色", Color) = (1, 1, 1 ,1)
        _MainTexFlowX("主贴图X轴流动速度", Float) = 0
        _MainTexFlowY("主贴图Y轴流动速度", Float) = 0
        
        [Header(Effect)]
        [Toggle]_Tip2("提示：Custom.Z和Custom.W可控制溶解位置或整体溶解强度", Float) = 0
        _Tip3("           （需将下面的X、Y溶解位置数值都改为0）", Float) = 0
        _DissolutionTex("溶解贴图", 2D) = "white" {}
        _DissolutionSoftness("边缘柔软度", Float) = 0
        _DissolutionTexFlowX("X轴流动速度", Float) = 0
        _DissolutionTexFlowY("Y轴流动速度", Float) = 0
        
        [Enum(Directional, 0, Whole, 1)]_DissolutionMode("溶解模式", Float) = 0
        [Enum(Forward, 0, Inverse, 1)]_DissolutionDirX("X轴溶解方向", Float) = 0
        [Enum(Forward, 0, Inverse, 1)]_DissolutionDirY("Y轴溶解方向", Float) = 0
        _DissolutionPosXOrWholeStrength("X轴溶解位置或整体溶解强度", Range(-1, 1)) = 0
        _DissolutionPosY("Y轴溶解位置", Range(-1, 1)) = 0
        
        [Header(Other)]
        _MaskTex("遮罩图", 2D) = "white" {}
    }
    SubShader
    {
        Tags {"IgnoreProjector"="True" "Queue"="Transparent" "RenderType"="Transparent" "RenderPipeline" = "UniversalPipeline"}
        
        Pass
        {
            Tags {"LightMode"="UniversalForward"}
            
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl" 
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            //基础
            TEXTURE2D (_MainTex);SAMPLER(sampler_MainTex);
            TEXTURE2D (_DissolutionTex);SAMPLER(sampler_DissolutionTex);
            TEXTURE2D (_MaskTex);SAMPLER(sampler_MaskTex);

            CBUFFER_START(UnityPerMaterial)
            
            float4 _MainTex_ST;
            
            half4 _Color;
            float _MainTexFlowX;
            float _MainTexFlowY;

            //溶解
            
            float4 _DissolutionTex_ST;

            half _DissolutionSoftness;
            float _DissolutionTexFlowX;
            float _DissolutionTexFlowY;

            half _DissolutionMode;
            half _DissolutionDirX;
            half _DissolutionDirY;
            half _DissolutionPosXOrWholeStrength;
            half _DissolutionPosY;

            //其他
            
            float4 _MaskTex_ST;
            CBUFFER_END

            struct a2v
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float4 customData : TEXCOORD1;
                float4 vertexColor : COLOR;
            };
            struct v2f
            {
                float4 pisitionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 customData : TEXCOORD1;
                float4 vertexColor : COLOR;
            };

            v2f vert(a2v i)
            {
                v2f o;

                o.pisitionCS = TransformObjectToHClip(i.positionOS.xyz);
                o.uv = i.uv;
                o.customData = i.customData;
                o.vertexColor = i.vertexColor;

                return o;
            }
            half4 frag(v2f i) : SV_TARGET
            {
                //主帖图
                float2 mainTexValueUV = i.uv + float2(_MainTexFlowX * _Time.y, _MainTexFlowY * _Time.y);
                mainTexValueUV = mainTexValueUV * _MainTex_ST.xy + _MainTex_ST.zw;
                mainTexValueUV.x += i.customData.x;
                mainTexValueUV.y += i.customData.y;
                half4 mainTexValue = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex, mainTexValueUV);

                //溶解效果
                float2 dissolutionTexUV = i.uv + float2(_Time.y * _DissolutionTexFlowX, _Time.y * _DissolutionTexFlowY);
                dissolutionTexUV = dissolutionTexUV * _DissolutionTex_ST.xy + _DissolutionTex_ST.zw;

                //溶解计算
                half dissolutionTexValue = SAMPLE_TEXTURE2D(_DissolutionTex,sampler_DissolutionTex, dissolutionTexUV).r;
                
                half dissolutionTexValueXOrWholeStrength = (1 - _DissolutionMode) * ((i.uv.x * (1 - _DissolutionDirX) + (1 - i.uv.x) * _DissolutionDirX) + _DissolutionPosXOrWholeStrength + i.customData.z) + _DissolutionMode * (_DissolutionPosXOrWholeStrength + i.customData.z) * 2;
                half dissolutionTexValueX = saturate(dissolutionTexValue * _DissolutionSoftness - lerp(_DissolutionSoftness, -1.5, dissolutionTexValueXOrWholeStrength));

                half dissolutionTexValueYValue = ((i.uv.y * (1 - _DissolutionDirY) + (1 - i.uv.y) * _DissolutionDirY) + _DissolutionPosY + i.customData.w) * (1 - _DissolutionMode) + _DissolutionMode;
                half dissolutionTexValueY = saturate(dissolutionTexValue * _DissolutionSoftness - lerp(_DissolutionSoftness, -1.5, dissolutionTexValueYValue));

                //遮罩图？
                half4 maskTexValue = SAMPLE_TEXTURE2D(_MaskTex, sampler_MaskTex, TRANSFORM_TEX(i.uv, _MaskTex));

                //最后颜色输出
                half3 finalColor = _Color.rgb * mainTexValue.rgb;
                finalColor *= i.vertexColor.rgb;
                finalColor *= dissolutionTexValueX;
                finalColor *= dissolutionTexValueY;
                finalColor *= maskTexValue.rgb;

                //透明度
                half alpha = _Color.a * mainTexValue.a;
                alpha *= i.vertexColor.a;
                alpha *= dissolutionTexValueX;
                alpha *= dissolutionTexValueY;
                alpha *= maskTexValue.a;
                alpha = saturate(alpha);
                
                return half4(finalColor, alpha);
            }
            
            
            ENDHLSL
        }
    }
}
