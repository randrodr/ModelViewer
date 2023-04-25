using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScreenCaptureWindow : EditorWindow
{
	[MenuItem("Tools/Camera Capture")]
	public static void ShowWindow()
	{
		GetWindow<ScreenCaptureWindow>("Camera Capture");
	}

	private void OnGUI()
	{
		GUILayout.Space(8f);
		if (GUILayout.Button("Take Capture"))
		{
			SaveScreenshot();
		}		
	}

	private void SaveScreenshot()
	{
		string exportPath = EditorUtility.SaveFilePanel("Save Screen Capture", "", "Screencapture.png", "png");
		ScreenCapture.CaptureScreenshot(exportPath);
	}
}
