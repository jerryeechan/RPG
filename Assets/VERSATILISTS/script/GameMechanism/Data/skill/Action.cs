using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;
namespace com.jerry.rpg
{
	public class Action : MonoBehaviour {
		Skill[] skills;
		public string description;
		public Sprite icon;
		public string actionAnimationID;
		public CharacterAnimation chAnimation;
		[HideInInspector]
		public ActionData actionData;
		public Character caster;
		
		
		void Awake()
		{
			//foreach (Skill skill in  GetComponentsInChildren<Skill>())
			skills = GetComponentsInChildren<Skill>();
			foreach (var skill in skills)
			{
				skill.parentAction = this;
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
	}
}