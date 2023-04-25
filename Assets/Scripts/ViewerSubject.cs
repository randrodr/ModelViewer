using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ViewerSubject : MonoBehaviour
{
	[SerializeField] float additionalRadius = 0f;
	[SerializeField] float initalCameraYNormalized = .5f;
	
	FreeLookAdjust flAdjust;

	private void Awake()
	{
		if (!flAdjust)
		{
			flAdjust = FindObjectOfType<FreeLookAdjust>();
		}
	}

	private void OnEnable()
    {
		SetCameraSubject();
	}

	[ContextMenu("Test new subject")]
	void SetCameraSubject()
	{
		if(!flAdjust)
		{
			Debug.LogWarning("No freelook camera adjust script found");
			return;
		}

		flAdjust.ResetFOV();
		flAdjust.ResetCombinedBounds();
		flAdjust.AdditionalRadius = additionalRadius; // in case the zoom needs changed
		// for initial camera angle
		flAdjust.SetCameraYPosition(initalCameraYNormalized);
		//float toCamera = flAdjust.CameraTransform.ang
		//transform.Rotate(Vector3.up, (180f - toCamera));
		
		foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
		{
			Debug.Log($"add renderer: {renderer.gameObject}");

			flAdjust.AddToCombinedBounds(renderer.bounds);
		}		
	}
}