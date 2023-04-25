using UnityEngine;

public class ContentController : MonoBehaviour
{
	public API api;

	public void LoadContent(string path)
	{
		AssetBundle.UnloadAllAssetBundles(true);
		DestroyAllChildren();
		api.GetBundleObject(path, OnContentLoaded, transform);
	}

	void OnContentLoaded(GameObject content)
	{
		//do something cool here
		Debug.Log("Loaded: " + content.name);
	}

	void DestroyAllChildren()
	{
		foreach (Transform child in transform)
		{
			Destroy(child.gameObject);
		}
	}
}