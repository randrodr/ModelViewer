using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToAnimatorSet : MonoBehaviour
{
	public AnimatorSet set;

    void Awake()
    {
		set = FindObjectOfType<AnimatorCommander>().animatorsInScene;
		if(set == null)
		{
			Debug.LogWarning("No animator set found");
			return;
		}
		Debug.Log($"{gameObject.name} trying to add its animator to {set}");
		set.Add(GetComponent<Animator>());

		// horrible bandaid for addlistener not working
		AnimatorCommander animatorCommander = FindObjectOfType<AnimatorCommander>();
		if(animatorCommander)
		{
			animatorCommander.FillDictionary(GetComponent<Animator>());
		}
    }

	private void OnEnable()
	{
		
		//set.Add(GetComponent<Animator>());
	}
}
