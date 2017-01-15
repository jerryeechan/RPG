using UnityEngine;
using System.Collections;
using com.jerrch.rpg;
public class SoulItem : ConsumableItem {

	public override void use()
	{
		StatChUIManager.instance.getAbilityPoints(3);
	}

	//consnum? skill points?
}
