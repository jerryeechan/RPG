using UnityEngine;
using System.Collections;

public class CharacterTile : MonoBehaviour {

	public int x;
	public int y;
	public RectTransform rect;
	void Awake()
	{
		rect = GetComponent<RectTransform>();
	}
	
}
