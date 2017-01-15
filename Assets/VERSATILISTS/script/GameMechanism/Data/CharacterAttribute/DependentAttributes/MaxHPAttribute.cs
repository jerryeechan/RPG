using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.SerializableAttribute]
public class MaxHPAttribute : DependentAttribute {
	public MaxHPAttribute(Attribute conAttr):base(10)
	{
		this.conAttr = conAttr;
		calculateValue();
	}
	Attribute conAttr;
	public override int calculateValue()
	{
		_finalValue = baseValue;
		
		// Every 5 points in dexterity adds 1 to attack speed
		Debug.Log(conAttr);
		_finalValue += (int)(conAttr.finalValue*2);
			
		applyRawBonuses();
		applyFinalBonuses();
			
		return _finalValue;
	}
	public void refresh()
	{
		_currentValue = calculateValue();
	}
	public int currentValue
	{
		get{
			return _currentValue;
		}
	}
	public int _currentValue;
	public void changeHP(int value)
	{
		_currentValue+=value;
		if(currentValue>_finalValue)
		{
			_currentValue = _finalValue;
		}
		else if(currentValue < 0)
		_currentValue = 0;
	}
	
}
