﻿using UnityEngine;
using System.Collections.Generic;
using com.jerrch.rpg;

//as Equipment template and also the runtime equip to character
public class Equip:Item {
	public static EquipType[] AllEquipType ={EquipType.Armor,EquipType.Helmet,EquipType.Weapon,EquipType.Shield}; 
	EquipState state = EquipState.Template;
	TemplateEffectSet[] templateEffectSets;
	[HideInInspector]
	public Character ch;
	[HideInInspector]
	public EquipGraphicAsset bindGraphic;
	public List<SkillEffect> effects;
	public EquipType equipType;
	public AbilityRequirement[] abilityReqs;
	void Awake()
	{
		templateEffectSets = GetComponentsInChildren<TemplateEffectSet>();
		itemType = ItemType.Equip;
		stackable = false;
		
	}
	override protected void Reset()
	{
		stackable = false;
		itemType = ItemType.Equip;
	}
	public void setEffect(int[] available)
	{
		effects = new List<SkillEffect>();
		
		for(int i=0;i<available.Length;i++)
		{
			if(templateEffectSets[i].effects.Length>available[i])
				effects.Add(templateEffectSets[i].effects[available[i]]);
		}
	}

	public string getEffectString()
	{
		string str="";
		foreach(var effect in effects)
		{
			str+=effect.getDescription()+"\n";
		}
		return str;
	}
	public string getRequirementString()
	{
		string str = "";
		foreach(var req in abilityReqs)
		{
			str+=req.ToString()+" ";
		}
		return str;
	}
	//the effects this equips has;	
	public void randomEquipStats()
	{
		effects = new List<SkillEffect>();
		foreach (TemplateEffectSet effectSet in templateEffectSets)
		{
			int propertyNum = Random.Range(effectSet.min,effectSet.max);
			int i;
			for(i=0;i<propertyNum;i++)
			{
				effects[i] = effectSet.effects[i];
			}
			for(;i<effectSet.effects.Length;i++)
			{
				int j = Random.Range(0,i+1);
				if(j<propertyNum)
				{
					effects[j] = effectSet.effects[i]; 
				}
			}
		}
	}
}
public enum EquipState{Template,Wearable}