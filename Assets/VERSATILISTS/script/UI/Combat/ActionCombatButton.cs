using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;

namespace com.jerrch.rpg{
	public class ActionCombatButton : ActionButton,IPointerClickHandler,IBeginDragHandler,IDragHandler,IEndDragHandler{
		
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
		public bool setAction(int index,Character ch, Action action)
		{	
			bindAction = action;
			
			bindCh = ch;
			skillIndex = index;
			if(action==null)
				return false;

			if(action.cd_count!=0)
			{
				cdMask.enabled = true;
			}
			else
			{
				cdMask.enabled = false;
			}

			// if(bindAction.type == ActionType.Passive||action.energyCost>EnergySlotUIManager.instance.energy||action.cd_count>0)
			//int diceValue = DiceRollerSingle.instance.currentValue;
			bool typeValid = true;
			
			//if(diceValue>=action.minCost&&diceValue<=action.maxCost)
			//	costValid = true;
			/*
			if(diceValue == (int)action.diceType)
			{
				typeValid = true;
			}*/
			//||ch.doneActionThisRound==true
			//||bindAction.type == ActionType.Passive
			if(!typeValid||action.cd_count>0)
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
			//skillBtnIcon.sprite = bindAction.iconx;
			//skillBtnIcon.SetNativeSize();
			//cdText.setSprite(NumberGenerator.instance.getCDSprite(bindSkill.cd));
			//manaText.setSprite(NumberGenerator.instance.getManaSprite(bindSkill.mpCost()));
	//		text.text = skill.name;
		}

		//no action
		public void disableButton()
		{
	//		print("disable btn");
			bindAction = null;
			Lock();
		}

		/*
		public void useAction()
		{
			bindAction.cd_count = bindAction.cd; 
			bindCh.useAction(skillIndex);
			bindCh.doneActionThisRound = true;
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
				
				return !isLocked&&ActionUIManager.instance.isReadyToDoAction;
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
			ActionUIManager.instance.detailPanel.setText(bindAction.name,bindAction.description);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if(isEnable)
			{
				ActionUIManager.instance.actionBtnTouched(skillIndex);
				
			}
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			isDragging = true;
			if(isEnable)
			{
				ActionUIManager.instance.OnBeginDrag(eventData,bindAction,transform.GetComponent<RectTransform>().anchoredPosition);
				SoundEffectManager.instance.playSound(BasicSound.Press);
			}
		
			print("begin drag");
			
		}
		bool isDragging = false;

        public void OnDrag(PointerEventData eventData)
        {
			if(isDragging)
            	ActionUIManager.instance.OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
			
			isDragging = false;
            print("end drag");
			
			if(isEnable)
			{
				ActionUIManager.instance.OnEndDrag();
				//**TODO: deprecated
				/*
				if(ActionUIManager.instance.testDrop())
				{
					ActionUIManager.instance.lockAllSkillBtn();
					this.myInvoke(0.5f,useAction);
				}
				*/
			}
			
        }
    }
}


