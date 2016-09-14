using UnityEngine;
using System.Collections;

public class PhyDmgBuff : SkillEffect {

	/*
	Physical Damage Buff
	Effect: Increase Physical Damage with all in a time duration
	level:1~5
	value:120,140,160,180,200
	duration:
	*/
	
	
	
//	public enum DamageType{Physic,Melee,Direct};
	
	public new void setLevel (int level)
	{
		baseValue = 120+20*level;
		mpCost = level*1;
		duration = 8+level*2;
		base.setLevel(level);
	}
	public override void useBy(Character ch)
	{
		//base.useBy(ch);
		calEffectValue = baseValue;
	}
	public override void ApplyOn (CharacterStat stat)
	{
		base.ApplyOn (stat);
		stat.phyDmgBuff += calEffectValue;
		Debug.Log("phydamageBuff");
		
	}
	public override void RemoveEffect()
	{
		base.RemoveEffect();
		//base.RemoveFrom (stat);
		onStat.phyDmgBuff-=calEffectValue;
	}
}
