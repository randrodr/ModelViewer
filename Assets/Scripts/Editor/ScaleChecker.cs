using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScaleChecker : EditorWindow
{
	string badScalesText;
	Vector2 scrollPos;

	[MenuItem("Tools/Scale Police")]
	public static void ShowWindow()
	{
		GetWindow<ScaleChecker>("Scale Police");
	}

	private void OnEnable()
	{
		ShowBadScales();
	}

	private void OnGUI()
	{
		
		GUILayout.Space(8f);
		if (GUILayout.Button("Scan scene for Non-1x scale"))
		{
			ShowBadScales();
		}
		GUILayout.Space(8f);
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
		GUILayout.TextArea(badScalesText);
		EditorGUILayout.EndScrollView();
	}

	void ShowBadScales()
	{
		badScalesText = "";
		int objectsCount = 0;
		foreach (Transform sceneObject in FindObjectsOfType<Transform>())
		{
			if (sceneObject.localScale != Vector3.one)
			{
				objectsCount++;
				badScalesText += sceneObject.name + "\n";
				badScalesText += sceneObject.localScale.ToString() + "\n\n";
			}
		}
		badScalesText = $"Scanned at {System.DateTime.Now}\n\n{objectsCount} Objects with non-1x scale:\n\n{badScalesText}";
	}
}
