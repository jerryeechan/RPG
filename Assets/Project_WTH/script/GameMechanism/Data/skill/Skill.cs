using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;
namespace com.jerrch.rpg
{
	public class Skill : MonoBehaviour {
		
		public SkillDiceType diceType;
		public SubSkill[] subSkills;
		public string id;
		public string description;
		public Sprite icon;
		public string skillAnimationID;
		public CharacterAnimation chAnimation;
		public SkillData skillData;
		[HideInInspector]
		public Character caster;
		
		//public int energyCost = 1;
		
		public int cd = 0;
		[HideInInspector]
		public int cd_count;
		

		void init()
		{
			subSkills = GetComponentsInChildren<SubSkill>();
			foreach (var subSkill in subSkills)
			{
				subSkill.parentSkill = this;
			}
			skillData.id = name;
			id = name;
		}
		void OnValidate()
		{
			init();	
		}
		
		
		void Awake()
		{
			init();
		}
		
		public int level{
			get{
				return skillData.level;
			}
			set{
				setLevel(value);
			}
		}
		void setLevel(int lv)
		{
			skillData.level = lv;
			foreach (var subSkill in subSkills)
			{
				subSkill.mainEffect.setLevel(lv);
				/*
				foreach(var effect in skill.effects)
				{
					effect.setLevel(lv);
				}*/
			}
			
		}

		/*
		public void PassiveApply()
		{
			foreach(var subSkill in subSkills)
			{
				foreach(var effect in subSkill.effects)
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
		*/

		//call this as a template
		public void PlaySkill(OnCompleteDelegate completeFunc)
		{
			start(completeFunc);
		}

		//start skill when generate a skill
		void start(OnCompleteDelegate completeFunc)
		{
			this.completeFunc = completeFunc;
			PlaySkillAnimation();
		}
		OnCompleteDelegate completeFunc;

		//????? TODO:XXX no need for aciton animation, direct call skills.doeffect
		void PlaySkillAnimation()
		{
			AnimationUnit a = AnimationManager.instance.getSkillEffect(skillAnimationID);
			if(a)
			{
				print(skillAnimationID);
				a.OnCompleteEvent = ()=>{
					OnSkillAnimationDone();
				};
				a.gameObject.SetActive(true);
				a.speed = AnimationManager.getChAnimSpeed(chAnimation);
				a.transform.position = caster.chRenderer.transform.position+Vector3.right*10;
				
			}
			else{
//				Debug.LogError("no skill effect:"+skillAnimationID);
				OnSkillAnimationDone();
			}
		}
		void OnSkillAnimationDone()
		{
			//TODO: only attack skill shake camera, or new attribute to 
			
			if(subSkills==null)
			{
				Debug.LogError("the skill has no skill");
				return;
			}
			/*
			foreach(var subSkill in subSkills)
			{
				subSkill.skillState = SubSkillState.Before;
			}*/
			playSkill();
		}
		void playSkill()
		{
			subSkills[currentSubSkillIndex].init(caster);
			subSkills[currentSubSkillIndex].DoEffect(SkillDoneAndNext);
			currentSubSkillIndex++;
		}
		int currentSubSkillIndex = 0;
		public void SkillDoneAndNext()
		{
			if(currentSubSkillIndex<subSkills.Length)
				playSkill();
			else 
			{
				//Done
				completeFunc();
				//RandomBattleRound.instance.SkillDone();
			}
				
		}
		//TODO: remove this function, instead use next skill
		public Skill genSkill()
		{
			Skill skill = Instantiate(this);
			return skill;
		}
	/*
		bool isSkillDone{
			get{
				print("check skill done");
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
		
		IEnumerator CheckSkillDone()
		{
			//yield return new WaitUntil(() => isSkillDone==true)
			while(!isSkillDone)
			{
				//print("ee");
				yield return new WaitForSeconds(0.1f);
			}
			RandomBattleRound.instance.SkillDone();
			
		}
		*/	
		
	}
}


[System.Flags] public enum SkillDiceType{
	None = 0,Attack = 1 , Support = 1<<1, Special = 1<<2,
	AttackAndSupport = Attack | Support,
	AttackAndSpecial = Attack | Special,
	SupportAndSpecial = Support | Special,
	Enemy
}