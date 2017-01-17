using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.jerrch.rpg;
public class EquipEffect :SkillEffect {
	public RawBonus bonus;
	public AttributeType buffType;
	protected override void OnValidate()
	{
		base.OnValidate();
		effectType = EffectType.EquipEffect;
		effectRange = EffectRange.Self;
	}
	protected override void Awake()
	{
		base.Awake();
		bonus = new RawBonus((int)initValue);
	}

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
