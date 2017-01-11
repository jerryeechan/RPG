using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;
namespace com.jerrch.rpg
{
	public class Action : MonoBehaviour {
		public ActionType type;
		public ActionDiceType diceType;
		public Skill[] skills;
		public string description;
		public Sprite icon;
		public string actionAnimationID;
		public CharacterAnimation chAnimation;
		public ActionData actionData;
		[HideInInspector]
		public Character caster;
		
		//public int energyCost = 1;
		
		public int cd = 0;
		[HideInInspector]
		public int cd_count;
		void OnValidate()
		{
			skills = GetComponentsInChildren<Skill>();
			foreach (var skill in skills)
			{
				skill.parentAction = this;
			}
			actionData.id = name;
			if(isPassive)
			{
				cd = 0;
				//energyCost = 0;
			}
		}
		
		void Awake()
		{
			skills = GetComponentsInChildren<Skill>();
			foreach (var skill in skills)
			{
				skill.parentAction = this;
			}
			actionData.id = name;
			if(isPassive)
			{
				cd = 0;
				//energyCost = 0;
			}
		}
		public void PassiveApply()
		{
			foreach(var skill in skills)
			{
				foreach(var effect in skill.effects)
				{
					effect.ApplyOn(caster.equipStat);
				}	
			}
		}
		public void PassiveRemove()
		{
			foreach(var skill in skills)
			{
				if(skill.effects==null)
					continue;
				foreach(var effect in skill.effects)
				{
					effect.RemoveEffect();
				}	
			}
		}

		//call this as a template
		public void PlayAction(OnCompleteDelegate completeFunc)
		{
			start(completeFunc);
		}

		//start action when generate a action
		void start(OnCompleteDelegate completeFunc)
		{
			this.completeFunc = completeFunc;
			PlayActionAnimation();
		}
		OnCompleteDelegate completeFunc;

		//????? TODO:XXX no need for aciton animation, direct call skills.doeffect
		void PlayActionAnimation()
		{
			AnimationUnit a = AnimationManager.instance.getActionEffect(actionAnimationID);
			
			if(a)
			{
				print(actionAnimationID);
				a.OnCompleteEvent = ()=>{
					OnActionAnimationDone();
				};
				a.gameObject.SetActive(true);
				a.speed = AnimationManager.getChAnimSpeed(chAnimation);
				a.transform.position = caster.chRenderer.transform.position+Vector3.right*10;
				
			}
			else{
//				Debug.LogError("no action effect:"+actionAnimationID);
				OnActionAnimationDone();
			}
			
		}
		void OnActionAnimationDone()
		{
			//TODO: only attack action shake camera, or new attribute to 
			
			if(skills==null)
			{
				Debug.LogError("the action has no skill");
				return;
			}
			foreach(var skill in skills)
			{
				skill.skillState = SkillState.Before;
			}
			playSkill();
		}
		void playSkill()
		{
			skills[currentSkillIndex].init(caster);
			skills[currentSkillIndex].DoEffect(SkillDoneAndNext);
			currentSkillIndex++;
		}
		int currentSkillIndex = 0;
		public void SkillDoneAndNext()
		{
			if(currentSkillIndex<skills.Length)
				playSkill();
			else 
			{
				//Done
				completeFunc();
				//RandomBattleRound.instance.ActionDone();
			}
				
		}
		//TODO: remove this function, instead use next skill
		public void checkSkillDone()
		{
			foreach(var skill in skills)
			{
				if(skill.skillState != SkillState.Done)
				{
					print(skill.skillState);
					return;
				}
			}
			RandomBattleRound.instance.ActionDone();
		}
		public Action genAction()
		{
			Action action = Instantiate(this);
			return action;
		}
	/*
		bool isActionDone{
			get{
				print("check action done");
				foreach(var skill in skills)
				{
					if(skill.skillState != SkillState.Done)
					{
						print(skill.skillState);
						return false;
					}
				}
				return true;
			}
		}
		
		IEnumerator CheckActionDone()
		{
			//yield return new WaitUntil(() => isActionDone==true)
			while(!isActionDone)
			{
				//print("ee");
				yield return new WaitForSeconds(0.1f);
			}
			RandomBattleRound.instance.ActionDone();
			
		}
		*/
		public bool isPassive
		{
			get{return type==ActionType.Passive;}
		}
		
	}
}


public enum ActionType
{
	Active,Passive
}


[System.Flags] public enum ActionDiceType{
	None = 0,Attack = 1 , Defense = 1<<1, Special = 1<<2,
	AttackAndDefense = Attack | Defense,
	AttackAndSpecial = Attack | Special,
	DefenseAndSpecial = Defense | Special
}