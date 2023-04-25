using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityAtoms.BaseAtoms;

public class IntToStringVariable : MonoBehaviour
{
	[SerializeField] StringVariable stringVariable;
	public int value;

	private void FixedUpdate()
	{
		ConvertToString(value);
	}

	void ConvertToString(int newValue)
	{
		stringVariable.Value = newValue.ToString();
	}
}
