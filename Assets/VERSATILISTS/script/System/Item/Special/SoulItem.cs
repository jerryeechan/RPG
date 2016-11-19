using UnityEngine;
using System.Collections;
using com.jerrch.rpg;
public class SoulItem : ConsumableItem {

	public override void use()
	{
		DataManager.instance.curPlayerData.curChData.statPoints++;
	}
	public void consume()
	{
		
	}
}
