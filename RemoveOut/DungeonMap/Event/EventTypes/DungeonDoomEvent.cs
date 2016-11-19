using UnityEngine;
using System.Collections;

public class DungeonDoomEvent : DungeonEvent {
	override public void encounter()
	{
		describe(descriptionText);
		DiceRoller2D.instance.Roll(diceResult);
	}
	int diceNum;
	public void diceResult(int num)
	{
		DungeonOptionSelector.instance.showPanel(this);
		//show the doom event, 
		diceNum = num;
		DungeonPlayerStateUI.instance.getDoom(-100);
	}

	override public void confirm(){
		base.confirm();
		//Struggle
		//show select dice item panel
		
	}
	override public void cancel()
	{
		base.cancel();
		//Accept
		DoomEvent(diceNum);
		DungeonManager.instance.dungeonEventComplete(false);
	}



	void useDiceItem()
	{
		
	}
	void DoomEvent(int num)
	{

	}
}
