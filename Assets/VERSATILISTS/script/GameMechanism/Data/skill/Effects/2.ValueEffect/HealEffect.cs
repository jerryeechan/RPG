using UnityEngine;
using System.Collections;
namespace com.jerrch.rpg
{
public class HealEffect :SkillEffect {

	// Use this for initialization
	public override void useBy(Character ch)
	{
		calEffectValue = baseValue*ch.battleStat.intAttr.finalValue;
	}
	// Update is called once per frame
	public override void ApplyOn (CharacterStat stat)
	{
		base.ApplyOn(stat);
		stat.hp.changeHP((int)calEffectValue);	
	}
}

}