using UnityEngine;
using System.Collections;

public class MagDefenseEffect : SkillEffect {


	public override void ApplyOn (CharacterStat stat)
	{
		base.ApplyOn (stat);
		stat.magDef+=calEffectValue;
		duration = 1;
	}
	public override void RemoveEffect ()
	{
		base.RemoveEffect();
		onStat.magDef-=calEffectValue;
		//Debug.Log("Defense over");
	}
}
