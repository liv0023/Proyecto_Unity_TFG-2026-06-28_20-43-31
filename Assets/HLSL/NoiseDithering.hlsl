void DitheringNoise_float(
    UnityTexture2D MainTex,
    UnityTexture2D Noise,
    float2         UV,
    float2         NoiseUV,
    float2         ScreenPos,
    float Strength,
    out float4     RGBA
)
{
    float4 texColor  = SAMPLE_TEXTURE2D(MainTex.tex, MainTex.samplerstate, UV);
    float  luminance = dot(texColor.rgb, float3(0.299, 0.587, 0.114));

    // Obtener dimensiones reales del blue noise
    float noiseW, noiseH;
    Noise.tex.GetDimensions(noiseW, noiseH);

    // Convertir ScreenPos [0,1] a coordenadas de píxel
    float2 screenPixel = ScreenPos * _ScreenParams.xy;

    // Tileable en la resolución nativa del noise, independiente de MainTex
    float2 noiseUV = fmod(screenPixel, float2(noiseW, noiseH)) / float2(noiseW, noiseH);

    float noise = SAMPLE_TEXTURE2D(Noise.tex, Noise.samplerstate, noiseUV).r * Strength;

    //Pintar el pixel en blanco si tiene mayor luminancia o en negro si tiene menor
    RGBA = luminance > noise ? float4(1.0, 1.0, 1.0, 1.0) : float4(0.0, 0.0, 0.0, 1.0);
}