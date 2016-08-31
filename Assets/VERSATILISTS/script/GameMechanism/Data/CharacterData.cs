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
	public int strVal;
	public int intVal;
	public int dexVal;
	public int hp = 50;
	public int mp = 10;
	public int sp = 10;
	
	public string UITemplateID="";
	public List<ActionData> currentActionData = new List<ActionData>();
	public List<ActionData> availableActionData = new List<ActionData>();

	public EquipData weapon; 
	public EquipData helmet; 
	public EquipData shield;
	public EquipData armor;  
	EquipData[] equipDatas;
	
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
		equipDatas = new EquipData[4];
		equipDatas[0] = helmet;
		equipDatas[1] = weapon;
		equipDatas[2] = shield;
		equipDatas[3] = armor;
		//return Character;
		Character ch = CharacterManager.instance.generateCharacter(name,UITemplateID);
		//statValues["str"] = 1;
		ch.chData = this;
		ch.init(hp,mp,sp,strVal,intVal,dexVal);
		genAndWearEquip(ch);
		ch.actionDataList = currentActionData;
		return ch;
	}

	public void genAndWearEquip(Character ch)
	{
		foreach(EquipData eqData in equipDatas)
		{
		   	Equip eq = eqData.genEquip();
			if(eq)
				ch.wear(eq);
		}
	}

}