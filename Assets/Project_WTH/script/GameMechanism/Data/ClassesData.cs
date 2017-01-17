using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassesData : MonoBehaviour {

	public string classID;
	public Sprite iconSprite;

	public string description;
	public string specialty;
	public string[] attackActionCandidateIDs;
	public string[] defenseActionCandidateIDs;
	public string[] specialActionCandidateIDs;
	
	public string[] helmetIDs;
	public string[] helmetGraphicIDs;
	public string[] armorIDs;
	public string[] armorGraphicIDs;
	public string[] weaponIDs;
	public string[] weaponGraphicIDs;
	public string[] shieldIDs;
	public string[] shieldGraphicIDs;
	
	void Awake()
	{
		
	}
	List<string> mergeActionsList()
	{
		List<string> allActionIDs = new List<string>();
		allActionIDs.AddRange(attackActionCandidateIDs);
		allActionIDs.AddRange(defenseActionCandidateIDs);
		allActionIDs.AddRange(specialActionCandidateIDs);
		return allActionIDs;
	}
	const int actionNumPerClass = 3;

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
		chData.actionDatas = new List<ActionData>();

		var actionIDs = getRandomActionIDs();
		for(int i=0;i<actionIDs.Count;i++)
		chData.actionDatas.Add(new ActionData(actionIDs[i]));

		chData.conVal = conSeed;
		chData.dexVal = dexSeed;
		chData.intVal = intSeed;
		chData.strVal = strSeed;
		return chData;
	}

	List<string> getRandomActionIDs()
	{
		var randomActions = mergeActionsList();
		//randomActions.Shuffle();
		
		// randomActions.Sort();
		return randomActions.GetRange(0,3);
	}
}

/*
		for(int i=0;i<actionNumPerClass;i++)
		{
			int r = Random.Range(0,actionIDs.Count);
			randomActions.Add(actionIDs[r]);
			actionIDs.RemoveAt(r);
		}*/