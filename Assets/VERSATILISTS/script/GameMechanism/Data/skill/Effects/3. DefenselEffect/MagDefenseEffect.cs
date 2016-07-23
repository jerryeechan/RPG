using UnityEngine;
using System.Collections;

public class MagDefenseEffect : SkillEffect {


	public override void ApplyOn (CharacterStat stat)
	{
		base.ApplyOn (stat);
		stat.magDefense+=calEffectValue;
		duration = 1;
	}
	public override void RemoveEffect (Character ch)
	{
		base.RemoveEffect(ch);
		onStat.magDefense-=calEffectValue;
		//Debug.Log("Defense over");
	}
}
