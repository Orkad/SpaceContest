using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;


[System.Serializable]
public class ObservedList<T> : List<T>
{
	public event System.Action<T> OnAdd = delegate { };
	public event System.Action<T> OnRemove = delegate { };
	public new void Add(T item)
	{
		base.Add(item);
		OnAdd(item);
	}
	public new void Remove(T item)
	{
		base.Remove(item);
		OnRemove(item);
	}
}