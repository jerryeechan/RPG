using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.SerializableAttribute]
public class CriticalRateAttribute : DependentAttribute{
	public CriticalRateAttribute(Attribute dex):base(0)
	{
		this.dexAttr = dex;
	}
	Attribute dexAttr;
	public override int calculateValue()
	{
		_finalValue = baseValue;
		
		

		_finalValue += (int)(dexAttr/5);
			
		applyRawBonuses();
		applyFinalBonuses();
			
		return _finalValue;
	}
}
