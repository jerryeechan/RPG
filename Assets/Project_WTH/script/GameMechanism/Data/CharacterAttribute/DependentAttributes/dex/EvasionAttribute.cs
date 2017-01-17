using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.jerrch.rpg;
[System.SerializableAttribute]
public class EvasionAttribute : DependentAttribute {

	public EvasionAttribute(Attribute dex):base(0)
	{
		this.dexAttr = dex;
	}	
	Attribute dexAttr;
	public override int calculateValue()
	{
		_finalValue = baseValue;
		
		// Every 5 points in dexterity adds 1 to attack speed
		
		_finalValue += (int)(dexAttr/5);
			
		applyRawBonuses();
		applyFinalBonuses();
			
		return _finalValue;
	}
}
