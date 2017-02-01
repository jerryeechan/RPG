using UnityEngine;
using System.Collections.Generic;
using System;
using com.jerrch.rpg;
[SerializableAttribute]
public class CharacterData:StringfyProperty{
	
	public string name;
	public string nickName;
	public string classID;
	public int level = 1;
	public int exp = 0;
	//public Dictionary<string,int> statValues = new Dictionary<string,int>(){{"str",1},{"int",1},{"dex",1}};
	//public SerializableDictionary<string,int> statValues;
	
	public int abilityPoints;
	public int skillPoints;
	public int strVal;
	public int conVal;
	public int intVal;
	public int dexVal;
	public int sanVal;
	
	public string UITemplateID="player";
	//public List<SkillData> currentSkillData = new List<SkillData>();
	//public List<string> skillIDs = new List<string>();
	//TODO: complex with skillBundle, add feature to skill of each ch.
	public List<SkillData> skillDatas;

	public EquipData weapon; 
	public EquipData helmet; 
	public EquipData shield;
	public EquipData armor;  
	EquipData[] equipDatas;
	
	//public List<Equip> currentEquips;
	// public List<Equip> availableEquips;

	/*
	public void knowNewSkillData(string name)
	{
		SkillData skilldata = new SkillData(name);
		availableSkillData.Add(skilldata);
	}

	public void replaceCurrentSkillData(SkillData target, SkillData replaceWith)
	{
		currentSkillData.Remove(target);
		currentSkillData.Add(replaceWith);
	}
	*/
	public Character genCharacter()
	{
		equipDatas = new EquipData[4];
		equipDatas[0] = helmet;
		equipDatas[1] = weapon;
		equipDatas[2] = shield;
		equipDatas[3] = armor;	
		Character ch = CharacterManager.instance.generateCharacter(UITemplateID);
		ch.name = nickName;
		//foreach()
		//skillDatas[i]
		ch.bindData = this;
		abilityPoints = 5;
		skillPoints = 3;
		ch.init(genStat(),genEquips(),SkillManager.instance.getSkills(skillDatas));
		
		return ch;
	}
	public CharacterStat genStat()
	{
		CharacterStat stat = new CharacterStat(name,strVal,intVal,dexVal,conVal);
		stat.statname = "initstat";
		
		return stat;
	}
	public List<Equip> genEquips()
	{
		List<Equip> equips = new List<Equip>();

		foreach(EquipData eqData in equipDatas)
		{
		   	Equip eq = eqData.getItem();
			if(eq)
				equips.Add(eq);
		}
		return equips;
	}
	/*
	public void genAndWearEquip(Character ch)
	{
		foreach(EquipData eqData in equipDatas)
		{
		   	Equip eq = eqData.genEquip();
			if(eq)
				ch.wear(eq);
		}
	}
	*/

}