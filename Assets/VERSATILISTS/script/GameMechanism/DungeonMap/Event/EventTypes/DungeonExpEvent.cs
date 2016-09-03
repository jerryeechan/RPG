using UnityEngine;
using System.Collections;

public class DungeonExpEvent : DungeonEvent {

	void Awake()
	{
		confirmText = "RIP";
		cancelText = "absorb";
		descriptionText = "You found a lonely soul..";
	}
	override public void confirm(){
		base.confirm();
		DiceRoller2D.instance.Roll(gamble);
		//enter battle
		
	}
	override public void cancel()
	{
		base.cancel();
		foreach(var chUI in DungeonPlayerStateUI.instance.chUIs)
			chUI.getExp(5);
		DungeonManager.instance.dungeonEventComplete(true);

	}

	void gamble(int num)
	{

	}
}
