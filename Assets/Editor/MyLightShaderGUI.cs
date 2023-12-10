using System;
using UnityEditor;
using UnityEngine;

public class MyLightingShaderGUI : ShaderGUI
{
    private Material target;
    private MaterialEditor editor;
    private MaterialProperty[] properties;
    private static GUIContent staticLabel = new GUIContent();
    
    MaterialProperty FindProperty(string name)
    {
        return FindProperty(name, properties);
    }

    static GUIContent MakeLabel(string text, string tooltip = null)
    {
        staticLabel.text = text;
        staticLabel.tooltip = tooltip;
        return staticLabel;
    }
    
    static GUIContent MakeLabel(MaterialProperty property, string tooltip = null)
    {
        staticLabel.text = property.displayName;
        staticLabel.tooltip = tooltip;
        return staticLabel;
    }

    void SetKeyword(string keyword, bool state)
    {
        if (state)
        {
            target.EnableKeyword(keyword);
        }
        else
        {
            target.DisableKeyword(keyword);
        }
    }
    
    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        // editor.target has generic Object type.
        this.target = materialEditor.target as Material;
        this.editor = materialEditor;
        this.properties = properties;
        DoMain();
        DoSecondary();
    }

    void DoMain()
    {
        GUILayout.Label("Main Maps", EditorStyles.boldLabel);

        MaterialProperty mainTex = FindProperty("_MainTex");

        editor.TexturePropertySingleLine(MakeLabel(mainTex, "Albedo (RGB)"), 
            mainTex, FindProperty("_Tint"));

        DoMetallic();
        DoSmoothness();
        DoNormals();
        
        editor.TextureScaleOffsetProperty(mainTex);
    }
    private void DoMetallic()
    {
        MaterialProperty map = FindProperty("_MetallicMap");
        EditorGUI.BeginChangeCheck();
        editor.TexturePropertySingleLine(MakeLabel(map, "Metal Map"),
            map, map.textureValue ? null : FindProperty("_Metallic"));
        if (EditorGUI.EndChangeCheck())
        {
            SetKeyword("_METALLIC_MAP", map.textureValue);
        }
    }
    private void DoSmoothness()
    {
        MaterialProperty slider = FindProperty("_Smoothness");
        EditorGUI.indentLevel += 2;
        editor.ShaderProperty(slider, MakeLabel(slider));
        EditorGUI.indentLevel -= 2;
    }
    

    void DoNormals()
    {
        GUILayout.Label("Normal Maps", EditorStyles.boldLabel);
        
        MaterialProperty map = FindProperty("_NormalMap");
        editor.TexturePropertySingleLine(MakeLabel(map, "Normal"), 
            map, map.textureValue ? FindProperty("_BumpScale") : null);
    }

    void DoSecondary()
    {
        GUILayout.Label("Secondary Maps", EditorStyles.boldLabel);

        MaterialProperty detailTex = FindProperty("_DetailTex");
        editor.TexturePropertySingleLine(MakeLabel(detailTex, "Albedo (RGB) multiplied by 2")
            , detailTex);
        DoSecondaryNormals();
        editor.TextureScaleOffsetProperty(detailTex);
    }
    private void DoSecondaryNormals()
    {
        MaterialProperty map = FindProperty("_DetailNormalMap");
        editor.TexturePropertySingleLine(MakeLabel(map, "Detail Normal"),
            map, map.textureValue ? FindProperty("_DetailBumpScale") : null);
    }
    
    
}
