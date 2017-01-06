using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.jerrch.rpg;
public class BuffEffect : SkillEffect{
	[SerializeField]
	float baseMultiplier;
	public FinalBonus bonus;
	public AttributeType buffType;
	protected override void Awake()
	{
		base.Awake();
		bonus = new FinalBonus((int)baseValue,baseMultiplier);
	}
	public override void ApplyOn(CharacterStat stat)
	{
		stat.getAttribute(buffType).addFinalBonus(bonus);
	}
	public override void RemoveEffect()
	{
		onStat.getAttribute(buffType).removeFinalBonus(bonus);
		base.RemoveEffect();
	}
}

public enum AttributeType{
	PhyAtk,MagAtk,
	PhyDamage,MagDamage,
	PhyDefense,MagDefense,
	Accuracy,Evasion,
}