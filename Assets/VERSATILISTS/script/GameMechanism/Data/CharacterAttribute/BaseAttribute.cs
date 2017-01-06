using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.SerializableAttribute]
public class BaseAttribute:StringfyProperty{
	[SerializeField]
	private int _baseValue;
    private float _baseMultiplier;
         
	public BaseAttribute(int value, float multiplier = 0) 
	{
		_baseValue = value;
		_baseMultiplier = multiplier;
	}
		
	public int baseValue
	{
		get{
			return _baseValue;
		}
		
	}
		
	public float baseMultiplier
	{
		get{
			return _baseMultiplier;	
		}
	}
}

