using UnityEngine;
using System.Collections;

public class DungeonMonsterEvent : DungeonEvent {
	
	override public void confirm(){
		base.confirm();
		//enter battle
		UIManager.instance.ShowCover(confirmEvent);
	}
	override public void cancel()
	{
		base.cancel();
		DiceRoller2D.instance.Roll(cancelEvent);
	}
	public void confirmEvent()
	{
		GameManager.instance.BattleMode();
		
		RandomBattleRound.instance.StartBattle(MonsterDataEditor.instance.getMonsterSet());
		UIManager.instance.HideCover(); 
	}
	public void cancelEvent(int sum)
	{
		if(sum>5)
		{
			//succeed
			Debug.Log("Run away succeed");
			DungeonManager.instance.dungeonEventComplete();
		}
		else
		{
			//fail
			Debug.Log("Run away fail");
			confirm();
		}
	}
}
