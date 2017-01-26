using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using com.jerrch.rpg;
public class ReSkinRenderer : MonoBehaviour {

	public string prefix;
	public string findName;

	
	protected SpriteRenderer spr;
	void Awake()
	{	
		spr = GetComponent<SpriteRenderer>();
	}
	
	Sprite[] subSprties;
	public void getSprites(string folder)
	{
		prefix = folder;
		subSprties = Resources.LoadAll<Sprite>("Characters/"+prefix);
	}
	void LateUpdate()
	{
		var ori_SpriteName = spr.sprite.name;
		
		findName = prefix+"_"+ori_SpriteName;
		
		//subSprties = Resources.LoadAll<Sprite>("Characters/"+prefix);
		//var subSprties = Resources.LoadAll<Sprite>("Characters/"+prefix);
		var newSprite = Array.Find(subSprties,item => item.name==findName);
		//print(newSprite);
		if(newSprite)
			spr.sprite = newSprite;
		//foreach()
		
	}
}
