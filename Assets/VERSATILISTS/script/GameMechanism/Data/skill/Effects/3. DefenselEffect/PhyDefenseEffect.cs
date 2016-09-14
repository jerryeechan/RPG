using UnityEngine;
using System.Collections;

public class PhyDefenseEffect : SkillEffect {


	public override void ApplyOn (CharacterStat stat)
	{
		base.ApplyOn (stat);
		stat.phyDef+=calEffectValue;
		duration = 1;
	}
	public override void RemoveEffect ()
	{
		base.RemoveEffect();
		onStat.phyDef-=calEffectValue;
		//Debug.Log("Defense over");
	}
}
