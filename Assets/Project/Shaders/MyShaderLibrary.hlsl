#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"

real4 Glow(float2 UV, real4 Color, sampler2D GlowTex, real4 GlowColor, float GlowIntensity)
{
    half4 texColor = tex2D(GlowTex, UV);
    
    if (texColor.a < 0.1)
        return Color;
    
    half4 addColor = tex2D(GlowTex, UV) * GlowColor * GlowIntensity;
    addColor.a = 0;

    Color += addColor;
    return Color;
}