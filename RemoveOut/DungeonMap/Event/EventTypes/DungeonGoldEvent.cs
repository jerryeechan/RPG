using UnityEngine;
using System.Collections;

public class DungeonGoldEvent : DungeonEvent {

	void Awake()
	{
		descriptionText = "You found some gold...";
	}
	override public void confirm(){
		base.confirm();
		//enter battle
		DiceRoller2D.instance.Roll(gambleEvent);
		
	}
	override public void cancel()
	{
		base.cancel();
		describe("You got 10 golds");
		DungeonPlayerStateUI.instance.getGold(10);
		DungeonManager.instance.dungeonEventComplete(true);
		//Destroy(gameObject);
		
	}
	public void gambleEvent(int sum)
	{
		if(sum>6)
		{
			describe("You win");
			DungeonPlayerStateUI.instance.getGold(15);
		}
		else{
			describe("You loose");
			DungeonPlayerStateUI.instance.getGold(5);
		}
		DungeonManager.instance.dungeonEventComplete(true);
	}
		
}
