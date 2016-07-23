using UnityEngine;
using System.Collections.Generic;
//Skill is the 

public class Skill : MonoBehaviour {

	//the corresponding skill data of player
	public Skill followSkill;
	public enum SkillType{Attack,Buff,ETC};
	public SkillEffect mainEffect;
	public float accuracy = 1;
	List<SkillEffect> _effects;
	
	public List<SkillEffect> effects{get{return _effects;}}
	Character caster;
	public Character castBy{get {return caster;}}
	
	public GameObject skillEffectAnimationPrefab;
	
	//WHEN CAST THE SKILL, CHARACTER DO RESPONSE ANIMATION
	public Character.CharacterAnimation chAnimation;
	
	//if the skill needs to wait until the animation to hit enemy or show the effect use wait signal
	//Testing skill use Instant
	public enum CastTiming{Instant,WaitSignal};
	public CastTiming castTiming;
	
	public Sprite icon;
	bool isCasting;
	List<Character> hitTargets = new List<Character>();

	
	public void init(Character caster) {
		this.caster = caster;
		_effects = new List<SkillEffect>();
		
		SkillEffect[] effectArray = transform.Find("bonus").GetComponents<SkillEffect>();
		int l= effectArray.Length;
		for(int i=0;i<l;i++)
		{
			_effects.Add(effectArray[i]);
			effectArray[i].init();
		}
	}
	public Skill GenCopy(Character caster)
	{
		Skill skillgened = ((GameObject)Instantiate(gameObject)).GetComponent<Skill>();
		skillgened.caster = caster;
		return skillgened;
	}
	
	public Skill AddEffect(SkillEffect effect)
	{
		_effects.Add(effect);
		return this;
	}
	bool isSkillDoneEffect;
	/*
	Do Animation of the skill
	after skill hit the target calculate the damage done and buff in SkillDoEffect
	
	*/
	
	public void StartCastAnimation(List<Character> targets)
	{
		isSkillDoneEffect = false;
		
		hitTargets.Add(targets[0]);
		
		//caster.setCD(cd);
		//Debug.Log("skill have cd:"+cd);
		
		if(skillEffectAnimationPrefab!=null)
		{
			GameObject seAnim = (GameObject)Instantiate(skillEffectAnimationPrefab);
			seAnim.GetComponentInChildren<SkillAnimation>().SetSkill(this);
			seAnim.transform.position = hitTargets[0].transform.position;
		}
		if(castTiming== CastTiming.Instant)
		{
			SkillDoEffect();
		}
		//else wait signal to call SkillDoEffect
	}
	
	//wait when animation to send Info
	public void SkillDoEffect()
	{
		Use();
		
		/*
		if(isSkillDoneEffect==false)
		BattleManager.instance.SkillApplyResult();
		else
		{
			throw new UnityException("double calculate effect result");
		}
		*/
	}
	
	//first and oneturn
	public float criticalBonus = 1;
	public virtual void Use()
	{
		//apply effects on caster
		
		//things to calculate first: critical, accuracy, 
		

		//things to apply last: dmg, value change,restore
		
		//1. apply those affect the skill, won't miss
		foreach (SkillEffect effect in _effects)
		{
			effect.FirstApply(caster);
		}
		float acc_final = accuracy + caster.battleStat.accuracy;
		criticalBonus = caster.battleStat.calCriticalBonus();
		mainEffect.useBy(caster);
		switch (mainEffect.effectRange)
		{
			case SkillEffect.EffectRange.Target:
				Character target = caster.attackTarget();
				if(mainEffect.FirstApply(target,acc_final,true))
					targetHit(target);
			break;
			case SkillEffect.EffectRange.AOE:
				foreach (Character ch in caster.attackTargets())
				{
					if(mainEffect.FirstApply(ch,acc_final,true))
						targetHit(ch);
				}
			break;
			case SkillEffect.EffectRange.Self:
				if(mainEffect.FirstApply(caster,acc_final,false))
					targetHit(caster);
			break;
			case SkillEffect.EffectRange.Allies:
				foreach(Character ch in caster.allies()){
					if(mainEffect.FirstApply(ch,acc_final,false))
						targetHit(ch);
				}
			break;
			case SkillEffect.EffectRange.Random:
			break;
		}
		
		//calulate accuracy for each target
		
		
		
		//isSkillDoneEffect = true;
		//CheckSkillDone();
	}
	void targetHit(Character ch)
	{
		hitTargets.Add(ch);
		if(followSkill)
		{
			followSkill.init(caster);
			followSkill.Use();
		}
		else
		{
			RandomBattleRound.instance.Round();
		}
		
	}
	//timing of apply effects, possibility of success apply, 
	
	
	//remove, check done in effect
	/*
	public void CheckSkillDone()
	{
		Debug.Log("checkskilldone"+onCasterEffects.Count+", ontargetEffect num:"+onTargetEffects.Count);
		for(int i=0;i<onCasterEffects.Count;i++)
		{
			SkillEffect effect =onCasterEffects[i];
			if(effect.isEffectOver)
			{
				effect.RemoveEffect(caster);
				onCasterEffects.Remove(effect);
			}
		}
		for(int i=0;i<onTargetEffects.Count;i++)
		{
			SkillEffect effect = onTargetEffects[i];
			if(effect.isEffectOver)
			{
				effect.RemoveEffect();
				onTargetEffects.Remove(effect);
			}
		}
		
	}*/
	public bool isDone{
		get{
			return effects.Count==0;
		}
	}
	
	Skill nextSkill;
	public void LinkNextSkill()
	{
		//if(nextSkill)
		//	nextSkill.Cast();
	}
	
	public int mpCost()
	{
		int mana=0;
		for(int i=0;i<effects.Count;i++)
			mana+= effects[i].mpCost;
		return mana;
	}

	//is
	public void checkDone()  
	{
		if(isDone)
		{
			Destroy(gameObject);
		}
	}
	
	public void effectDone(SkillEffect effect)
	{
		effects.Remove(effect);
		checkDone();
	}
}

public class SkillHitStat
{
	float accuracy;
	float critical;
}