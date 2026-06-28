using UnityEngine;

public static class DitherMaterialCreator
{
    public static Material Create(
        Shader    shader,
        Texture2D baseTexture,
        Color     baseColor,
        Color     compColor,
        float     density,
        float     litThreshold,
        float     falloffThreshold,
        float     softness)
    {
        Material _mat = new Material(shader) {name = "DitheringMaterial_Generated"};

        _mat.SetTexture(Shader.PropertyToID("_MainTex"),      baseTexture);
        _mat.SetColor  (Shader.PropertyToID("_BaseColor"),        baseColor);
        _mat.SetColor  (Shader.PropertyToID("_BaseColor_1"),        compColor);
        _mat.SetFloat  (Shader.PropertyToID("_Density"),          density);
        _mat.SetFloat  (Shader.PropertyToID("_Lit_Threshold"),     litThreshold);
        _mat.SetFloat  (Shader.PropertyToID("_Falloff_Threshold"), falloffThreshold);
        _mat.SetFloat  (Shader.PropertyToID("_Softness"),         softness);

        return _mat;
    }
}