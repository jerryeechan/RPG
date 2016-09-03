using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using com.jerry.rpg;
//Skill is the 

public class Skill : MonoBehaviour {

	//the corresponding skill data of player
	public Action parentAction;
	public Skill parentSkill;
	public Skill followSkill;
	public enum SkillType{Attack,Buff,ETC};
	public SkillEffect mainEffect;
	public float accuracy = 1;
	List<SkillEffect> _effects;
	
	public List<SkillEffect> effects{get{return _effects;}}
	Character caster;
	public Character castBy{get {return caster;}}
	
	public string hitAnimationID;
	
	//WHEN CAST THE SKILL, CHARACTER DO RESPONSE ANIMATION
	
	
	//if the skill needs to wait until the animation to hit enemy or show the effect use wait signal
	//Testing skill use Instant
	public enum CastTiming{Instant,WaitSignal};
	public CastTiming castTiming;
	
	public Sprite icon;
	bool isCasting;
	List<Character> hitTargets = new List<Character>();
	
	public SkillState skillState = SkillState.Before;
	void Awake()
	{
		if(mainEffect==null)
		{
			mainEffect = transform.Find("main").GetComponent<SkillEffect>();
		}
	}
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
	
	//hit animation
	public void PlayAnimation(Character target,bool isHit = true)
	{
		isSkillDoneEffect = false;
		
		//hitTargets.Add(target);
		
		//caster.setCD(cd);
		//Debug.Log("skill have cd:"+cd);
		
		if(hitAnimationID!=null)
		{
			
		}
		if(!isHit)
		{
			PlayEffectAnimation(target,"miss");
		}
		//else wait signal to call SkillDoEffect
	}
	public void PlayEffectAnimation(Character target,string animID)
	{
			Animator seAnim = AnimationManager.instance.getSkillHitEffect(animID);
			seAnim.speed = GameManager.playerAnimationSpeed;
			//print(hitAnimationID);
			if(seAnim)
			{
				//may be useless
				//SkillHitAnimation skillhitAnim = seAnim.GetComponentInChildren<SkillHitAnimation>();
				//skillhitAnim.SetSkill(this,target);
				//TODO characterUI
				seAnim.gameObject.SetActive(true);
				seAnim.transform.position = target.chRenderer.transform.position;
			}
			else
			{
				Debug.LogError("null hit animation");
			}
	}
	
	//wait when animation to send Info
	
	//first and oneturn
	public float criticalBonus = 1;
	
	public void OnAnimationDone()
	{
		DoEffect();
	}
	int doingTargetCnt = 0;
	public void DoEffect()
	{
		
		skillState = SkillState.Doing;
		doingTargetCnt = 0;
		//apply effects on caster
		
		//things to calculate first: critical, accuracy, 
		

		//things to apply last: dmg, value change,restore
		
		//1. apply those affect the skill, won't miss
		
		
		//calulate accuracy for each target
		foreach (SkillEffect effect in _effects)
		{
			effect.FirstApply(caster);
		}
		float acc_final = accuracy + caster.battleStat.accuracy;
		criticalBonus = caster.battleStat.calCriticalBonus();
		mainEffect.useBy(caster);
		
		bool hit;
		switch (mainEffect.effectRange)
		{
			
			case SkillEffect.EffectRange.Target:
				doingTargetCnt = 1;
				Character target = caster.attackTarget();
				
				hit = mainEffect.FirstApply(target,acc_final,true);
				if(hit) 
					targetAfterHit(target);
				else
					miss(target);
				PlayAnimation(target,hit);
				
			break;
			case SkillEffect.EffectRange.AOE:
				doingTargetCnt = caster.attackTargets().Count;
				foreach (Character ch in caster.attackTargets())
				{
					hit = mainEffect.FirstApply(ch,acc_final,true);
					if(hit)
						targetAfterHit(ch);
					else
						miss(ch);
					PlayAnimation(ch,hit);
				}
			break;
			case SkillEffect.EffectRange.Self:
				doingTargetCnt = 1;
				PlayAnimation(caster);
				if(mainEffect.FirstApply(caster,acc_final,false))
					targetAfterHit(caster);
				else
					miss(caster);
				
			break;
			case SkillEffect.EffectRange.Allies:
				doingTargetCnt = caster.allies().Count;
				foreach(Character ch in caster.allies()){
					if(mainEffect.FirstApply(ch,acc_final,false))
						targetAfterHit(ch);
					else
						miss(ch);
					//yield return new WaitForSeconds(2);
				}
			break;
			case SkillEffect.EffectRange.Random:
			break;
		}
		//StartCoroutine(this.CheckSkillDone());
		
	}

	

	bool isSkillDone{
		get{
//			print(doingTargetCnt);
			return doingTargetCnt==0;
		}
	}

	void CheckSkillDone()
	{
		//yield return new WaitUntil(() => isSkillDone);
		if(isSkillDone)
		SkillDone();
	}

	void SkillDone()
	{
		skillState = SkillState.Done;
		parentAction.checkSkillDone();
	}
	

	
	void targetAfterHit(Character ch)
	{
		ch.updateRenderer();
		if(ch.chRenderer)
			ch.chRenderer.HitAnimation();
		if(ch.isDead)
		{
			print("charater die");
			ch.die();
			//RandomBattleRound.instance.NextAction();
		}
		
		hitTargets.Add(ch);
		//OnTargetHitDone();
		this.myInvoke(1,OnTargetHitDone);		
	}
	void miss(Character ch)
	{
		//TODO:
		//play miss animation
		//oncomplete set doingTargetCnt--

		doingTargetCnt--;
		CheckSkillDone();
	}

	void OnTargetHitDone()
	{
		if(followSkill)
		{
			followSkill.init(caster);
			followSkill.DoEffect();
		}
		//	RandomBattleRound.instance.NextAction();
		doingTargetCnt--;
		CheckSkillDone();
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
	
	
	public void effectDone(SkillEffect effect)
	{
		effects.Remove(effect);
		//checkDone();
	}
	public void checkDone()  
	{
		if(isDone&&!followSkill)
		{
			Destroy(parentAction.gameObject);
			//Destroy(gameObject);
		}
	}
}

public enum SkillState{Before,Doing,Done}

public class SkillHitStat
{
	float accuracy;
	float critical;
}