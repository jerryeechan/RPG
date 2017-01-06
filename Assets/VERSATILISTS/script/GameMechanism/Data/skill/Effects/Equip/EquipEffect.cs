using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.jerrch.rpg;
public class EquipEffect :SkillEffect {
	public RawBonus bonus;
	public AttributeType buffType;
	public override void ApplyOn(CharacterStat stat)
	{
		stat.getAttribute(buffType).addRawBonus(bonus);
	}
	public override void RemoveEffect()
	{
		onStat.getAttribute(buffType).removeRawBonus(bonus);
		base.RemoveEffect();
	}

}
