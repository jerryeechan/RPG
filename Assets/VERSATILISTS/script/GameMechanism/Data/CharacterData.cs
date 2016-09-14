using UnityEngine;
using System.Collections.Generic;
using System;

[SerializableAttribute]
public class CharacterData:StringfyProperty{
	
	public string name;
	public int level = 1;
	public int exp = 0;
	//public Dictionary<string,int> statValues = new Dictionary<string,int>(){{"str",1},{"int",1},{"dex",1}};
	//public SerializableDictionary<string,int> statValues;
	
	public int statPoints;
	public int strVal = 5;
	public int conVal = 5;
	public int intVal = 5;
	public int dexVal = 5;
	
	public string UITemplateID="";
	//public List<ActionData> currentActionData = new List<ActionData>();
	public List<string> actionIDs = new List<string>();
	//TODO: complex with actionBundle, add feature to action of each ch.

	//public List<ActionData> availableActionData = new List<ActionData>();

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
		//return Character;
		Character ch = CharacterManager.instance.generateCharacter(name,UITemplateID);
		//statValues["str"] = 1;
		ch.chData = this;		
		ch.init(genStat(),genEquips(),ActionManager.instance.getActions(actionIDs));
		
		return ch;
	}
	public CharacterStat genStat()
	{
		CharacterStat stat = new CharacterStat(name);
		stat.statname = "initstat";
		stat.strValue = strVal;
		stat.dexValue = dexVal;
		stat.intValue = intVal;
		stat.conValue = conVal;
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