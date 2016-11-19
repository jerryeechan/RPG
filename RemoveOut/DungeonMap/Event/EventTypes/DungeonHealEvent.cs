using UnityEngine;
using System.Collections;

public class DungeonHealEvent : DungeonEvent {

	override public void confirm(){
		base.confirm();
		//enter battle
		DiceRoller2D.instance.Roll(healEvent);
		
	}
	override public void cancel()
	{
		base.cancel();
		DungeonManager.instance.dungeonEventComplete();
		//Destroy(gameObject);
		
	}
	public SkillEffect effect;
	public void healEvent(int sum)
	{
		if(sum>6)
		{
			
			foreach(var ch in GameManager.instance.chs)
			{
				effect.ApplyOn(ch.equipStat);
			}
		}
		else{
			
		}
		DungeonManager.instance.dungeonEventComplete();
	}
}
