using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using com.jerry.rpg;
using DG.Tweening;
using UnityEngine.EventSystems;
public class Character : MonoBehaviour{
#region VARIABLES
	// Use this for initialization≥
	
	public CharacterData chData;
	public CharacterRenderer chRenderer;
	public CharacterStat initStat;
	public CharacterStat equipStat;
	public CharacterStat battleStat;

	DungeonCharacterUI ui;
	public DungeonCharacterUI chUI{
		get{
			return ui;
		}
		set{
			ui = value;
			ui.bindCh = this;
		}
	}
	List<Action> actionUsed;
	public List<SkillEffect> effectsOnMe;
	//the skill this character can use, total 6
	public List<ActionData> actionDataList;
	 /*
	public List<ActionData> actionData{
		get{return _actionData;} 
		set{
			actionDict = new Dictionary<string,ActionData>();
			foreach (var item in value)
			{
				actionDict.Add(item.id,item);
			}
			_actionData = value;
		}
	}*/
	//public Dictionary<string,ActionData> actionDict = new Dictionary<string,ActionData>();
	
	
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
		actionDataList = new List<ActionData>();
		actionDataList.Capacity = 4;

		//ApplyEquipEffects();
		EquipStart();
	}
	
	public void EquipStart()
	{
		equipStat = initStat.Clone();
		equipStat.statname = "equipstat";
		
	}
	public void BattleStart()
	{
		battleStat = equipStat.Clone(); 
		battleStat.statname = name+"_battlestat";
		chRenderer.init(battleStat);
	}
	
	public void wear(Equip equip)
	{
		print("wear:"+equip.name);
		equip.transform.SetParent(transform.Find("Equips"));
		
		foreach(SkillEffect effect in equip.effects)
		{
			effect.ApplyOn(equipStat);
		}
		chRenderer.wearEquip(equip.bindGraphic);
	}
	public void updateRenderer()
	{
		//scene chRenderer update
		if(!chRenderer)
		{
			Debug.LogError("no Renderer");
		}
		else
		{
			chRenderer.updateRenderer(battleStat);
		}		
	}
	public void updateDungeonUI()
	{
		//dungeon UI update
		if(!chUI)
		{
			Debug.LogError("no chUI");
		}
		else
		{
			chUI.updateUI(battleStat);
		}
	}
	public bool isDead
	{
		get{ return battleStat.hp==0?true:false;}
	}
	Action usingAction;
	List<Character> targets;
	
	static float chmove_to_action_delay = 0.2f;
	public Action useAction(int index)
	{
		if(actionDataList.Count<=index)
		{
			Debug.LogError("no action");
			return null;
		}
		usingAction = actionDataList[index].genAction(this);
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
		
		this.myInvoke(chmove_to_action_delay,OnCharacterAnimationDone);
		
		//PlayCharacterAnimation();
		
		return usingAction;
		//wait the animation moment to send message
	}
	


	//animation of character's  move done
	void OnCharacterAnimationDone()
	{
		usingAction.start();
	}
	 
	
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

	public void die()
	{
		chRenderer.PlayCharacterAnimation(CharacterAnimation.die);
		if(side == CharacterSide.Enemy)
			RandomBattleRound.instance.EnemyDie(this);
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
public enum CharacterSide{Player,Enemy};
