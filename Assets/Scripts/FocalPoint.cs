using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FocalPoint : MonoBehaviour
{
	CinemachineFreeLook camera;

    void OnEnable()
    {
		if(!camera)
		{
			camera = FindObjectOfType<CinemachineFreeLook>();
		}
		//camera.Follow = transform;
		camera.LookAt = transform;
    }
}
