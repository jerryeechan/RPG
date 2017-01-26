using UnityEngine;
using System.Collections.Generic;
using com.jerrch.rpg;
public class SkillManager : Singleton<SkillManager> {

	public static int skill_max_num = 3;
	Dictionary<string,Skill> skillPool;
	public Skill[] skills;
	//Dictionary<string,SkillData> skillDataPool;
	
	void Awake()
	{
		skillPool = new Dictionary<string,Skill>();
		skills = GetComponentsInChildren<Skill>();		
		foreach(Skill skill in skills)
		{
			skillPool.Add(skill.name,skill);
		}
	}

	public List<Skill> getSkills(List<SkillData> dataList)
	{
		List<Skill> skillList = new List<Skill>();
		int i = 0;
		foreach(var data in dataList)
		{
			if(skillPool.ContainsKey(data.id))
			{
				var skill = Instantiate(skillPool[data.id]);
				skill.level = data.level;
				skillList.Add(skill);
				i++;
			}
			else{
				Debug.LogError(data.id+":Character's skill does not exist anymore");
			}
		}
		for(;i<skill_max_num;i++)
		{
			skillList.Add(null);
		}
		return skillList;
	}

	public Skill GenSkill(string name)
	{
		if(skillPool[name])
		{
			Skill skill = Instantiate(skillPool[name]);
			return skill;
		}	
		else
		{
			Debug.LogError("No skill:"+name);
			return null;
		}
		
	}
	public Skill getSkill(string name)
	{
		if(skillPool.ContainsKey(name))
		{
			Skill skill = skillPool[name];
			return skill;
		}
		else
		{
			Debug.LogError("No skill:"+name);
			return null;
		}
	}

	public static Color getDiceTypeColor(SkillDiceType type)
	{
		switch(type)
		{
			case SkillDiceType.Attack:
				return new Color32(188,51,51,255);
			case SkillDiceType.Support:
				return new Color32(73,149,212,255);
			case SkillDiceType.Special:
				return new Color32(255,247,155,255);
			default:
				return new Color32(188,51,51,255);
		}
	}
}
