using UnityEngine;
using System.Collections;
namespace com.jerrch.rpg
{
	public class PhyDamageEffect : SkillEffect {
		override protected void Awake()
		{
			parentSkill.mainEffect = this;
		}
		public override void useBy(Character ch)
		{
			base.useBy(ch);
			calEffectValue = ch.battleStat.phyDmg.finalValue;
			Debug.LogError(calEffectValue);
			calEffectValue *= Random.Range(casterStat.lowestPhyDmg,1)*initValue;
			//calEffectValue *= parentSkill.criticalBonus;
			calEffectValue *= (1+ch.battleStat.critDmg.finalValue);
		}

		int phyDmgMax = 10000;
		public override void ApplyOn (CharacterStat stat)
		{
			base.ApplyOn(stat);
			int defense = stat.phyDef.finalValue/5;
			//defense *= (1-casterStat.ignorePhyDefensePer);//caster's ignore defense
			
			
			//int reduce = stat.damageReduce.finalValue;//TODO
			applyResult = Mathf.Clamp((calEffectValue-defense),1,phyDmgMax);
			
			print(casterStat.chName+"do"+calEffectValue+" def:"+defense+"result: @"+(int)applyResult+"damage to"+stat.chName);
			stat.hp.changeHP(-(int)applyResult);
		}	
		public string description()
		{
			return CompositeText.GetLocalText("造成物理傷害")+initValue*100+"%";
		}
	}
}