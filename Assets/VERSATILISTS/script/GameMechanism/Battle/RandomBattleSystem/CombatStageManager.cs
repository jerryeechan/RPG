using UnityEngine;
using System.Collections.Generic;
namespace com.jerrch.rpg
{
public class CombatStageManager : Singleton<CombatStageManager> {
	Dictionary<string,CombatStage> combatStageDict;
	CombatStage defaultStage;
	void Awake()
	{
		CombatStage[] stages = GetComponentsInChildren<CombatStage>(true);
		combatStageDict = new Dictionary<string,CombatStage>();
		foreach(var stage in stages)
		{
			combatStageDict.Add(stage.name,stage);
		}
		defaultStage = combatStageDict["default"];
	}
	public CombatStage getStage(string name)
	{
		CombatStage stage;
		if(combatStageDict.ContainsKey(name))
		{
			 stage = combatStageDict[name];
		}
		else{
			stage = defaultStage;
		}
		stage.gameObject.SetActive(true);
		return stage;
	}

	public CombatStage getTestStage()
	{
		defaultStage.gameObject.SetActive(true);
		return defaultStage;
	}
}

}