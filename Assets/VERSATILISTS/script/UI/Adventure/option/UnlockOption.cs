using UnityEngine;
using System.Collections;

public class UnlockOption : AdventureOption {

	public int difficulty;
	// Use this for initialization
	public override void choose()
	{
		DiceRoller2D.instance.Roll(result);
	}

	void result(int num)
	{
		if(num>1)
		{
			parentEvent.triggerNextEvent = true;
			success();
			
		}
		else
		{
			fail();
		}
	}
}
