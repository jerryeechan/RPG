using UnityEngine;
using System.Collections;
namespace com.jerrch.rpg
{
	public class PhyDamageEffect : SkillEffect {
		override protected void Awake()
		{
			parentSkill.mainEffect = this;
		}
			
		
		public override void setLevel (int level)
		{
			//one level 10 %;
			initValue = baseValue * (1+level*0.1f);
			//mpCost = level*1;
			//spCost = level*2;
			base.setLevel(level);
		}
		public override void useBy(Character ch)
		{
			base.useBy(ch);
			calEffectValue = baseValue * ch.battleStat.phyDmg.finalValue;
			
			print("baseValue:"+baseValue);

			calEffectValue *= Random.Range(casterStat.lowestPhyDmg,1);
			//calEffectValue *= parentSkill.criticalBonus;
			calEffectValue *= ch.battleStat.critDmg.finalValue;
		}

		int phyDmgMax = 10000;
		public override void ApplyOn (CharacterStat stat)
		{
			base.ApplyOn(stat);
			int defense = stat.phyDef.finalValue/5;
			//defense *= (1-casterStat.ignorePhyDefensePer);//caster's ignore defense
			//print("defense:"+defense);
			
			int reduce = stat.damageReduce.finalValue;
			applyResult = Mathf.Clamp((calEffectValue-defense)*reduce,1,phyDmgMax);
			//print(" casue damage: "+(int)applyResult);
			print(casterStat.chName+" do @"+(int)applyResult+"damage to"+stat.chName);
			stat.hp.changeHP(-(int)applyResult);
		}	
	}
}