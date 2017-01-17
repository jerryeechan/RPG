using UnityEngine;
using System.Collections.Generic;
using com.jerrch.rpg;
public class TemplateEffectSet:MonoBehaviour
{
	public SkillEffect[] effects;
	public int min;
	public int max;
	void Awake()
	{
		effects = GetComponentsInChildren<SkillEffect>();
	}
}