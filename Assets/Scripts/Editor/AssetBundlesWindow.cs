using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityAtoms.BaseAtoms;

public class AssetBundlesWindow : EditorWindow
{
	[SerializeField] StringValueList modules;

	string[] modulesArray;
	[SerializeField] [HideInInspector] int selectionIndex;

	// button style fields
	GUIStyle style, oldStyle;

	private void OnEnable()
	{
		if (modules)
		{
			ConvertListToArray();
			//modules.Added.Register(ConvertListToArray);
		}
	}

	void ConvertListToArray()
	{
		modulesArray = modules.List.ToArray();
	}

	[MenuItem("Window/AssetBundle Builder")]
	public static void ShowWindow()
	{
		GetWindow<AssetBundlesWindow>("AssetBundle Builder");
	}

	private void OnGUI()
	{
		modules = (StringValueList)EditorGUILayout.ObjectField("Modules List: ", modules, typeof(StringValueList), false);
		if (modulesArray.Length > 0)
		{
			selectionIndex = GUILayout.SelectionGrid(selectionIndex, modulesArray, 4); 
		}

		EditorGUILayout.Space(16f);

		style = GUI.skin.button;
		oldStyle = style;

		//style.padding = new RectOffset(0, 0, 10, 10);
		//style.margin = new RectOffset(10, 10, 10, 10);
		if (GUILayout.Button($"Build AssetBundles in\n {modules.List[selectionIndex]}", style))
		{
			BuildSelectedAssetBundle();
		}

		style = oldStyle;
	}

	void BuildSelectedAssetBundle()
	{
		CreateAssetBundles.BuildAssetBundlesInPath(modules.List[selectionIndex]);
	}

	private void OnDisable()
	{
		//if (modules)
		//{
		//	modules.Added.UnregisterAll(); 
		//}
	}
}
