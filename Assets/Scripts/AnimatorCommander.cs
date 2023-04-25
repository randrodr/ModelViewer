using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorCommander : MonoBehaviour
{
	public AnimatorSet animatorsInScene;
	public Dictionary<string, Animator> animatorsByID = new Dictionary<string, Animator>();

	// debug fields
	public string animationDataTest;

	private void Awake()
	{
		Debug.Log($"{animatorsInScene}");
		animatorsByID = SetToDictionary(animatorsInScene);
		//animatorsInScene.OnItemAdded += FillDictionary;
	}

	private void OnEnable()
	{
		Debug.Log("Animator Manager on enable");
		//animatorsInScene.OnItemAdded += FillDictionary;
		animatorsInScene.OnAnimatorAdded.AddListener(FillDictionary);
	}

	private void OnDisable()
	{
		Debug.Log("Animator Manager disabled");
		//animatorsInScene.OnItemAdded -= FillDictionary;
		animatorsInScene.OnAnimatorAdded.RemoveAllListeners();
	}

	public Dictionary<string, Animator> SetToDictionary(AnimatorSet set)
	{
		Debug.Log($"SetToDictionary the {set} of {set.Items.Count} {set.Items}");
		Dictionary<string, Animator> newDict = new Dictionary<string, Animator>();
		Animator[] animators = set.Items.ToArray();

		for (int i = 0; i < set.Items.Count; i++) // loop through animators in set (list)
		{
			Debug.Log($"dict loop i = {set.Items[i]}");
			if (set.Items[i] == null)
			{
				Debug.Log($"{set.Items[i]} is null");
				continue;
			}
			if (!newDict.ContainsKey(set.Items[i].runtimeAnimatorController.name))
			{
				Debug.Log($"New key: {set.Items[i].runtimeAnimatorController.name}");
				for (int j = 0; j < animators.Length; j++) // loop through animators in set (array)
				{
					Debug.Log($"dict loop j = {animators[j]}");
					if (animators[j].runtimeAnimatorController == set.Items[i].runtimeAnimatorController)
					{
						Debug.Log($"Adding {set.Items[i].runtimeAnimatorController.name} to animator dictionary");
						newDict.Add(set.Items[i].runtimeAnimatorController.name, animators[j]);
					}
				}
			}
		}

		return newDict;
	}

	public void FillDictionary(Animator newAnimator)
	{
		Debug.Log($"Refreshing Animator Dictionary with {animatorsInScene} of count {animatorsInScene.Items.Count}");
		animatorsByID = SetToDictionary(animatorsInScene);
		Debug.Log($"animator dictionary keys count = {animatorsByID.Keys.Count}");
	}

	public void SetParameter(Animator animator, string parameterName, string valueAsString, string typeAsString)
	{
		if (Enum.TryParse(typeAsString, true, out AnimatorControllerParameterType parameterType))
		{
			Debug.Log($"Changing state of {animator}'s {parameterName} to {valueAsString}");

			switch (parameterType)
			{
				case AnimatorControllerParameterType.Float:
					if (float.TryParse(valueAsString, out float parsedFloat))
						animator.SetFloat(parameterName, parsedFloat);
					break;
				case AnimatorControllerParameterType.Int:
					if (int.TryParse(valueAsString, out int parsedInt))
						animator.SetInteger(parameterName, parsedInt);
					break;
				case AnimatorControllerParameterType.Bool:
					if (bool.TryParse(valueAsString, out bool parsedBool))
						animator.SetBool(parameterName, parsedBool);
					break;
				case AnimatorControllerParameterType.Trigger:
					animator.SetTrigger(parameterName);
					break;
				default:
					break;
			}
		}
		else
		{
			Debug.LogWarning($"Could not parse {typeAsString} type, skipping");
		}
	}

	[ContextMenu("Test animation")]
	public void TestAnimation()
	{
		ForceAnimate(animationDataTest);
	}

	public void ForceAnimate(string animationData)
	{
		// animationData string example: "BallAnimator Bouncing True Bool"
		char breakChar = ' ';

		Debug.Log($"ForceAnimate: {animationData}");

		string animatorName = animationData.Substring(0, animationData.IndexOf(breakChar));
		animationData = animationData.Remove(0, animationData.IndexOf(breakChar) + 1);
		Debug.Log($"{animatorName} taken and left {animationData}");

		string parameterName = animationData.Substring(0, animationData.IndexOf(breakChar));
		animationData = animationData.Remove(0, animationData.IndexOf(breakChar) + 1);
		Debug.Log($"{parameterName} taken and left {animationData}");

		string parameterValue = animationData.Substring(0, animationData.IndexOf(breakChar));
		animationData = animationData.Remove(0, animationData.IndexOf(breakChar) + 1);
		Debug.Log($"{parameterValue} taken and left {animationData}");

		string parameterType = animationData;
		Debug.Log($"{parameterType} is left");

		Animator animator = animatorsByID[animatorName];

		if (animator)
		{
			Debug.Log("Setting animator parameter");
			SetParameter(animator, parameterName, parameterValue, parameterType);
		}
	}
}
