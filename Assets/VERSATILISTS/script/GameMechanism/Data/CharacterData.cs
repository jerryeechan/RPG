using UnityEngine;
using System.Collections.Generic;
using System;

[SerializableAttribute]
public class CharacterData{

	
	public string name;
	public int level = 0;
	public int exp = 0;
	public int strValue = 1;
	public int intValue = 1;
	public int dexValue = 1;
	public int hp = 50;
	public int mp = 10;
	public int sp = 10;
	public string UITemplateID="";
	public List<ActionData> currentActionData = new List<ActionData>();
	public List<ActionData> availableActionData = new List<ActionData>();

	public EquipData weapon; 
	public EquipData helmet; 
	
	//public List<Equip> currentEquips;
	// public List<Equip> availableEquips;
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
	
	public Character genCharacter()
	{
		//return Character;
		Character ch = CharacterManager.instance.generateCharacter(name,UITemplateID);
		ch.init(hp,mp,sp,strValue,intValue,dexValue);
		ch.initStat.strValue = strValue;
		ch.initStat.strValue = intValue;
		ch.initStat.dexValue = dexValue;
		
		ActionLogger.Log(ch.name);
		ch.actionData = currentActionData;
		return ch;
	}
	
}
