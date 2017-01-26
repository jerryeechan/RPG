using UnityEngine;
using System.Collections;
using com.jerrch.rpg;

[System.SerializableAttribute]
public class SkillData {
	public SkillData(string id)
	{
		this.id = id;
	}
	//the skill data of characters, can be saved and loaded
	public string id;

	public int level;
	
	//public SkillState state = SkillState.Locked;
	//int rank;//1~6, can be put on dice

	public int masterLevel = 0;
	static int masterLevelMax = 100;
	public SkillState state;	
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

	public Skill getSkillRef()
	{
		Skill skill = SkillManager.instance.getSkill(id);
		return skill;
	}*/
	
}

public enum SkillState{
	Locked,Avalible,Learned,Mastered
}