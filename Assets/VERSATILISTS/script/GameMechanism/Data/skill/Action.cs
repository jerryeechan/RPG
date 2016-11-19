using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;
namespace com.jerrch.rpg
{
	public class Action : MonoBehaviour {
		public ActionType type;
		public Skill[] skills;
		public string description;
		public Sprite icon;
		public string actionAnimationID;
		public CharacterAnimation chAnimation;
		public ActionData actionData;
		[HideInInspector]
		public Character caster;
		
		public int energyCost = 1;
		public int cd = 0;
		[HideInInspector]
		public int cd_count;
		void OnValidate()
		{
			actionData.id = name;
			if(isPassive)
			{
				cd = 0;
				energyCost = 0;
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
				energyCost = 0;
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
		public void start()
		{
			PlayActionAnimation();
		}

		//TODO:XXX no need for aciton animation, direct call skills.doeffect
		void PlayActionAnimation()
		{

			//TODO:
			this.myInvoke(1/6f,OnActionAnimationDone);
			//temp
			Animator a = AnimationManager.instance.getActionEffect(actionAnimationID);
			a.speed = AnimationManager.getChAnimSpeed(chAnimation);
			a.transform.position = caster.chRenderer.transform.position+Vector3.right*10;
			a.gameObject.SetActive(true);
			
		}
		void OnActionAnimationDone()
		{
			if(skills==null)
			{
				Debug.LogError("the action has no skill");
				return;
			}
			foreach(var skill in skills)
			{
				skill.skillState = SkillState.Before;
			}
			//StartCoroutine(CheckActionDone());
			skills[0].init(caster);
			skills[0].DoEffect();
			Camera.main.DOShakePosition(0.2f,5,30);
		}
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