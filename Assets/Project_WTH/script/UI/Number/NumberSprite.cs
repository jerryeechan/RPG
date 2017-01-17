using UnityEngine;
using System.Collections;

public class NumberSprite : MonoBehaviour {

	// Use this for initialization
	SpriteRenderer spr;
	void Awake () {
		spr= GetComponent<SpriteRenderer>();
	}
	public void setSprite(Sprite sp)
	{
		spr.sprite = sp;
	}
}
