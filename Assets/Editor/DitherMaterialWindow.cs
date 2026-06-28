using UnityEngine;
using UnityEditor;

public class DitherMaterialWindow : EditorWindow
{
    Shader    shader;
    Texture2D defaultTexture;
    Color     baseColor          = Color.white;
    Color     compColor          = Color.black;
    float     density            = 1f;
    float     litThreshold       = 0.1f;
    float     falloffThreshold   = 0.1f;
    float     softness           = 0f;

    bool      selectionFoldout   = true;
    Vector2   selectionScroll;

    [MenuItem("Tools/Dither Material")]
    static void Open() => GetWindow<DitherMaterialWindow>("Dither Material");

    void OnGUI()
    {
        EditorGUILayout.LabelField("Shader", EditorStyles.boldLabel);
        shader         = (Shader)   EditorGUILayout.ObjectField("Shader",              shader,         typeof(Shader),    false);
        defaultTexture = (Texture2D)EditorGUILayout.ObjectField("Textura por defecto", defaultTexture, typeof(Texture2D), false);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Colors", EditorStyles.boldLabel);
        baseColor = EditorGUILayout.ColorField("Base Color", baseColor);
        compColor = EditorGUILayout.ColorField("Comp Color", compColor);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Parameters", EditorStyles.boldLabel);
        density          = EditorGUILayout.Slider("Density",           density,          0f, 5f);
        litThreshold     = EditorGUILayout.Slider("Lit Threshold",     litThreshold,     0f, 5f);
        falloffThreshold = EditorGUILayout.Slider("Falloff Threshold", falloffThreshold, 0f, 10f);
        softness         = EditorGUILayout.Slider("Softness",          softness,         0f, 1f);

        EditorGUILayout.Space();
        DrawSelection();

        EditorGUILayout.Space();
        GUI.enabled = CanApply();
        if (GUILayout.Button("Aplicar a selección"))
            ApplyToSelection();
        GUI.enabled = true;

        if (!CanApply())
            EditorGUILayout.HelpBox(GetValidationMessage(), MessageType.Warning);
    }

    void DrawSelection()
    {
        GameObject[] selection = Selection.gameObjects;

        // Header plegable con conteo
        string header = $"Selección actual ({selection.Length})";
        selectionFoldout = EditorGUILayout.Foldout(selectionFoldout, header, true, EditorStyles.foldoutHeader);

        if (!selectionFoldout) return;

        if (selection.Length == 0)
        {
            EditorGUILayout.LabelField("Ningún objeto seleccionado.", EditorStyles.miniLabel);
            return;
        }

        // Scroll con altura máxima fija
        float rowHeight   = EditorGUIUtility.singleLineHeight + 2f;
        float maxHeight   = rowHeight * 6f;
        float listHeight  = rowHeight * selection.Length;
        float scrollHeight = Mathf.Min(listHeight, maxHeight);

        selectionScroll = EditorGUILayout.BeginScrollView(
            selectionScroll,
            GUILayout.Height(scrollHeight));

        foreach (GameObject go in selection)
        {
            Renderer  r   = go.GetComponent<Renderer>();
            Texture2D tex = r != null ? r.sharedMaterial?.mainTexture as Texture2D : null;

            string status = r == null            ? "Sin Renderer"                              :
                            tex != null          ? tex.name                                    :
                            defaultTexture != null ? $"Sin textura → {defaultTexture.name}"   :
                                                     "Sin textura";

            EditorGUILayout.LabelField(go.name, status);
        }

        EditorGUILayout.EndScrollView();
    }

    void ApplyToSelection()
    {
        GameObject[] selection = Selection.gameObjects;

        Undo.SetCurrentGroupName("Aplicar Dither Material");
        int group = Undo.GetCurrentGroup();

        foreach (GameObject go in selection)
        {
            Renderer renderer = go.GetComponent<Renderer>();
            if (renderer == null) continue;

            Material  source = renderer.sharedMaterial;
            Texture2D tex    = source?.mainTexture as Texture2D ?? defaultTexture;

            if (tex == null) continue;

            Material result = tex == source?.mainTexture as Texture2D
                ? DitherMaterialFromMaterial.Create(
                    source, shader, baseColor, compColor,
                    density, litThreshold, falloffThreshold, softness)
                : DitherMaterialCreator.Create(
                    shader, tex, baseColor, compColor,
                    density, litThreshold, falloffThreshold, softness);

            if (result == null) continue;

            Undo.RecordObject(renderer, "Aplicar Dither Material");
            renderer.material = result;
        }

        Undo.CollapseUndoOperations(group);
    }

    bool CanApply() =>
        shader != null &&
        Selection.gameObjects.Length > 0;

    string GetValidationMessage()
    {
        if (shader == null)
            return "Asigna un Shader.";
        if (Selection.gameObjects.Length == 0)
            return "Selecciona al menos un objeto en la escena.";
        return string.Empty;
    }

    void OnSelectionChange() => Repaint();
}