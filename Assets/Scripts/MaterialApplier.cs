using UnityEngine;
public static class DitherMaterialApplier
{
    public static void Apply(
        GameObject target,
        Shader     shader,
        Texture2D  baseTexture,
        Color      baseColor,
        Color      compColor,
        float      density,
        float      litThreshold,
        float      falloffThreshold,
        float      softness)
    {
        Renderer renderer = target.GetComponent<Renderer>();

        if (renderer != null){

            renderer.material = DitherMaterialCreator.Create(
                shader,
                baseTexture,
                baseColor,
                compColor,
                density,
                litThreshold,
                falloffThreshold,
                softness
            );
        }
        else    
            Debug.LogWarning($"[DitherMaterialApplier] {target.name} no tiene Renderer.", target);
        
        return;
    }
}