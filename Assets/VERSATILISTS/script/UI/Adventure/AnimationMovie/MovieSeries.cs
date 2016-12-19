using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieSeries : Series<MovieClip> {

}
public class Series<T>:MonoBehaviour{
	Queue<T> queue;

	[SerializeField]
	T[] array;
	public void Awake()
	{
		if(array==null||array.Length == 0)
		{
			array = GetComponentsInChildren<T>();
		}
		
		queue = new Queue<T>();
		foreach(var element in array)
		{
			queue.Enqueue(element);
		}
	}
	public T getNext()
	{
		if(queue.Count>0)
			return queue.Dequeue();
		else
			return default(T);
	}
}