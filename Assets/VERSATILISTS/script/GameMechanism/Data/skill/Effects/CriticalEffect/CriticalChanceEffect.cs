using UnityEngine;
using System.Collections;

namespace com.jerrch.rpg
{
	public class CriticalChanceEffect : ValueEffect {

	public float rate;
	
	public override void ApplyOn (CharacterStat stat)
	{
		base.ApplyOn (stat);
		stat.criticalRate+=rate;
		
	}
	public override void setLevel (int level)
	{
		rate = level*3;
	}
	public override void RemoveEffect()
	{
		//base.RemoveFrom (stat);
		onStat.criticalRate-=rate;
		base.RemoveEffect();
	}
}
}