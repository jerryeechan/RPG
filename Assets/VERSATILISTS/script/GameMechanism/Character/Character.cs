using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class Character : MonoBehaviour{
#region VARIABLES
	// Use this for initialization≥
	
	CharacterData chData;
	public CharacterRenderer chRenderer;
	public CharacterStat initStat;
	public CharacterStat equipStat;
	public CharacterStat battleStat;


	List<Action> actionUsed;
	public List<SkillEffect> effectsOnMe;
	//the skill this character can use, total 6
	public List<ActionData> actionData;
	
	public enum CharacterSide{Player,Enemy};
	public CharacterSide side;
	

	#endregion
	
	
	//permanent property
	void Reset()
	{

	}
	Transform equipTransform;
	void Awake () {
		
		//initCharacter();
		equipTransform = transform.Find("Equips");
	}
	public void init(int hp, int mp,int sp,int strValue,int intValue,int dexValue)
	{
		initStat = new CharacterStat(name);
		initStat.statname = "initstat";
		initStat.maxHP = hp;
		initStat.maxMP = mp;
		initStat.maxSP = sp;
		initStat.hp = hp;
		initStat.mp = mp;
		initStat.sp = sp;
		
		initStat.strValue = strValue;
		initStat.intValue = intValue;
		initStat.dexValue = dexValue;

		actionUsed = new List<Action>();
		actionData = new List<ActionData>();
		actionData.Capacity = 6;

		ApplyEquipEffects();
	}
	
	public void ApplyEquipEffects()
	{
		equipStat = initStat.Clone();
		equipStat.statname = "equipstat";
		if(equipTransform)
		{
			Equip[] equips = equipTransform.GetComponentsInChildren<Equip>();
			if(equips.Length==0)
				Debug.Log("No equip");
			//Debug.Log("equip num:"+equipSkill.Length);
			foreach(Equip equipment in equips)
			{
				wear(equipment);
			}
		}
		
		
		battleStat = equipStat.Clone(); 
		battleStat.statname = name+"_battlestat";
	}

	public void wear(Equip equip)
	{
		foreach(SkillEffect effect in equip.effects)
		{
			effect.ApplyOn(equipStat);
		}
	}
	public void updateRenderer()
	{
		if(chRenderer)
			chRenderer.updateRenderer(battleStat);
		else
		{
			Debug.LogError("no Renderer");
		}
	}
	public bool isDead
	{
		get{ return battleStat.hp==0?true:false;}
	}
	Action usingAction;
	List<Character> targets;
	public Action useAction(int index)
	{
		usingAction = actionData[index].genAction(this);
		//ActionLogger.Log(usingAction.name);
		actionUsed.Add(usingAction);
		/*
		
		for testing, no animation, play skill directly
		or wait animation invoke function
		*/
		if(chRenderer)
			chRenderer.PlayCharacterAnimation(usingAction.chAnimation);
		//PlayCharacterAnimation(usingAction.chAnimation);
		//TODO evnet of animation done;
		OnCharacterAnimationDone();
		
		//PlayCharacterAnimation();
		
		
		return usingAction;
		//wait the animation moment to send message
	}
	
	public void die()
	{
		chRenderer.PlayCharacterAnimation(CharacterAnimation.die);
	}
	void OnCharacterAnimationDone()
	{
		usingAction.move();
	}
	//animation of character move done 
	
	/*
	public SkillApplyStat hitBySkill(Skill skill)
	{
		//skillOnMe.Add(skill);
		
		SkillApplyStat stat = battleStat.hitBySkill(skill.skillStats);
		stat.applyTarget = this;
		//if(stat.damageCause>0)
		//{
			NumberGenerator.instance.GetDamageNum(transform.position,stat.damageCause);
			//JPoolManager.instance.GetObject("strike").transform.position = transform.position;
		//}
		
		healthBar.SetNowValue(stat.healthLeft);
		if(isDead)
		{
			if(anim)
			anim.Play("die");
		}
		return stat;
	}*/
	public void HitByEffect(SkillEffect effect)
	{
		effectsOnMe.Add(effect);
	}
	public void OneTurn()
	{
		foreach (SkillEffect effect in effectsOnMe)
		{
			if(effect.effectType==SkillEffect.EffectType.Value)
				effect.ApplyOn(battleStat);
		}
	}
	public Character attackTarget()
	{
		if(side==CharacterSide.Player)
		{
			return RandomBattleRound.instance.selectedEnemy;
		}
		else
		{
			//TODO: replace with hate
			return RandomBattleRound.instance.players[0];
		}
	}
	public List<Character> allies()
	{
		if(side==CharacterSide.Player)
		{
			return RandomBattleRound.instance.players;
		}
		else
		{
			return RandomBattleRound.instance.enemies;
		}
	}
	public List<Character> attackTargets()
	{
		if(side==CharacterSide.Player)
		{
			return RandomBattleRound.instance.enemies;
		}
		else
		{
			return RandomBattleRound.instance.players;
		}
	}

	public void RemoveEffect(SkillEffect effect)
	{
		effectsOnMe.Remove(effect);	
	}
	public void RemoveNegativeEffects()
	{
		RemoveEffects(SkillEffect.EffectType.NegativeBuff);
	}
	public void RemoveEffects(SkillEffect.EffectType type)
	{
		foreach(SkillEffect effect in effectsOnMe)
		{
			if(effect.effectType == type)
			{
				effectsOnMe.Remove(effect);
			}
		}
	}
}