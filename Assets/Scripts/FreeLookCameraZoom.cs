using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class FreeLookCameraZoom : MonoBehaviour
{
	[SerializeField] InputActionReference zoomInputAction;
	[SerializeField] Vector2 FOV_Extents;

	private CinemachineFreeLook flCam;

	// demo
	public float zoomSensitivity = 1f;

	private void Awake()
	{
		if (flCam == null)
			flCam = GetComponent<CinemachineFreeLook>();

		// New input setup
		zoomInputAction.action.performed += context => { ChangeFOV(context.ReadValue<float>()); };

		//FOV_Extents.x = flCam.m_Lens.FieldOfView - 20f;
		//FOV_Extents.y = flCam.m_Lens.FieldOfView + 20f;
	}

	void ChangeFOV(float scrollInput)
	{
		if (flCam)
		{
			flCam.m_Lens.FieldOfView += zoomSensitivity * scrollInput / Mathf.Abs(scrollInput) * -1;
			flCam.m_Lens.FieldOfView = Mathf.Clamp(flCam.m_Lens.FieldOfView, FOV_Extents.x, FOV_Extents.y); 
		}
	}

	private void OnEnable()
	{
		zoomInputAction.action.Enable();
	}

	private void OnDisable()
	{
		zoomInputAction.action.Disable();
	}
}
