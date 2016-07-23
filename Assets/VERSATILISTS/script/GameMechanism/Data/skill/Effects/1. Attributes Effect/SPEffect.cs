using UnityEngine;
using System.Collections;

public class SPEffect : SkillEffect{

	public override void useBy(Character ch)
	{
		calEffectValue = baseValue;
	}
	override public void ApplyOn(CharacterStat stat)
	{
		base.ApplyOn(stat);
		stat.sp+=(int)calEffectValue;
	}
	
}
