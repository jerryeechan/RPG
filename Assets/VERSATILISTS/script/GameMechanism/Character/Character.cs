using UnityEngine;
using System.Collections.Generic;

namespace com.jerrch.rpg
{
	public class Character : MonoBehaviour{
	#region VARIABLES
		// Use this for initialization≥
		
		public CharacterData bindData;
		public CharacterRenderer chRenderer;
		public CharacterStat initStat;
		public CharacterStat equipStat;
		public CharacterStat battleStat;
		
		public int index;
		public bool doneActionThisRound = false;
		Dictionary<EquipType,Equip> equipsDict; 

		public Equip getEquip(EquipType type)
		{
			return equipsDict[type];
		}

		/*
		CharacterUI ui;
		public CharacterUI chUI{
			get{
				return ui;
			}
			set{
				ui = value;
				ui.bindCh = this;
			}
		}*/
		
		public List<SkillEffect> effectsOnMe;
		
		//the skill this character can use, total 4
		[SerializeField]
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
			initStat = stat;
			equipList = equips;
			EquipStart();
			BattleStart();
			actionList = actions;	
		}
		
		public void updateValues()
		{
			//int hp = equipStat.hp;
			initStat = bindData.genStat();
			//initStat.hp = hp;
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
			battleStat.hp.refresh();
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
			if(equip == null)
			{
				Debug.LogError("null equip?");
				return ;
			}	
			
			equip.transform.SetParent(transform.Find("Equips"));
			if(equipsDict.ContainsKey(equip.equipType))
			{
				//Take off the wearing same equip
				takeOff(equip.equipType);
				equipsDict[equip.equipType] = equip;
			}
			else
			{
				equipsDict.Add(equip.equipType,equip);
			}
			
			foreach(SkillEffect effect in equip.effects)
			{
				effect.ApplyOn(equipStat);
			}
			equip.bindCh = this;
			chRenderer.wearEquip(equip);
		}
		public void takeOff(EquipType type)
		{
			var lastEquip = equipsDict[type];
			foreach(SkillEffect effect in lastEquip.effects)
			{
				effect.RemoveEffect();
				//effect.ApplyOn(equipStat);
			}
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
		public bool isDead
		{
			get{ return battleStat.hp.currentValue==0?true:false;}
		}
		Action usingAction;
		
		static float chmove_to_action_delay = 0.2f;

		public void doActionMove(CharacterAnimation chAnim)
		{
			if(chRenderer)
				chRenderer.PlayCharacterAnimation(chAnim);
			//this.myInvoke(chmove_to_action_delay,OnCharacterAnimationDone);
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
		public int getLevelFullExp(int level)
		{
			return level*50;
		}
		public void getExp(int val)
		{
			bindData.exp+=val;
			int fullexp = getLevelFullExp(bindData.level);
			if(bindData.exp>fullexp)
			{
				bindData.exp -= fullexp;
				levelUp();
			}
		}
		public void levelUp()
		{
			bindData.skillPoints+=1;
			bindData.abilityPoints+=3;
			bindData.level++;
		}

		public void die()
		{
			chRenderer.PlayCharacterAnimation(CharacterAnimation.die);
			if(side == CharacterSide.Enemy)
			{
				//RandomBattleRound.instance.EnemyDie(this);
				//BattleChUIManager.instance.allGetExp(bindData.exp);
			}
			else
			{

				GameManager.instance.characterDied(this);
			}
				
		}
		
		public Character enemyTarget(SkillSpecificFilter filter)
		{
			
				//TODO: replace with hate
			return TurnBattleManager.instance.filterEnemy(side,filter);
			
		}
		
		public Character allieTarget(SkillSpecificFilter filter)
		{
			return TurnBattleManager.instance.filterAllies(side,filter);
		}

		public Character diedAllie()
		{
			 return TurnBattleManager.instance.diedPlayers[0];
		}

		

		public List<Character> allies()
		{
			if(side==CharacterSide.Player)
			{
				return TurnBattleManager.instance.players;
			}
			else
			{
				return TurnBattleManager.instance.monsters;
			}
		}
		public List<Character> enemyTargets()
		{
			if(side==CharacterSide.Player)
			{
				return TurnBattleManager.instance.monsters;
			}
			else
			{
				return TurnBattleManager.instance.players;
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

}