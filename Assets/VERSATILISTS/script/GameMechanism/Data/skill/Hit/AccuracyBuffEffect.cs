using UnityEngine;
using System.Collections;

public class AccuracyBuffEffect : SkillEffect {

	// Use this for initialization
	public override void useBy(Character ch)
	{
		calEffectValue = baseValue;
	}
	public override void ApplyOn(CharacterStat stat)
	{
		stat.accuracy += calEffectValue;
	}
	
	public override void RemoveEffect(Character ch)
	{
		onStat.accuracy -= calEffectValue;
		base.RemoveEffect(ch);
	}
}
