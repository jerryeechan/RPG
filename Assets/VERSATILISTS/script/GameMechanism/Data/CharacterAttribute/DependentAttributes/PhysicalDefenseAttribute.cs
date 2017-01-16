using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.SerializableAttribute]
public class PhysicalDefenseAttribute : DependentAttribute {
	
	public PhysicalDefenseAttribute(Attribute _str,Attribute _con):base(0)
	{
		this.strAttr = _str;
		this.conAttr = _con;
	}	
	Attribute conAttr;
	Attribute strAttr;
	public override int calculateValue()
	{
		_finalValue = baseValue;
		
		

		_finalValue += (int)(conAttr.finalValue*2+strAttr.finalValue);
			
		applyRawBonuses();
		applyFinalBonuses();
			
		return _finalValue;
	}
}
