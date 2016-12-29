using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassesData : MonoBehaviour {

	public string classID;
	public Sprite iconSprite;
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

	public CharacterData getChData()
	{
		CharacterData chData = new CharacterData();
		chData.actionIDs = getRandomActionIDs();
		chData.classID = classID;
		//TODO: temp fixed as first
		chData.helmet = new EquipData(helmetIDs[0]);
		chData.armor = new EquipData(armorIDs[0]);
		chData.weapon = new EquipData(weaponIDs[0]);
		chData.shield = new EquipData(shieldIDs[0]);
		return chData;
	}

	List<string> getRandomActionIDs()
	{
		var actionIDs = mergeActionsList();
		var randomActions = new List<string>();
		for(int i=0;i<actionNumPerClass;i++)
		{
			int r = Random.Range(0,actionIDs.Count);
			randomActions.Add(actionIDs[r]);
			actionIDs.RemoveAt(r);
		}
		return randomActions; 
	}
}
