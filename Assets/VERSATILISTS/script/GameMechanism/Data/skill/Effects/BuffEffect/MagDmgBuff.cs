using UnityEngine;
using System.Collections;

public class MagDmgBuff : SkillEffect {

	/*
	Magical Damage Buff
	Effect: Increase Magical Damage in a time duration
	level:1~5
	value:120,140,160,180,200
	duration:
	*/
	
	
	
	public enum DamageType{Physic,Melee,Direct};
	
	public override void setLevel (int level)
	{
		baseValue = (120+20*level)/100;
		mpCost = level*1;
		duration = 3+level*2;
		base.setLevel(level);
	}
	public override void ApplyOn (CharacterStat stat)
	{
		base.ApplyOn (stat);
		stat.magDmgBuff+=calEffectValue;
		Debug.Log("magdamageBuff");
	}
	public override void RemoveEffect (Character ch)
	{
		base.RemoveEffect(ch);
		if(ch.battleStat!=onStat)
			onStat.magDmgBuff-=calEffectValue;
	}
}
