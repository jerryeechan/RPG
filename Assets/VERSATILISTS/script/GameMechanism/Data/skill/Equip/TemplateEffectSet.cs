using UnityEngine;
using System.Collections.Generic;

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