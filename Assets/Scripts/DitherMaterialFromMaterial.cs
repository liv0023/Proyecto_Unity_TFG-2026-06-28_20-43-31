using System;
using UnityEngine;

public static class DitherMaterialFromMaterial
{
    public static Material Create(
        Material  sourceMaterial,
        Shader    shader,
        Color     baseColor,
        Color     compColor,
        float     density,
        float     litThreshold,
        float     falloffThreshold,
        float     softness)
    {
        Texture2D baseTexture = sourceMaterial.mainTexture as Texture2D;

        if (baseTexture != null){
            Debug.Log("[DitherMaterialFromMaterial] Texture:" + baseTexture.name);
            Material _mat = DitherMaterialCreator.Create(
                shader,
                baseTexture,
                baseColor,
                compColor,
                density,
                litThreshold,
                falloffThreshold,
                softness
            );

            return _mat;
        }
        else{
            Debug.LogWarning("[DitherMaterialFromMaterial] El material no tiene una textura principal.");
            return null;
        }
    }
}
