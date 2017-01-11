using UnityEngine;
using System.Collections;

public class EnemyEscapeOption : AdventureOption {
	
	override public void choose()
	{	
		print("逃跑");
//		DiceRoller2D.instance.Roll(cancelEvent);
		cancelEvent(6);
	}
	public void cancelEvent(int values)
	{
		if(values > 5)
		{
			//succeed
			Debug.Log("Run away succeed");
			success();
			/*
			this.myInvoke(1,
			()=>{
				PauseMenuManager.instance.Transition(()=>{
					
				});
			});	*/
			
		}
		else
		{
			//fail
			Debug.Log("Run away fail");
			fail();
		}
	}
}
