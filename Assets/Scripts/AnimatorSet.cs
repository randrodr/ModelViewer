using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class AnimatorEvent : UnityEvent<Animator> { }

[System.Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/Animator Set")]
public class AnimatorSet : RuntimeSet<Animator>, ISerializationCallbackReceiver
{
	public AnimatorEvent OnAnimatorAdded;

	public override void Add(Animator t)
	{
		base.Add(t);
		OnAnimatorAdded.Invoke(t);
	}

	public void OnAfterDeserialize()
	{
		//Items.Clear();
	}

	public void OnBeforeSerialize()
	{
		
	}
}
