using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

[InitializeOnLoad]
public class PaletteWindow : Editor {


    static private List<GameObject> prefabs;

	static private Vector2 _scrollPosition;
	private const float ButtonWidth = 200;
	private const float ButtonHeight = 90;

	static PaletteWindow() {
		SceneView.onSceneGUIDelegate -= OnSceneGUI;
		SceneView.onSceneGUIDelegate += OnSceneGUI;
		InitContent();
	}

	private static void OnSceneGUI (SceneView sceneView ) {
		Handles.BeginGUI();
		GUILayout.BeginArea( new Rect( 0, 0, sceneView.position.width, 200 ), EditorStyles.toolbar );
		{
			//EditorGUILayout.LabelField("The GUI of this window was modified.");
			if (GUILayout.Button ("Refresh")) {
				InitContent ();
			}
			if (GUILayout.Button ("ClearCache")) {
				AssetPreview.SetPreviewTextureCacheSize (1);
				AssetPreview.SetPreviewTextureCacheSize (1000);
			}

			DrawScroll (sceneView);
			GUILayout.EndArea ();
			Handles.EndGUI ();
		}
	}





	private static void InitContent () {

		Debug.Log ("InitContent called...");

		prefabs = new List<GameObject> ();
		string[] guids = AssetDatabase.FindAssets ("t:Prefab", new string[] {"Assets/Prefabs"});
		for (int i = 0; i < guids.Length; i++) {
			var assetPath = AssetDatabase.GUIDToAssetPath (guids [i]);
			var asset = AssetDatabase.LoadAssetAtPath (assetPath, typeof(GameObject)) as GameObject;
			prefabs.Add (asset);
		}

	}

	private static void DrawScroll (SceneView sceneView) {
		
		int rowCapacity =
			Mathf.FloorToInt (sceneView.position.width / (ButtonWidth));
		_scrollPosition =
			GUILayout.BeginScrollView (_scrollPosition);
		int selectionGridIndex = -1;
		selectionGridIndex = GUILayout.SelectionGrid (
			selectionGridIndex,
			GetGUIContentsFromItems (),
			rowCapacity,
			GetGUIStyle ());
		GUILayout.EndScrollView ();
	}


	private static GUIContent[] GetGUIContentsFromItems () {
		List<GUIContent> guiContents = new List<GUIContent> ();
		int totalItems = prefabs.Count;
		for (int i = 0; i < totalItems; i ++) {
			GUIContent guiContent = new GUIContent ();
			var item = prefabs[i];
			guiContent.text = item.name;

			var component = item.GetComponentInChildren <AnotherCustomObjectComponent> (true);
			UnityEngine.Object previewItem = component == null ? (Object)item : (Object)component;
			guiContent.image = AssetPreview.GetAssetPreview (previewItem);

		    if (guiContent.image == null)
		    {
				if (AssetPreview.IsLoadingAssetPreview(previewItem.GetInstanceID()))
		        {
		            Debug.LogFormat("Item: {0} is loading Preview", guiContent.text);
		        }
		        else
		        {
		            Debug.LogFormat("Item: {0} has no preview", guiContent.text);
		        }
		    }
			guiContents.Add (guiContent);
		}
		return guiContents.ToArray ();
	}


	private static GUIStyle GetGUIStyle () {
		GUIStyle guiStyle = new GUIStyle (GUI.skin.button);
		guiStyle.alignment = TextAnchor.LowerCenter;
		guiStyle.imagePosition = ImagePosition.ImageAbove;
		guiStyle.fixedWidth = ButtonWidth;
		guiStyle.fixedHeight = ButtonHeight;
		return guiStyle;
	}
}
