using UnityEngine;
using System.Collections;

public class DungeonExpEvent : DungeonEvent {

	override public void confirm(){
		base.confirm();
		DiceRoller2D.instance.Roll(gamble);
		//enter battle
		
	}
	override public void cancel()
	{
		base.cancel();
		DungeonPlayerStateUI.instance.chUIs[0].getExp(5);
	}

	void gamble(int num)
	{
		
	}
}
