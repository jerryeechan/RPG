using UnityEngine;
using System.Collections.Generic;

public enum EquipState{Template,Wearable}
//as Equipment template and also the runtime equip to character
public class Equip:MonoBehaviour {
	public string description;
	EquipState state = EquipState.Template;
	List<TemplateEffectSet> templateEffectSets;
	[HideInInspector]
	public Character ch;
	public EquipData equipData;
	public List<SkillEffect> effects{
		get {
			return _effects;
		}
	} 
	List<SkillEffect> _effects;
	//the effects this equips has;	
	public void randomEquipStats()
	{
		_effects = new List<SkillEffect>();
		foreach (TemplateEffectSet effectSet in templateEffectSets)
		{
			int propertyNum = Random.Range(effectSet.min,effectSet.max);
			int i;
			for(i=0;i<propertyNum;i++)
			{
				_effects[i] = effectSet.effects[i];
			}
			for(;i<effectSet.effects.Count;i++)
			{
				int j = Random.Range(0,i+1);
				if(j<propertyNum)
				{
					_effects[j] = effectSet.effects[i]; 
				}
			}
		}
	}
}

public class TemplateEffectSet:MonoBehaviour
{
	public List<SkillEffect> effects;
	public int min;
	public int max;
	
}