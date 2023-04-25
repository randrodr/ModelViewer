using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class API : MonoBehaviour
{
	UnityWebRequest www = new UnityWebRequest(); // keep this as member so we can cancel it later if needed
	
	// pass the URL to this from the webpage
	public void GetBundleObject(string path, UnityAction<GameObject> callback, Transform bundleParent)
	{
		StartCoroutine(GetDisplayBundleRoutine(path, callback, bundleParent));
	}

	IEnumerator GetDisplayBundleRoutine(string path, UnityAction<GameObject> callback, Transform bundleParent)
	{
		// we abort any ongoing requests in case we're requesting too fast (we only want one model on screen at a time)
		www.Abort();

		Debug.Log("Requesting bundle at " + path);
		
		//request asset bundle
		www = UnityWebRequestAssetBundle.GetAssetBundle(path);
		yield return www.SendWebRequest();

		if (www.result != UnityWebRequest.Result.Success)
		{
			Debug.Log($"Error: {www.result.ToString()} ");
		}
		else
		{
			AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
			if (bundle != null)
			{
				string rootAssetPath = bundle.GetAllAssetNames()[0];
				GameObject viewerSubject = Instantiate(bundle.LoadAsset(rootAssetPath) as GameObject, bundleParent);
				bundle.Unload(false);
				callback(viewerSubject);
			}
			else
			{
				Debug.Log("Not a valid asset bundle");
			}
		}
	}

	
}