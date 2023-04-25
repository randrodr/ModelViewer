// This is for stopping the freelook when the mouse button isn't being clicked

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VirtualCamHoldEnable : MonoBehaviour
{
	[SerializeField] InputActionReference activationInputAction;
	[SerializeField] Cinemachine.CinemachineInputProvider inputProvider; 

	InputActionReference lookActionReference;


	private void OnEnable()
	{
		activationInputAction.action.Enable();
	}

	void Awake()
    {
		// store the input provider XY Axis for later, and set reference to null
		lookActionReference = inputProvider.XYAxis;
		inputProvider.XYAxis = null;

		activationInputAction.action.started += context => EnableCam(true);
		activationInputAction.action.canceled += context => EnableCam(false);
		//inputActionReference.action.started += context => ActivateCam(context);
		//inputActionReference.action.canceled += context => ActivateCam(context);
	}

	public void EnableCam(bool value)
	{
		//Debug.Log("Enable");
		
		inputProvider.XYAxis = value ? lookActionReference : null;
	}

	public void ActivateCam(InputAction.CallbackContext context)
	{
		//Debug.Log("Activate");
		inputProvider.XYAxis = context.performed ? lookActionReference : null;
	}

	private void OnDisable()
	{
		activationInputAction.action.Disable();
	}
}
