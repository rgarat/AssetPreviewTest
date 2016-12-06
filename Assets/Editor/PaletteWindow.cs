using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;


public class PaletteWindow : EditorWindow {

	public static PaletteWindow instance;

    private List<GameObject> prefabs;

	private Vector2 _scrollPosition;
	private const float ButtonWidth = 200;
	private const float ButtonHeight = 90;


	[MenuItem ("Tools/ShowPreviewPalette", false, 51)]
	public static void ShowPalette () {
		instance = (PaletteWindow)EditorWindow.GetWindow (typeof(PaletteWindow));
		instance.titleContent = new GUIContent ("Palette");
	}

	private void OnEnable ()
	{
	    InitContent();
	}

	private void OnDisable () {
		//Debug.Log ("OnDisable called...");
	}

	private void OnDestroy () {
		//Debug.Log ("OnDestroy called...");
	}

	private void Update () {
//			if (_previews.Count != _items.Count) {
//				GeneratePreviews ();
//			}
	}

	private void OnGUI () {
		//EditorGUILayout.LabelField("The GUI of this window was modified.");
	    if (GUILayout.Button("Refresh"))
	    {
	        InitContent();
	    }
	    if (GUILayout.Button("Set 0"))
	    {
	        AssetPreview.SetPreviewTextureCacheSize(1);
	    }
	    if (GUILayout.Button("Set 1000"))
	    {
	        AssetPreview.SetPreviewTextureCacheSize(1000);
	    }


		DrawScroll ();
	}





	private void InitContent () {

		Debug.Log ("InitContent called...");

		prefabs = new List<GameObject> ();
		string[] guids = AssetDatabase.FindAssets ("t:Prefab", new string[] {"Assets/Prefabs"});
		for (int i = 0; i < guids.Length; i++) {
			var assetPath = AssetDatabase.GUIDToAssetPath (guids [i]);
			var asset = AssetDatabase.LoadAssetAtPath (assetPath, typeof(GameObject)) as GameObject;
			prefabs.Add (asset);
		}

	}

	private void DrawScroll () {
		
		int rowCapacity =
			Mathf.FloorToInt (position.width / (ButtonWidth));
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


	private GUIContent[] GetGUIContentsFromItems () {
		List<GUIContent> guiContents = new List<GUIContent> ();
		int totalItems = prefabs.Count;
		for (int i = 0; i < totalItems; i ++) {
			GUIContent guiContent = new GUIContent ();
			var item = prefabs[i];
			guiContent.text = item.name;

			var component = item.GetComponentInChildren <AnotherCustomObjectComponent> ();
		    guiContent.image = AssetPreview.GetAssetPreview (component == null ? (Object)item : (Object)component);

		    if (guiContent.image == null)
		    {
		        if (AssetPreview.IsLoadingAssetPreview(item.gameObject.GetInstanceID()))
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


	private GUIStyle GetGUIStyle () {
		GUIStyle guiStyle = new GUIStyle (GUI.skin.button);
		guiStyle.alignment = TextAnchor.LowerCenter;
		guiStyle.imagePosition = ImagePosition.ImageAbove;
		guiStyle.fixedWidth = ButtonWidth;
		guiStyle.fixedHeight = ButtonHeight;
		return guiStyle;
	}
}
