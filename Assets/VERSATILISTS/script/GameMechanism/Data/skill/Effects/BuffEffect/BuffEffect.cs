using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.jerrch.rpg;
public class BuffEffect : SkillEffect{
	[SerializeField]
	float baseMultiplier = 1; //1: does not change
	public FinalBonus bonus;
	public AttributeType buffType;	

	protected override void Awake()
	{
		base.Awake();
		bonus = new FinalBonus((int)initValue,baseMultiplier);
	}
	public override void ApplyOn(CharacterStat stat)
	{
		base.ApplyOn(stat);
			onStat.getAttribute(buffType).addFinalBonus(bonus);
	}
	public override void RemoveEffect()
	{
		onStat.getAttribute(buffType).removeFinalBonus(bonus);
		base.RemoveEffect();
	}

	public string description()
	{
		
		return "Target:"+effectRange.ToString()+"持續"+duration+"回合";
	}
}

public enum AttributeType{
	PhyAtk,MagAtk,
	PhyDamage,MagDamage,
	PhyDefense,MagDefense,
	Accuracy,Evasion,
	DamageReduce,
	Movable,
	CriticalRate,
	CriticalDmg
}