using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.SerializableAttribute]
public class MagicDamageAttribute :DependentAttribute{

	public MagicDamageAttribute(Attribute magAtk,Attribute intAttr):base(0)
	{
		this.intAttr = intAttr;
		this.magAtk = magAtk;
	}	
	Attribute intAttr;
	Attribute magAtk;
	public override int calculateValue()
	{
		_finalValue = baseValue;
			
		// Every 5 points in dexterity adds 1 to attack speed
		
		_finalValue += (int)((intAttr.finalValue / 5)*magAtk.finalValue);
			
		applyRawBonuses();
		applyFinalBonuses();
			
		return _finalValue;
	}
}
