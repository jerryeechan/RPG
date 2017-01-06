using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBonus : BaseAttribute {

	public int effectLastFor = -1;
	public int counter;
	public FinalBonus(int value = 0, float multiplier = 0):base(value,multiplier)
	{
		counter = effectLastFor;
	}
	public bool OneTurn()
	{
		counter--;
		if(counter<=0)
		{
			return false;
		}
		else
		{
			return true;
		}
	}
}
