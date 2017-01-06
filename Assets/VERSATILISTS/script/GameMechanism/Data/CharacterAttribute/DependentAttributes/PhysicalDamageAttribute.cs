using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.SerializableAttribute]
public class PhysicalDamageAttribute : DependentAttribute {
	public PhysicalDamageAttribute(Attribute phyAtk, Attribute strAttr):base(0)
	{
		this.strAttr = strAttr;
		this.phyAtk = phyAtk;
	}	
	Attribute phyAtk;
	Attribute strAttr;
	public override int calculateValue()
	{
		_finalValue = baseValue;
		_finalValue += (int)(strAttr.finalValue / 5) * phyAtk.finalValue;
		applyRawBonuses();
		applyFinalBonuses();
			
		return _finalValue;
	}
}
