using UnityEngine;
using System.Collections;

public class RestOption : AdventureOption{

	override public void choose()
	{
		DiceRoller2D.instance.Roll(restResult);
	}
	void restResult(int num)
	{
		if(num<5)
		{
			fail();
		}
		success();
	}
}
