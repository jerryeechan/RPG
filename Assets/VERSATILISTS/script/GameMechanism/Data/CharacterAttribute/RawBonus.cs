using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.SerializableAttribute]
public class RawBonus : BaseAttribute {
	public RawBonus(int value = 0, float multiplier = 0):base(value,multiplier)
	{
	}
}
