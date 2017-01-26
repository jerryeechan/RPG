using UnityEngine;
using System.Collections.Generic;
using com.jerrch.rpg;

//as Equipment template and also the runtime equip to character
public class Equip:Item {
	public static EquipType[] AllEquipType ={EquipType.Armor,EquipType.Helmet,EquipType.Weapon,EquipType.Shield}; 
	public ClassesType bindClasses;
	EquipState state = EquipState.Template;
	TemplateEffectSet[] templateEffectSets;
	[HideInInspector]
	public Character bindCh;
	[HideInInspector]
	public EquipGraphicAsset bindGraphic{
		get{
			return base.asset as EquipGraphicAsset;
		}
		set{
			base.asset = value;
		}
	}
	
	EquipData _bindData;
	public new EquipData bindData{
		get{
			return _bindData;
		}	
		set{
			_bindData = value;
			//_bindData.seed; to change effects;
		}
	}
	public List<SkillEffect> effects;
	public EquipType equipType;
	public AbilityRequirement[] abilityReqs;
	void Awake()
	{

		templateEffectSets = GetComponentsInChildren<TemplateEffectSet>();
		itemType = ItemType.Equip;
		stackable = false;
		
	}
	override protected void Reset()
	{
		stackable = false;
		itemType = ItemType.Equip;
	}

	int _seed;
	public int seed{
		set{
			setEffect(value);
			_seed = value;
		}
		get{
			return _seed;
		}
	}
	public int[] genAvaliableSkillEffectsFromSeed(int seed)
	{
		//TODO: random effects 
		return new int[1]{0};
	}
	public void setEffect(int seed)
	{
		effects = new List<SkillEffect>();
		var available = genAvaliableSkillEffectsFromSeed(seed);
		if(available!=null)
		{
			for(int i=0;i<available.Length;i++)
			{
				if(templateEffectSets[i].effects.Length>available[i])
					effects.Add(templateEffectSets[i].effects[available[i]]);
			}
		}
	}

	public string getEffectString()
	{
		string str="";
		foreach(var effect in effects)
		{
			str+=effect.getDescription()+"\n";
		}
		return str;
	}
	public string getRequirementString()
	{
		string str = "";
		foreach(var req in abilityReqs)
		{
			str+=req.ToString()+" ";
		}
		return str;
	}
	//the effects this equips has;	
	public void randomEquipStats()
	{
		effects = new List<SkillEffect>();
		foreach (TemplateEffectSet effectSet in templateEffectSets)
		{
			int propertyNum = Random.Range(effectSet.min,effectSet.max);
			int i;
			for(i=0;i<propertyNum;i++)
			{
				effects[i] = effectSet.effects[i];
			}
			for(;i<effectSet.effects.Length;i++)
			{
				int j = Random.Range(0,i+1);
				if(j<propertyNum)
				{
					effects[j] = effectSet.effects[i]; 
				}
			}
		}
	}
	public new EquipData gerateData()
	{
		return new EquipData(id,asset.id,seed);
	}
}
public enum EquipState{Template,Wearable}
