using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;

namespace com.jerrch.rpg{
	public class SkillCombatButton : SkillButton,IPointerClickHandler{//,IBeginDragHandler,IDragHandler,IEndDragHandler{
		
		public Character bindCh;
		protected Image cdMask;
		
		
		protected override void OnValidate()
		{
			base.OnValidate();
		}
		override protected void Awake()
		{
			base.Awake();
			btn = GetComponent<Button>();
			cdMask = transform.Find("icon").Find("CDMask").GetComponent<Image>();
		}
		int skillIndex;
		public bool setSkill(int index,Character ch, Skill skill)
		{	
			bindSkill = skill;
			
			bindCh = ch;
			skillIndex = index;
			if(skill==null)
				return false;

			if(skill.cd_count!=0)
			{
				cdMask.enabled = true;
			}
			else
			{
				cdMask.enabled = false;
			}

			// if(bindSkill.type == SkillType.Passive||skill.energyCost>EnergySlotUIManager.instance.energy||skill.cd_count>0)
			//int diceValue = DiceRollerSingle.instance.currentValue;
			bool typeValid = true;
			
			//if(diceValue>=skill.minCost&&diceValue<=skill.maxCost)
			//	costValid = true;
			/*
			if(diceValue == (int)skill.diceType)
			{
				typeValid = true;
			}*/
			//||ch.doneSkillThisRound==true
			//||bindSkill.type == SkillType.Passive
			if(!typeValid||skill.cd_count>0)
			{
				Lock();
				return false;
			}
			else
			{	
				Unlock();
				return true;
			}
			
			
			//Text text = GetComponentInChildren<Text>(); 	
			//skillBtnIcon.sprite = bindSkill.iconx;
			//skillBtnIcon.SetNativeSize();
			//cdText.setSprite(NumberGenerator.instance.getCDSprite(bindSkill.cd));
			//manaText.setSprite(NumberGenerator.instance.getManaSprite(bindSkill.mpCost()));
	//		text.text = skill.name;
		}

		//no skill
		public void disableButton()
		{
	//		print("disable btn");
			bindSkill = null;
			Lock();
		}

		/*
		public void useSkill()
		{
			bindSkill.cd_count = bindSkill.cd; 
			bindCh.useSkill(skillIndex);
			bindCh.doneSkillThisRound = true;
			DiceRollerSingle.instance.next();
		}*/
		//Color32 white = new Color32(255,255,255,0);
		//Color32 black = new Color32(0,0,0,150);
		//Color32 red = new Color32(255,0,0,150);
		public void Unlock()
		{
			isLocked = false;
			btn.interactable = true;
			disableMask.DOFade(0,0.2f);				
			
			//disableMask.color = white;
		}
		
		Button btn;
		public bool isEnable
		{
			get{
				
				return !isLocked&&SkillCombatUIManager.instance.isReadyToDoSkill;
			}
		}
		bool isLocked;
		public void Lock()
		{
			isLocked = true;
			btn.interactable = false;
			disableMask.DOFade(0.8f,0.2f);
			//disableMask.enabled = true;
	//		print("lock");
			//disableMask.color = black;
		}

		public void selectButton()
		{
			//skillNameText.color = Color.yellow;
			SkillCombatUIManager.instance.detailPanel.setText(bindSkill.name,bindSkill.description);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if(isEnable)
			{
				//SkillCombatUIManager.instance.skillBtnTouched(skillIndex);
				TurnBattleManager.instance.selectSkill(bindSkill);
			}
		}
		/*
		public void OnBeginDrag(PointerEventData eventData)
		{
			isDragging = true;
			if(isEnable)
			{
				SkillCombatUIManager.instance.OnBeginDrag(eventData,bindSkill,transform.GetComponent<RectTransform>().anchoredPosition);
				SoundEffectManager.instance.playSound(BasicSound.Press);
			}
		
			print("begin drag");
			
		}
		bool isDragging = false;

        public void OnDrag(PointerEventData eventData)
        {
			if(isDragging)
            	SkillCombatUIManager.instance.OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
			
			isDragging = false;
            print("end drag");
			
			if(isEnable)
			{
				SkillCombatUIManager.instance.OnEndDrag();
			}
			
        }
		*/
    }
}


