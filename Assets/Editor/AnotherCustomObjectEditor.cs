using System;
using UnityEngine;
using System.Linq;
using UnityEditor;
using Object = UnityEngine.Object;

[CustomEditor(typeof(AnotherCustomObjectComponent))]
public class AnotherCustomObjectEditor : Editor {

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("CustomInspector");

       base.OnInspectorGUI();
    }

    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        Debug.LogFormat("AssetPath: {0} - subassets:{1} - width:{2} - height:{3}", assetPath, String.Join(", ", subAssets.Select(o => o.name).ToArray()), width, height);


        var tex = new Texture2D(width, height, TextureFormat.ARGB32, false);

        Color32[] pixels = new Color32[width * height];
        for(int i = 0; i < width * height; i++)
        {
            pixels[i] = Color.red;
        }

        tex.SetPixels32(pixels);
        tex.Apply();


        return tex;
    }
}
