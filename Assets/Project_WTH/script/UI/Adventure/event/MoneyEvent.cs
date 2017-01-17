using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyEvent : EffectEvent{
	
	public override void doEffect()
	{
		PlayerStateUI.instance.getGold(baseValue);
	}
}
