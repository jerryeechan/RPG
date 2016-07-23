using UnityEngine;
using System.Collections;

public class CriticalEffect : ValueEffect {

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
	public override void RemoveEffect(Character ch)
	{
		//base.RemoveFrom (stat);
		onStat.criticalRate-=rate;
	}
}
