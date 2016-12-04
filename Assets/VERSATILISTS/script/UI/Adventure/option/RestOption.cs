using UnityEngine;
using System.Collections;

public class RestOption : AdventureOption{

	override public void choose()
	{
		DiceRoller2D.instance.Roll(restResult);
	}
	void restResult(int[] values)
	{
		if(values[0]<3)
		{
			fail();
		}
		success();
	}
}
