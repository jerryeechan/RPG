using UnityEngine;
using System.Collections;

public class DungeonOptionButton : MonoBehaviour {

	public CompositeText text;
	public int index;
	void Awake()
	{
		text = GetComponentInChildren<CompositeText>();
	}
}

