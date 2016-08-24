using UnityEngine;
using System.Collections;

public class DungeonGoldEvent : DungeonEvent {

override public void confirm(){
		base.confirm();
		//enter battle
		DiceRoller2D.instance.Roll(gambleEvent);
		
	}
	override public void cancel()
	{
		base.cancel();
		DungeonPlayerStateUI.instance.getGold(10);
		DungeonManager.instance.dungeonEventComplete();
		//Destroy(gameObject);
		
	}
	public void gambleEvent(int sum)
	{
		if(sum>6)
		{
			DungeonPlayerStateUI.instance.getGold(15);
		}
		else{
			DungeonPlayerStateUI.instance.getGold(5);
		}
		DungeonManager.instance.dungeonEventComplete();
	}
		
}
