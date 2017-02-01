using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using com.jerrch.rpg;
public class ReSkinRenderer : MonoBehaviour {

	public string classesName;
	public string suitName;
	public string equipTypeName;
	public string findName;

	
	protected SpriteRenderer spr;
	void Awake()
	{	
		spr = GetComponent<SpriteRenderer>();
	}
	
	Sprite[] subSprties;
	protected void getSprites(string classesName,string suitName,string equipTypeName)
	{
		this.classesName = classesName;
		this.suitName = suitName;
		this.equipTypeName = equipTypeName;
		//equipTypeName = ch_folder_name;
		subSprties = Resources.LoadAll<Sprite>("Characters/"+classesName+"/"+suitName+"/"+equipTypeName);
	}
	public string directory;
	void LateUpdate()
	{
		if(subSprties==null)
		{
			directory = "Characters/"+classesName+"/"+suitName+"/"+equipTypeName;
			subSprties = Resources.LoadAll<Sprite>("Characters/"+classesName+"/"+suitName+"/"+equipTypeName);
		}
		
		
		
		if(subSprties.Length>1)
		{
			var ori_SpriteName = spr.sprite.name;
			findName = suitName+"_"+equipTypeName+"_"+ori_SpriteName;

			var newSprite = Array.Find(subSprties,item => item.name==findName);
			//print(newSprite);
			if(newSprite)
				spr.sprite = newSprite;
		}
		else
		{
			spr.sprite = SpriteManager.instance.emptySprite;
		}
		
		//foreach()
		
	}
}
