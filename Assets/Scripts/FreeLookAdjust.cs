// Adjust the radius and height of freelook orbits to fit the subject

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FreeLookAdjust : MonoBehaviour
{
	[SerializeField] CinemachineFreeLook freeLook;
	[SerializeField] float radiusMultiplier = 1f;
	[SerializeField] float rigAngle = 45f;
	[SerializeField] Bounds combinedBounds;
	GameObject target;
	//Transform cameraTransform;
	float additionalRadius;
	float originalFOV;

	public float AdditionalRadius { set => additionalRadius = value; }
	//public Transform CameraTransform { get => cameraTransform; }

	private void Awake()
	{
		//cameraTransform = Camera.main.transform;
		originalFOV = freeLook.m_Lens.FieldOfView;
		if(!target)
		{
			target = new GameObject("Freelook Target");
		}
	}

	private void OnEnable()
	{
		freeLook.Follow = target.transform;
		freeLook.LookAt = target.transform;
	}

	[ContextMenu("Test Orbit")]
	public void UpdateOrbit()
	{
		freeLook.m_Orbits[1].m_Radius = CalculateRadius(1) * radiusMultiplier + additionalRadius; // calculate middle radius first so height has a reference
		for (int i = 0; i < freeLook.m_Orbits.Length; i++)
		{
			freeLook.m_Orbits[i].m_Radius = CalculateRadius(i) * radiusMultiplier + additionalRadius;
			freeLook.m_Orbits[i].m_Height = CalculateHeight(i);
		}
	}

    private void Update()
    {
		//UpdateOrbit();
    }

    float CalculateRadius(int index)
	{
		float halfFOV, distance, halfSize;

		// Use SOHCAHTOA to figure out the distance we need based on the FOV and the size of the bounds 
		halfFOV = originalFOV * .5f; // angle ⍺
		halfSize = Vector3.Magnitude(combinedBounds.extents); // side A (opposite)
		distance = (1 / Mathf.Tan(halfFOV * Mathf.Deg2Rad)) * halfSize; // side B (adjacent)

		float radius = distance + halfSize;

		if (index != 1)
		{
			radius *= Mathf.Cos(rigAngle * Mathf.Deg2Rad);
		}

		return radius;
	}

	float CalculateHeight(int index)
	{
		float heightFromCenter = Mathf.Sin(rigAngle * Mathf.Deg2Rad) * freeLook.m_Orbits[1].m_Radius * -1;

		// since the index will be 0, 1, or 2, we can multiply by half the index
		// and get 0 for bottom rig, and the full height for the top. half for the middle
		return heightFromCenter * (index - 1);
	}

	public void AddToCombinedBounds(Bounds addedBounds)
	{
		//Debug.Log($"adding bounds: Center: {addedBounds.center}, Extents: {addedBounds.extents.x}, {addedBounds.extents.y}, {addedBounds.extents.z}");
		if (combinedBounds.size == Vector3.zero)
		{
			combinedBounds = addedBounds;
		}
		else
		{
			combinedBounds.Encapsulate(addedBounds.max);
			combinedBounds.Encapsulate(addedBounds.min);
		}

		target.transform.position = combinedBounds.center;

		UpdateOrbit();
	}

	public void ResetCombinedBounds()
	{
		combinedBounds.center = Vector3.zero;
		combinedBounds.size = Vector3.zero;		
	}

	public void ResetFOV()
	{
		freeLook.m_Lens.FieldOfView = originalFOV;
	}

	public void SetCameraYPosition(float value)
	{
		freeLook.m_YAxis.Value = value;
	}

	public void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(combinedBounds.center, combinedBounds.size);
		Gizmos.DrawWireSphere(combinedBounds.center, .01f);

		Gizmos.color = Color.red;
		Gizmos.DrawLine(combinedBounds.center, Vector3.Normalize(new Vector3(combinedBounds.extents.x, 0, combinedBounds.extents.z) + combinedBounds.center) * Vector3.Magnitude(combinedBounds.extents));
		Gizmos.DrawWireSphere(combinedBounds.center, Vector3.Magnitude(combinedBounds.extents));
	}	
}
