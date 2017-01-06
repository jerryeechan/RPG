using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.SerializableAttribute]
public class AccuracyAttribute : DependentAttribute{

	public AccuracyAttribute(Attribute dex):base(0)
	{
		this.dexAttr = dex;
	}
	Attribute dexAttr;
	public override int calculateValue()
	{
		_finalValue = baseValue;
		_finalValue += (int)(dexAttr/2);			
		applyRawBonuses();
		applyFinalBonuses();
			
		return _finalValue;
	}
}
