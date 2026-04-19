Shader "ludumdare59/Glow"
{
    Properties
    {
        [PerRendererData] _MainTex("Texture", 2D) = "white" {}
        
         _GlowColor("GlowColor", Color) = (1, 1, 1, 0)
        [NoScaleOffset]_GlowTex("GlowTex", 2D) = "white" {}
        _GlowIntensity("GlowIntensity", Float) = 1
        
        _AlphaClipThreshold("AlphaClipThreshold", Range(0, 1)) = 0.5
        
         [Space(20)]
        // Stencil
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        
        _ColorMask ("Color Mask", Float) = 15
    }
    
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "RenderPipeline" = "UniversalPipeline" }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend One OneMinusSrcAlpha
        ColorMask [_ColorMask]
        
        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }
        
        Pass
        {
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Assets/Project/Shaders/MyShaderLibrary.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
                half4 color    : COLOR;
                float2 uv: TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                half4 color    : COLOR;
                float2 uv: TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _GlowColor;
            sampler2D _GlowTex;
            float _GlowIntensity;
            float _AlphaClipThreshold;

            v2f vert(appdata IN)
            {
                v2f OUT;
                
                OUT.uv = IN.uv;
                OUT.pos = TransformObjectToHClip(IN.vertex.xyz);
                OUT.color = IN.color;
                
                return OUT;
            }

            half4 frag(v2f IN) : SV_Target
            {
                half4 color = tex2D(_MainTex, IN.uv) * IN.color;

                color = Glow(IN.uv, color, _GlowTex, _GlowColor, _GlowIntensity);
                
                clip(color.a - _AlphaClipThreshold);
                
                color.rgb *= color.a;
                return color;
            }
            ENDHLSL
        }
    }
}

