using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

namespace com.jerrch.rpg
{

	public class SkillButton : HoverUI{

		// Use this for initialization
		
		//CharacterButton chButton;
		public CompositeText cdText;
		//public CompositeText manaText;
		public Image skillIcon;
		public Image diceTypeImage;
		protected Image disableMask;



		//should not use in public
		[SerializeField]
		Skill _skill;
		public virtual Skill bindSkill{
			set{
	//			print("set bind skill");
				_skill = value;
				if(_skill==null)
				{
					skillIcon.sprite = SpriteManager.instance.emptySprite;
					//disableMask.enabled = true;
					if(diceTypeImage)
						diceTypeImage.color = Color.black;
					//manaText.transform.parent.gameObject.SetActive(false);
					cdText.transform.parent.gameObject.SetActive(false);
				}
				else 
				{
					skillIcon.sprite = _skill.icon;
					//disableMask.enabled = false;
					if(diceTypeImage)
						diceTypeImage.color = SkillManager.getDiceTypeColor(_skill.diceType);
					showManaCD(true);
					//manaText.text = ""+_skill.minCost+"-"+_skill.maxCost; 
					//_skill.energyCost.ToString();
					cdText.text = _skill.cd.ToString();
				}
			}
			get{
				return _skill;
			}
		}
		void showManaCD(bool b)
		{
			//manaText.transform.parent.gameObject.SetActive(b);
			cdText.transform.parent.gameObject.SetActive(b);	
		}
		void link()
		{
			skillIcon = transform.Find("icon").GetComponentInChildren<Image>();
			cdText = transform.Find("cd").GetComponentInChildren<CompositeText>();
			//manaText = transform.Find("mp").GetComponentInChildren<CompositeText>();
			Transform disableMaskT = transform.Find("disableMask"); 
			if(disableMaskT)
				disableMask = disableMaskT.GetComponent<Image>();
			diceTypeImage = transform.Find("type").GetComponent<Image>();
		}
		protected virtual void OnValidate()
		{
			link();
		}
		protected virtual void Awake()
		{
			link();
		}

        public override string description()
		{
			return "Level:"+bindSkill.level+'\n'+CompositeText.GetLocalText(bindSkill.description);
		}

		public override string title()
		{
			return bindSkill.id;
		}

		
    }
}