﻿using UnityEngine;
using System.Collections;



[System.SerializableAttribute]

public class SkillRequirement : MonoBehaviour {
	string requireSkillID;
	public int totalUseReq;
	public int totalUseCurrent;
	public bool checkRequirement()
	{
		return totalUseCurrent>=totalUseReq;
	}
}
