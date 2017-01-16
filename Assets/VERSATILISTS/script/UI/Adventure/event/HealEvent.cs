using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.jerrch.rpg;
public class HealEvent : EffectEvent {

	public override void doEffect()
	{
		foreach(var ch in GameManager.instance.chs)
		{
			ch.battleStat.hp.changeHP(baseValue);
			ch.updateRenderer();
		}
		
	}
}
