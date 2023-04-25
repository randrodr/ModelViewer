using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public abstract class RuntimeSet<T> : ScriptableObject
{
	[SerializeField] public List<T> Items = new List<T>();

	public delegate void Del(T item);
	public Del OnItemAdded = delegate { };

	public virtual void Add(T t)
	{
		if (!Items.Contains(t))
		{
			Debug.Log($"{name} is adding {t}");
			Items.Add(t);
			Debug.Log($"{name} now has {Items.Count} items:\n");
			foreach(T item in Items)
			{
				Debug.Log($" -- {item}");
			}
			//OnItemAdded(t);
		}
	}

	public void Remove(T t)
	{
		if (Items.Contains(t))
			Items.Remove(t);
	}
}
