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
	
	
	Dictionary<EquipType,Equip> equipsDict; 

	public Equip getEquip(EquipType type)
	{
		return equipsDict[type];
	}

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
	
	public List<SkillEffect> effectsOnMe;
	
	//the skill this character can use, total 4
	List<Action> _actions;
	public List<Action> actionList{
		get{return _actions;}
		set{
			print("ActionList modified");
			_actions = value;
			foreach(var action in _actions)
			{
				if(action)
				{
					action.caster = this;
					if(action.isPassive)
					{
						action.PassiveApply();
					}
				}
			}
		}
	}
	public void changeAction(int index, Action action)
	{
		print("change action");
		Action replaceAction = actionList[index];
		if(replaceAction)
		{
			if(replaceAction.isPassive)
			replaceAction.PassiveRemove();
		}

		//replace with new one
		actionList[index] = action;
		if(action.isPassive)
		{
			action.PassiveApply();
		}
	}
	public void removeAction(int index)
	{
		Action action = actionList[index];
		if(action)
		{
			if(action.isPassive)
			{
				action.PassiveApply();
			}
		}
		actionList[index].PassiveRemove();
		actionList[index] = null;
	}
	public List<Action> actionUsed;
	//public List<ActionData> actionDataList;
	
	
	public CharacterSide side;
	

	#endregion
	
	//permanent property
	Transform equipTransform;
	void Awake () {
		
		//initCharacter();
		equipTransform = transform.Find("Equips");
		equipsDict = new Dictionary<EquipType,Equip>();
	}
	public List<Equip> equipList;
	public void init(CharacterStat stat,List<Equip> equips,List<Action> actions)
	{
		stat.hp = stat.maxHP;
		initStat = stat;
		equipList = equips;
		EquipStart();
		actionList = actions;
		actionUsed = new List<Action>();
	}
	
	public void updateValues()
	{
		int hp = equipStat.hp;
		initStat = chData.genStat();
		initStat.hp = hp;
		EquipStart();
	}
	public void EquipStart()
	{
		print("equip start");
		equipStat = initStat.Clone();
		equipStat.statname = "equipstat";
		equipsDict = new Dictionary<EquipType,Equip>();
		wearEquips(equipList);
	}

	public void BattleStart()
	{
		battleStat = equipStat.Clone(); 
		battleStat.statname = name+"_battlestat";
		chRenderer.init(battleStat);
	}

	public void BattleEnd()
	{
		equipStat.hp = battleStat.hp;
	}
	public void wearEquips(List<Equip> equips)
	{
		foreach(Equip eq in equips)
			wear(eq);
		
		chRenderer.syncAnimation();
	}
	public void wear(Equip equip)
	{
		print("wear:"+equip.name);
		equip.transform.SetParent(transform.Find("Equips"));
		equipsDict.Add(equip.equipType,equip);
		foreach(SkillEffect effect in equip.effects)
		{
			effect.ApplyOn(equipStat);
		}
		chRenderer.wearEquip(equip);
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
	public void getExp(int exp)
	{
		chUI.getExp(exp);
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
		/*
		if(actionList.Count<=index)
		{
			Debug.LogError("no action");
			return null;
		}*/
		if(actionList[index]==null)
		{
			Debug.LogError("action null");
			return null;
		}
		usingAction = actionList[index].genAction();
		
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
			if(effect.effectType==EffectType.Value)
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
		else
		{

			GameManager.instance.characterDied(this);
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
		RemoveEffects(EffectType.NegativeBuff);
	}
	public void RemoveEffects(EffectType type)
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
