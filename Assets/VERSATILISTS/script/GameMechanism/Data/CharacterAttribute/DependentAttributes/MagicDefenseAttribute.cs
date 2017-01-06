using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.SerializableAttribute]
public class MagicDefenseAttribute : DependentAttribute {
	public MagicDefenseAttribute(Attribute intAttr):base(0)
	{
		this.intAttr = intAttr;
	}
	Attribute intAttr;
	public override int calculateValue()
	{
		_finalValue = baseValue;
		_finalValue += (int)(intAttr.finalValue);
			
		applyRawBonuses();
		applyFinalBonuses();
			
		return _finalValue;
	}
}
