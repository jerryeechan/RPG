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
	
	public string UITemplateID="player";
	//public List<ActionData> currentActionData = new List<ActionData>();
	//public List<string> actionIDs = new List<string>();
	//TODO: complex with actionBundle, add feature to action of each ch.
	public List<ActionData> actionDatas;

	public EquipData weapon; 
	public EquipData helmet; 
	public EquipData shield;
	public EquipData armor;  
	EquipData[] equipDatas;
	
	//public List<Equip> currentEquips;
	// public List<Equip> availableEquips;

	/*
	public void knowNewActionData(string name)
	{
		ActionData skilldata = new ActionData(name);
		availableActionData.Add(skilldata);
	}

	public void replaceCurrentSkillData(ActionData target, ActionData replaceWith)
	{
		currentActionData.Remove(target);
		currentActionData.Add(replaceWith);
	}
	*/
	public Character genCharacter()
	{
		equipDatas = new EquipData[4];
		equipDatas[0] = helmet;
		equipDatas[1] = weapon;
		equipDatas[2] = shield;
		equipDatas[3] = armor;	
		Character ch = CharacterManager.instance.generateCharacter(name,UITemplateID);
		//foreach()
		//actionDatas[i]
		ch.bindData = this;
		ch.init(genStat(),genEquips(),ActionManager.instance.getActions(actionDatas));
		
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
		   	Equip eq = eqData.genEquip();
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