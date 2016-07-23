using UnityEngine;
using System.Collections;

public class PhyDefenseEffect : SkillEffect {


	public override void ApplyOn (CharacterStat stat)
	{
		base.ApplyOn (stat);
		stat.phyDefense+=calEffectValue;
		duration = 1;
	}
	public override void RemoveEffect (Character ch)
	{
		base.RemoveEffect(ch);
		onStat.phyDefense-=calEffectValue;
		//Debug.Log("Defense over");
	}
}
