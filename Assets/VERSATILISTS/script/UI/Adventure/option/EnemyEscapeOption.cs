using UnityEngine;
using System.Collections;

public class EnemyEscapeOption : AdventureOption {
	
	override public void choose()
	{	
		print("escape");
		DiceRoller2D.instance.Roll(cancelEvent);
	}
	public void cancelEvent(int sum)
	{
		if(sum>5)
		{
			//succeed
			Debug.Log("Run away succeed");
			success();
		}
		else
		{
			//fail
			Debug.Log("Run away fail");
			fail();
		}
	}
}
