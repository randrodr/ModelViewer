using System.Collections.Generic;
using UnityEditor;
using System.IO;
using UnityEngine;

public class CreateAssetBundles
{
	public static string assetBundleDirectory = "AssetBundles/";

	[MenuItem("Assets/Build AssetBundles")]
	static void BuildAllAssetBundles()
	{

		//if main directory doesnt exist create it
		if (Directory.Exists(assetBundleDirectory))
		{
			Directory.Delete(assetBundleDirectory, true);
		}

		Directory.CreateDirectory(assetBundleDirectory);

		//create bundles 
		BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.WebGL);
		Debug.Log("WebGL bundle created...");

		RemoveSpacesInFileNames();

		AssetDatabase.Refresh();
		Debug.Log("Process complete!");
	}

	public static void BuildAssetBundlesInPath(string assetBundlePath)
	{
		// assetBundlePath is the subfolder the AssetBundle is set to (eg. atmmd/mod1)

		string directory = assetBundleDirectory + assetBundlePath + '/';

		if (Directory.Exists(directory))
		{
			Directory.Delete(directory, true);
		}
		Directory.CreateDirectory(directory);

		// Compile list of assetbundles in the path
		List<string> abNames = new List<string>();

		foreach (string abName in AssetDatabase.GetAllAssetBundleNames())
		{
			//todo: try catch
			// OR todo: get length of substring by finding last '/'
			string abNamePath = abName.Substring(0, abName.LastIndexOf('/'));
			
			// check if the folder of current ab matches the module that was passed in
			if (abNamePath.GetHashCode() == assetBundlePath.GetHashCode())
			{
				//Debug.Log($"{abName} is in {assetBundlePath}");
				abNames.Add(abName);
			}
			else
			{
				//Debug.Log($"{abName} is not in {assetBundlePath}");
			}
		}
		
		List<AssetBundleBuild> builds = new List<AssetBundleBuild>();
		foreach (string abName in abNames)
		{
			var assetPaths = AssetDatabase.GetAssetPathsFromAssetBundle(abName);

			AssetBundleBuild build = new AssetBundleBuild();
			build.assetBundleName = abName;
			build.assetNames = assetPaths;

			builds.Add(build);
			Debug.Log($"assetBundle to build: {build.assetBundleName}");
		}

		BuildPipeline.BuildAssetBundles(assetBundleDirectory, builds.ToArray(), BuildAssetBundleOptions.None, BuildTarget.WebGL);
		Debug.Log($"Bundle created at {directory}");

		AssetDatabase.Refresh();
		Debug.Log("Process complete!");
	}

	static void RemoveSpacesInFileNames()
	{
		foreach (string path in Directory.GetFiles(assetBundleDirectory))
		{
			string oldName = path;
			string newName = path.Replace(' ', '-');
			File.Move(oldName, newName);
		}
	}

	static void AppendPlatformToFileName(string platform)
	{
		foreach (string path in Directory.GetFiles(assetBundleDirectory))
		{
			//get filename
			string[] files = path.Split('/');
			string fileName = files[files.Length - 1];

			//delete files we dont need
			if (fileName.Contains(".") || fileName.Contains("Bundle"))
			{
				File.Delete(path);
			}
			else if (!fileName.Contains("-"))
			{
				//append platform to filename
				FileInfo info = new FileInfo(path);
				info.MoveTo(path + "-" + platform);
			}
		}
	}
}