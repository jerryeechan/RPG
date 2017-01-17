using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.jerrch.rpg;
[System.SerializableAttribute]
public class CriticalDamageAttribute : DependentAttribute {

	
	public CriticalDamageAttribute(Attribute strAttr):base(0)
	{
		this.strAttr = strAttr;
	}
	Attribute strAttr;
	public override int calculateValue()
	{
		_finalValue = baseValue;		
		int str = strAttr.finalValue;

		_finalValue += (int)(str*0.02f);
			
		applyRawBonuses();
		applyFinalBonuses();
			
		return _finalValue;
	}
}
