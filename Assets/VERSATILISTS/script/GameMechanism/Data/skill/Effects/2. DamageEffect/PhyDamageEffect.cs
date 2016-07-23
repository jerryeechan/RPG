using UnityEngine;
using System.Collections;

public class PhyDamageEffect : SkillEffect {
	override protected void Reset()
	{
		base.Reset();
		parentSkill.mainEffect = this;
	}
	public override void setLevel (int level)
	{
		baseValue = (100+20*level)/100;
		//mpCost = level*1;
		spCost = level*2;
		base.setLevel(level);
	}
	public override void useBy(Character ch)
	{
		base.useBy(ch);
		calEffectValue = baseValue;
		calEffectValue *= casterStat.phyAtk;
		calEffectValue *= casterStat.phyDmgBuff;
		calEffectValue *= Random.Range(casterStat.lowestPhyDmg,1);
		calEffectValue *= parentSkill.criticalBonus;
		
	}
	public override void ApplyOn (CharacterStat stat)
	{
		base.ApplyOn(stat);
		float defense = stat.phyDefense/5;
		defense *= (1-casterStat.ignorePhyDefensePer);//caster's ignore defense
		
		
		applyResult = Mathf.Clamp(calEffectValue-defense,1,10000);
		print(casterStat.statname+" casue damage: "+(int)applyResult);
		ActionLogger.Log(casterStat.statname+"造成傷害"+(int)applyResult);
		stat.hp -= (int)applyResult;
	}	
}