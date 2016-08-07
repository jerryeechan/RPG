using UnityEngine;

public class SkillEffect : MonoBehaviour {
	//issue

	//--------------------------------------------------
	public enum EffectRange{Target,AOE,ExceptTarget,Self,Allies,Random};
	public enum EffectType{Value,PositiveBuff,NegativeBuff};//Value can't be removed, only with hp, mp, sp
	public EffectRange effectRange;
	public EffectType effectType;
	public int targetCount = 0;//0 is all
	
	// Use this for initialization
	public int level = 0;
	public int mpCost=0;
	public int spCost=0;
	public float duration = 1;//apply last for, at least 1
	
	public int applyDelay=0;
	

	public enum WithDependency{NONE,DamageDone,CasterHealth,TargetHealth,AllyHealth,EnemyHealth};
	public WithDependency dependency;
	
	public float baseValue;
	[HideInInspector]
	public float calEffectValue;

	[HideInInspector]
	public float applyResult;
	protected bool hasLevelSet = false;

	[HideInInspector]
	public bool hit = true;
	bool isApplying=false;

	[HideInInspector]
	//--------------------------------------------------
	public Skill parentSkill;
	//--------------------------------------------------
	protected virtual void Reset()
	{
		parentSkill = transform.parent.GetComponent<Skill>();
		print("Reset");
	}
	void Awake()
	{
		setLevel(level);
		//print(GetType().ToString());
	}
	public virtual void setLevel(int level)
	{
		hasLevelSet = true;
	}
	//public SkillType skillType;
	
	//public int roundLeft = 0;
	
	public bool FirstApply(Character ch,float acc = 100,bool avoidable = false)
	{
		bool hit;
		if(avoidable)
		{
			hit = ch.battleStat.testHit(acc);
		}
		else
		{
			hit = testPossibility(acc);
		}

		if(hit)
		{
			ch.HitByEffect(this);
			OneTurn(ch);
			return true;
		}
		else{
			parentSkill.effectDone(this);
			return false;
		}
	}
	bool testPossibility(float acc)
	{
		return acc>=Random.Range(0,100);
	}
	public virtual void ApplyOn(CharacterStat stat)
	{
		onStat = stat;
	}

	public virtual void useBy(Character ch)
	{
		casterStat = ch.battleStat;
	}
	public void OneTurn(Character ch)
	{
		if(applyDelay==0)
		{
			onStat = ch.battleStat;
			duration--;
			if(effectType==EffectType.Value)
			{
				ApplyOn(ch.battleStat);
			}
			else
			{
				if(!isApplying)
				{
					ApplyOn(ch.battleStat);
					isApplying = true;
				}
			}
		}
		else
		{
			applyDelay--;
		}

		if(isEffectOver)
		{
			RemoveEffect(ch);
		}
		
	}
	
	protected CharacterStat onStat;
	protected CharacterStat casterStat;
	
	//bool _isOver = false;
	
	public bool isEffectOver{get{return duration == 0;}}
	public void init()
	{

	}
	//true as over, remove from stat

	public virtual void RemoveEffect(Character ch)
	{
		//virtual method, for remove debuff or buff
		parentSkill.effectDone(this);
		ch.RemoveEffect(this);
	}
	// is there OnDestroy??
	
	
	public virtual void ApplyOnWithDependency()
	{
		
	}
	public string getDescription()
	{
		
		return SkillEffectManager.instance.descriptionDict[GetType().ToString()];
	}
}
public interface ISkillEffect{
	void ApplyOn(CharacterStat stat);
	void setLevel(int level);
	void RemoveEffect();
}