using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.jerrch.rpg;
public class ReviveEffect : SkillEffect {

	public override void useBy(Character ch)
	{
        
		//int _int = ch.battleStat.intAttr.finalValue; 
		//int _con = ch.battleStat.conAttr.finalValue; 
		calEffectValue = initValue;
	}
	// Update is called once per frame
	public override void ApplyOn (CharacterStat stat)
	{
        
		base.ApplyOn(stat);
		stat.hp.changeHP((int)calEffectValue);	
	}
}
