using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.jerrch.rpg;
public class ClassesData : MonoBehaviour {

	public string classID;
	public Sprite iconSprite;

	public string description;
	public string specialty;
	//public List<Skill> attackSkillCandidates;
	//public List<Skill> supportSkillCandidates;
	// pub/lic List<Skill> specialSkillCandidates;
	[SerializeField]
	Transform skillTemplates;
	
	public string[] helmetIDs;
	public string[] helmetGraphicIDs;
	public string[] armorIDs;
	public string[] armorGraphicIDs;
	public string[] weaponIDs;
	public string[] weaponGraphicIDs;
	public string[] shieldIDs;
	public string[] shieldGraphicIDs;
	
	void OnValidate()
	{
		//init();
	}
	Dictionary<SkillDiceType,List<Skill>> candidatesDict;
	void init()
	{
		candidatesDict = new Dictionary<SkillDiceType,List<Skill>>();
		candidatesDict.Add(SkillDiceType.Attack,new List<Skill>());
		candidatesDict.Add(SkillDiceType.Support,new List<Skill>());
		candidatesDict.Add(SkillDiceType.Special,new List<Skill>());
		if(skillTemplates!=null)
		{
			var skills = skillTemplates.GetComponentsInChildren<Skill>();
			foreach(var skill in skills)
			{
				candidatesDict[skill.diceType].Add(skill);
			}
		}
	}
	void Awake()
	{
		init();
	}
	List<Skill> mergeSkillsList()
	{
		List<Skill> allSkillIDs = new List<Skill>();
		foreach(var v in candidatesDict)
		{
			//if there are more skills, random select of each type
			allSkillIDs.AddRange(v.Value);
		}
		return allSkillIDs;
	}
	const int skillNumPerClass = 3;

	public int strSeed;
	public int intSeed;
	public int conSeed;
	public int dexSeed;
	public CharacterData generateChData()
	{
		//generate the data of new character
		CharacterData chData = new CharacterData();
		chData.nickName = TextTokenGenerator.instance.boyName();
		
		chData.classID = classID;
		//TODO: temp fixed as first
		chData.helmet = new EquipData(helmetIDs[0],0);
		chData.armor = new EquipData(armorIDs[0],0);
		chData.weapon = new EquipData(weaponIDs[0],0);
		chData.shield = new EquipData(shieldIDs[0],0);
		chData.skillDatas = new List<SkillData>();

		var skillIDs = getRandomSkillIDs();
		for(int i=0;i<skillIDs.Count;i++)
		chData.skillDatas.Add(new SkillData(skillIDs[i]));

		chData.conVal = conSeed;
		chData.dexVal = dexSeed;
		chData.intVal = intSeed;
		chData.strVal = strSeed;
		return chData;
	}

	List<string> getRandomSkillIDs()
	{
		var randomSkills = mergeSkillsList();
		var ids = new List<string>();
		for(int i=0;i<3;i++)
		{
			ids.Add(randomSkills[i].id);
		}
		
		//randomSkills.Shuffle();
		
		// randomSkills.Sort();
		return ids;
	}
}
public enum ClassesType{
	None,
	Paladin,DarkPalidin,
	Mage,Cleric,Taoist
}



/*
		for(int i=0;i<skillNumPerClass;i++)
		{
			int r = Random.Range(0,skillIDs.Count);
			randomSkills.Add(skillIDs[r]);
			skillIDs.RemoveAt(r);
		}*/