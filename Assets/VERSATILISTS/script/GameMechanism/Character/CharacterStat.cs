using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
[System.SerializableAttribute]
public class CharacterStat:StringfyProperty{
	public CharacterStat(string name)
	{
		chName = name;
	}
	public string chName;
	public string statname;
	public int strValue = 1;
	public int intValue = 1;
	public int dexValue = 1;
	
	//basic
	public float _hp;
	public float maxHP;
	public float hp{
		get{return _hp;}
		set{
			_hp = Mathf.Clamp(value,0,maxHP);	
		}
	} 
	public float maxMP;
	public float maxSP;
	public float _mp;
	public float mp{
		get{return _mp;}
		set{
			_mp = Mathf.Clamp(value,0,maxMP);	
		}
	} 
	
	float _sp;
	public float sp{
		get{return _sp;}
		set{
			_sp = Mathf.Clamp(value,0,maxSP);	
		}
	} 


	public float hpRecoverPerRound = 0;
	public float mpRecoverPerRound = 1;
	public float spRecoverPerRound = 2;
	
	
	//skill
	//public float SkillPhyDmgPer = 0;
	//public float SkillMagDmgPer = 0;
	
	//Attack
	
	public float phyAtk = 0;
	public float magAtk = 0;

	public float lowestPhyDmg = 0.4f;
	public float lowestMagDmg = 0.4f;
	public float phyDmgBuff = 1;
	public float magDmgBuff = 1;
	
	public float damageVaryLow = 0.6f;//damage may hit lower than basic
	public float damageVaryHigh =1;//
	
	public float criticalRate = 5;//basic critical Rate in percentage
	public float criticalLow = 1.25f;//lowest critical damage bonus;
	public float criticalHigh = 1.5f;//highest
	
	
	public float accuracy=100;
	/*
	defense
	*/
	public float dodge=0;	//ignore damage totally;
	public float damageUpBound = 0;//doesn't take the damage higher than it
	public float damageLowerBound = 0;//doesn't take the damage lower than it
	
	/*
	buff
	*/
	//public float buffBuff; //bonus rate
	public float durationBuff=0;
	/*
	Defense
	*/
	public float phyDefense=0;  //for physical damage
	public float magDefense=0;

	public float ignorePhyDefensePer = 0;
	public float ignoreMagDefensePer = 0;
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
		if(criticalDice<criticalRate)
		{
			return  Random.Range(criticalLow,criticalHigh);
		}
		return 1;
	}
	public bool testHit(float accuracy)
	{
		accuracy -= dodge;
		return (accuracy > Random.Range(0,100)); 
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
