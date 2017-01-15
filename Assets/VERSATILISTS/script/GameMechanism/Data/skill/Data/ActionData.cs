using UnityEngine;
using System.Collections;
using com.jerrch.rpg;

[System.SerializableAttribute]
public class ActionData {
	public ActionData(string id)
	{
		this.id = id;
	}
	//the skill data of characters, can be saved and loaded
	public string id;

	public int level;
	
	//public ActionState state = ActionState.Locked;
	//int rank;//1~6, can be put on dice

	public int masterLevel = 0;
	static int masterLevelMax = 100;
	public ActionState state;	
	/*
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
	}*/
	
}

public enum ActionState{
	Locked,Avalible,Learned,Mastered
}