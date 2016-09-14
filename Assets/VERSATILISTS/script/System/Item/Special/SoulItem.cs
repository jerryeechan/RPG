using UnityEngine;
using System.Collections;

public class SoulItem : ConsumableItem {

	public override void use()
	{
		DataManager.instance.curPlayerData.curChData.statPoints++;
	}
	public void consume()
	{
		
	}
}
