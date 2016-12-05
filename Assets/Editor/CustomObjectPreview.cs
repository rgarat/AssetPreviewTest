using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomPreview(typeof(GameObject))]
public class CustomObjectPreview : ObjectPreview {


    public override bool HasPreviewGUI()
    {
        var go = target as GameObject;
        if (go != null)
        {
			var component = go.GetComponentInChildren<CustomObjectComponent>();

			if (component == null) {
				return false;
			}

			return component.sprite != null;
        }

        return false;
    }

    public override void OnPreviewGUI(Rect r, GUIStyle background)
    {
        var go = target as GameObject;
		var component = go.GetComponentInChildren<CustomObjectComponent>();

		GUI.DrawTexture(r, component.sprite.texture);
        //EditorGUILayout.LabelField("Testito preview " + go.name);
    }

    public override void OnInteractivePreviewGUI(Rect r, GUIStyle background)
    {
        var go = target as GameObject;
		var component = go.GetComponentInChildren<CustomObjectComponent>();

		GUI.DrawTexture(r, component.sprite.texture);

    }


}
