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
		baseValue = (100f+5*level)/100f;
		//mpCost = level*1;
		spCost = level*2;
		base.setLevel(level);
	}
	public override void useBy(Character ch)
	{
		base.useBy(ch);
		calEffectValue = baseValue;
		print("baseValue:"+baseValue);
		calEffectValue += casterStat.strValue/5;
		calEffectValue *= casterStat.phyAtk;
		print("phyAtk:"+casterStat.phyAtk);
		calEffectValue *= casterStat.phyDmgBuff;
		print("dmgBuff:"+casterStat.phyDmgBuff);
		print("buff and atk cal:"+calEffectValue);
		calEffectValue *= Random.Range(casterStat.lowestPhyDmg,1);
		calEffectValue *= parentSkill.criticalBonus;
		print("damage cal:"+calEffectValue);
		
	}
	public override void ApplyOn (CharacterStat stat)
	{
		base.ApplyOn(stat);
		float defense = stat.phyDefense/5;
		defense *= (1-casterStat.ignorePhyDefensePer);//caster's ignore defense
		print("defense:"+defense);
		
		applyResult = Mathf.Clamp(calEffectValue-defense,1,10000);
		//print(" casue damage: "+(int)applyResult);
		print(casterStat.chName+" do @"+(int)applyResult+" damage to "+stat.chName);
		stat.hp -= (int)applyResult;
	}	
}