using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using com.jerrch.rpg;
[System.SerializableAttribute]
public class CharacterStat:StringfyProperty{
	public CharacterStat(string name,int _str,int _int,int _dex,int _con)
	{
		chName = name;
		magAtk = new Attribute(1);
		phyAtk = new Attribute(1);
		strAttr = new Attribute(_str);
		dexAttr = new Attribute(_dex);
		intAttr = new Attribute(_int);
		conAttr = new Attribute(_con);
		damageReduce = new Attribute(0);
		movable = new Attribute(1);

		hp = new MaxHPAttribute(conAttr);
		phyDmg = new PhysicalDamageAttribute(phyAtk,strAttr);
		magDmg = new MagicDamageAttribute(magAtk,intAttr);
		phyDef = new PhysicalDefenseAttribute(strAttr,conAttr);
		magDef = new MagicDefenseAttribute(intAttr);
		critDmg = new CriticalDamageAttribute(strAttr);
		critRate = new CriticalRateAttribute(dexAttr);
		accuracy = new AccuracyAttribute(dexAttr);
		evasion = new EvasionAttribute(dexAttr);
		
		
		attributeDict = new Dictionary<AttributeType,Attribute>();
		attributeDict.Add(AttributeType.PhyAtk,phyAtk);
		attributeDict.Add(AttributeType.MagAtk,magAtk);

		attributeDict.Add(AttributeType.PhyDamage,phyDmg);
		attributeDict.Add(AttributeType.MagDamage,magDmg);
		

		attributeDict.Add(AttributeType.PhyDefense,phyDef);
		attributeDict.Add(AttributeType.MagDefense,magDef);
		attributeDict.Add(AttributeType.DamageReduce,damageReduce);
		attributeDict.Add(AttributeType.Movable,movable);
		attributeDict.Add(AttributeType.Accuracy,accuracy);
		attributeDict.Add(AttributeType.Evasion,evasion);
		attributeDict.Add(AttributeType.CriticalDmg,critDmg);
		attributeDict.Add(AttributeType.CriticalRate,critRate);
		
	}

	public string chName;
	public string statname;

	public MaxHPAttribute hp;
	
	//Dependent attributes
	public PhysicalDamageAttribute phyDmg;//atk * str...
	public MagicDamageAttribute magDmg;
	public CriticalDamageAttribute critDmg;
	public CriticalRateAttribute critRate;
	public PhysicalDefenseAttribute phyDef;
	public MagicDefenseAttribute magDef;
	public AccuracyAttribute accuracy;
	public EvasionAttribute evasion;


	//
	public Attribute damageReduce; //direct damage reduce
	public Attribute phyAtk;
	public Attribute magAtk;
	
	//special attribute
	public Attribute movable; //行動力，機率，0不能動

	//basic Attribute
	public Attribute strAttr;
	public Attribute intAttr;
	public Attribute dexAttr;
	public Attribute conAttr;

	public Dictionary<AttributeType,Attribute> attributeDict;
	public Attribute getAttribute(AttributeType type)
	{
		if(attributeDict.ContainsKey(type))
		{
			return attributeDict[type];
		}
		else{
			Debug.LogError(type.ToString()+"attribute doesn't exist");
			return null;
		}
	}

	//public int conValue{
		//block += diff*0.01f;
		
	//public int dexValue{
		//accuracy += diff;
		//evasion += diff;	
		//criticalRate += diff*0.01f;
		

	//public int intValue{
	
	//		magAtk+= diff;
	//		magDef+= diff;	

	
	//skill
	//public float SkillPhyDmgPer = 0;
	//public float SkillMagDmgPer = 0;
	
	//Attack
	
	public void start()
	{
		
	}

	public float lowestPhyDmg = 0.4f;
	public float lowestMagDmg = 0.4f;


//	public float ignorePhyDefensePer = 0;
//	public float ignoreMagDefensePer = 0;
	public float block = 0; //ignore damage totally;
	
	/*
	public void ApplyEffects(List<SkillEffect> effects)
	{
		foreach (SkillEffect effect in effects)
		{
			effect.ApplyOn(this);
		}
	}
	public void RemoveEffects(List<SkillEffect> effects)
	{
		foreach (SkillEffect effect in effects)
		{
			if(effect.isEffectOver)
			effect.RemoveFrom(this);
		}
	}*/
	public CharacterStat Clone()
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new MemoryStream();
        formatter.Serialize(stream, this);
        stream.Seek(0, SeekOrigin.Begin);
		CharacterStat statCloned = (CharacterStat)formatter.Deserialize(stream);
        return statCloned;
     }
//check up to here
	public float calCriticalBonus()
	{
		int criticalDice = Random.Range(0,100);
		if(criticalDice < critRate.finalValue)
		{
			return  1+critDmg.finalValue;
		}
		return 1;
	}


	//TODO Evasion formula
	public bool testHit(float accuracy)
	{
		float hit = accuracy/(accuracy+Mathf.Pow(evasion.finalValue/2,0.8f));
		int test =  Random.Range(0,100);
		Debug.Log("hit chance:"+hit*100+"%,"+test);
		
		
		return true;//Fake (hit*100 >test); 
	}


	
	
/*
	public SkillApplyStat hitBySkill(SkillStats s)
	{
		SkillApplyStat applyStat = new SkillApplyStat();
		applyStat.name = s.name;
		
		
		avoidance = avoidance+(float)dexValue/10;
		float hitRate = s.accuracy - avoidance;
		
		//hit Rate as a percentage, above 100 hits 100 percent
		
		//Debug.Log("HitRate "+hitRate);
		
		if(hitRate>100||Random.Range(0,100)<hitRate)
		{
			float recieveDamage = s.phyDmg_Total*(100/phyArmor) + s.magDmg_Total*(100/magArmor);//Armor 100 as normal damage, temperay use
			
			if(isDefending)
				recieveDamage = 0;
			int damageDone = Mathf.RoundToInt(recieveDamage);
		//	Debug.Log("ori_dmg "+s.phyDmg_Total+" phyArmor: "+phyArmor +" recieve dmg "+recieveDamage);
			applyStat.damageCause = damageDone;
			applyStat.isSuccess =true;
			
			health-=damageDone;
			if(health<0)
				health = 0;
			applyStat.healthLeft = health;
			//Debug.Log(name+" remain health:"+health);
		}
		else
		{
			applyStat.isSuccess=false;	
		}
		
		return applyStat;
	}
	*/
	
}
