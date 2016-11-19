﻿using UnityEngine;
using System.Collections;
using SmartLocalization;
public class AbilityRequirement : MonoBehaviour {

	public AbilityType type;
	public int reqValue;
	public override string ToString()
	{
		string typeStr = LanguageManager.Instance.GetTextValue(type.ToString());
		return  reqValue+":"+reqValue;
	}
	
}
