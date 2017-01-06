using UnityEngine;
using System.Collections;
namespace com.jerrch.rpg
{

	//skill value effect 
	public class MagDamageEffect :SkillEffect{

		/*
		Magical Damage Effect
		The percentage of the character's magical attack
		
		*/
		public override void setLevel (int level)
		{
			baseValue = 100+25*level;
			mpCost = level*2;
			base.setLevel(level);
		}
		public override void useBy(Character ch)
		{
			calEffectValue = ch.battleStat.magDmg.finalValue * baseValue;
			calEffectValue *= Random.Range(casterStat.lowestMagDmg,1);
			//calEffectValue *= parentSkill.criticalBonus;
			base.useBy(ch);
		}
		public override void ApplyOn (CharacterStat stat)
		{
			base.ApplyOn(stat);
			
			int defense = stat.magDef.finalValue/5;//defense
			//defense *= (1-casterStat.ignoreMagDefensePer);//caster's ignore defense
			applyResult = Mathf.Clamp(calEffectValue-defense,1,10000);
			stat.hp.changeHP(-(int)applyResult);
		}
	}
}