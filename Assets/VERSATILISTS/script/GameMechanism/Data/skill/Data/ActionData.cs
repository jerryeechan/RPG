using UnityEngine;
using System.Collections;
using com.jerry.rpg;
public enum ActionType
{
	Melee,Magic
}
public enum ActionState{
	Locked,Learned,Mastered
}
[System.SerializableAttribute]
public class ActionData {
	public ActionData(string id)
	{
		this.id = id;
	}
	//the skill data of characters, can be saved and loaded
	public string id;
	public ActionRequirement[] requirements;
	ActionState state = ActionState.Locked;
	int rank;//1~6, can be put on dice
	//int availableRank = 1;
	
	public int energyCost = 1;
	public int cd = 0;
	public int cd_count;

	int masterLevel = 0;
	static int masterLevelMax = 100;

	
	
	int genDiceAvailableMask(int from, int to)
	{
		int mask = 0;
		for(int i=from;i<to;i++)
		{
			mask &= (1<<i);
		}
		return mask;
	}
	float masterBouns{
		get{
			return 1+(float)masterLevel/100;
		}
	}
	
	public void practiceUse()
	{
		if(masterLevel<masterLevelMax)
			masterLevel++;
	}
	public void practiceKill()
	{
		if(masterLevel<masterLevelMax)
			masterLevel+=2;
	}

	public Action getActionRef()
	{
		Action action = ActionManager.instance.getAction(id);
		return action;
	}
	public Action genAction(Character ch)
	{
		Action action = ActionManager.instance.GenAction(id);
		action.actionData = this;
		action.caster = ch;
		return action;
	}
}
