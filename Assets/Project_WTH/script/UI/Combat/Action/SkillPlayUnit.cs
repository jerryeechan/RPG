using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.jerrch.rpg;
public class SkillPlayUnit{
	
	Queue<SkillPair> skillPairQueue = new Queue<SkillPair>();
	
	public Queue<SkillPair> skillPairs
	{
		get{
			return skillPairQueue;
		}
	}
	public int maximum = 1;
	public void AddSkill(Character ch,Skill skill)
	{
		
		if(skillPairQueue.Count==maximum)
		{
			skillPairQueue.Dequeue();
		}
		skillPairQueue.Enqueue(new SkillPair(ch,skill));

	}
}
public class SkillPair
{
	public SkillPair(Character ch,Skill skill)
	{
		this.skillTemplate = skill;
		this.ch = ch;
	}
	Skill skillTemplate;
	Character ch;
	public void PlaySkill(OnCompleteDelegate completeFunc)
	{	
		if(!ch.isDead)
		{
			if(ch.side == CharacterSide.Player)
				ch.chRenderer.selected();
			if(ch.battleStat.movable.randomCheck())
			{
				Skill skill = GameObject.Instantiate(skillTemplate);
				skill.caster = ch;
				ch.doSkillMove(skill.chAnimation);
				skill.PlaySkill(completeFunc);
			}			
			else{
				//can't move
				Debug.Log("can't move");	
			}
		}
		else
			completeFunc();
	}
}