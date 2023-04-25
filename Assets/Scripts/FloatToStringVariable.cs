using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;

public class FloatToStringVariable : MonoBehaviour
{
	[SerializeField] StringVariable stringVariable;
	public float value;

	private void FixedUpdate()
	{
		ConvertToString(value);
	}

	void ConvertToString(float newValue)
	{
		stringVariable.Value = newValue.ToString("0.00");
	}
}
