using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AssetBundleJsonExporter : Editor
{
	[MenuItem("Assets/Export AssetBundle JSON")]
	public static void ExportJSON()
	{
		AssetBundleDataArray abDataArray = new AssetBundleDataArray();
		List<AssetBundlePathData> abDataList = new List<AssetBundlePathData>();

		// loop thru assetbundles and generate array of pathdata
		foreach (string abName in AssetDatabase.GetAllAssetBundleNames())
		{
			// extract name of ab from path
			string abNameExtracted = abName.Substring(abName.LastIndexOf('/')+1);
			Debug.Log($"{abName} is \n{abNameExtracted}");

			AssetBundlePathData abData = new AssetBundlePathData(abNameExtracted, abName);
			//abData.label = abNameExtracted;
			//abData.value = abName;

			abDataList.Add(abData); 
		}

		abDataArray.threeDModels = abDataList.ToArray();
		string jsonString = JsonUtility.ToJson(abDataArray, true);

		string exportPath = EditorUtility.SaveFilePanel("Save JSON", "", "AssetBundles.json", "json");
		Export(exportPath, jsonString);

		Debug.Log(jsonString);
	}

	static void Export(string path, string json)
	{
		System.IO.File.WriteAllText(path, json);
	}

	[System.Serializable]
	class AssetBundleDataArray
	{
		[SerializeField] public AssetBundlePathData[] threeDModels;
	}

	[System.Serializable]
	class AssetBundlePathData
	{
		[SerializeField] public string label;
		[SerializeField] public string value;

		public AssetBundlePathData(string labelArg, string valueArg)
		{
			label = labelArg;
			value = valueArg;
		}
	}
}
