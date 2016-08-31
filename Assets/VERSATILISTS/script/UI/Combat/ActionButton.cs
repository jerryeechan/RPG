using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using com.jerry.rpg;
using UnityEngine.EventSystems;
using System;

public class ActionButton : MonoBehaviour,IPointerClickHandler{

	// Use this for initialization
	int skillIndex;
	public	Character bindCh;
	//CharacterButton chButton;
	public CompositeText cdText;
	public CompositeText manaText;

	public Text skillNameText;
	public Image skillIcon;
	Image disableMask;
	Image cdMask;
	
	public ActionData bindData;
	
	void Awake()
	{
		skillNameText = GetComponent<Text>();
		
		skillIcon = transform.Find("icon").GetComponentInChildren<Image>();
		cdText = transform.Find("cd").GetComponentInChildren<CompositeText>();
		manaText = transform.Find("mp").GetComponentInChildren<CompositeText>();
		disableMask = transform.Find("disableMask").GetComponent<Image>();
		cdMask = transform.Find("icon").Find("CDMask").GetComponent<Image>();
		/*
		cdText = transform.Find("cd").GetComponent<NumberImageText>();
		manaText = transform.Find("manacost").GetComponent<NumberImageText>();
		
		disableMask = transform.Find("DisableMask").GetComponent<Image>();
		skillBtnIcon = transform.Find("icon").GetComponent<Image>();
		*/
		btn =GetComponent<Button>();
	}
	
	public bool setAction(int id,Character ch, ActionData actionData)
	{	
		bindData = actionData;
		bindCh = ch;
		skillIndex = id;
		manaText.text = actionData.energyCost.ToString();
		cdText.text = actionData.cd.ToString();
		skillIcon.sprite = actionData.getActionRef().icon;

		//print(EnergySlotUIManager.instance.energy);
		if(actionData.cd_count!=0)
		{
			cdMask.enabled = true;
		}
		else
		{
			cdMask.enabled = false;
		}


		if(actionData.energyCost<=EnergySlotUIManager.instance.energy&&actionData.cd_count==0)
		{
			
			Unlock();
			print("eneergy enough");
			return true;
		}
		else
		{
			Lock();
			return false;
		}
		
		//Text text = GetComponentInChildren<Text>(); 	
		//skillBtnIcon.sprite = bindAction.iconx;
		//skillBtnIcon.SetNativeSize();
		//cdText.setSprite(NumberGenerator.instance.getCDSprite(bindSkill.cd));
		//manaText.setSprite(NumberGenerator.instance.getManaSprite(bindSkill.mpCost()));
//		text.text = skill.name;
	}
	public void disableButton()
	{
//		print("disable btn");
		manaText.text = "0";
		cdText.text = "0";
		Lock();
		
	}
	public void useAction()
	{
		bindData.cd_count = bindData.cd; 
		bindCh.useAction(skillIndex);
	}
	//Color32 white = new Color32(255,255,255,0);
	//Color32 black = new Color32(0,0,0,150);
	//Color32 red = new Color32(255,0,0,150);
	public void Unlock()
	{
		isLocked = false;
		ratio++;
		btn.interactable = true;
		disableMask.enabled = false;
//		print("unlock");
		highlightRaito();
		
		//disableMask.color = white;
	}
	
	void highlightRaito()
	{
		 
		if(ratio==1)
		{
			//skillNameText.color = Color.white;
		}
		else if(ratio == 2)
		{
			//skillNameText.color = Color.yellow;
		}
	}
	
	int ratio = 0;
	Button btn;

	public bool isEnable
	{
		get{
			return !isLocked;
		}
	}
	bool isLocked;
	public void Lock()
	{
		isLocked = true;
		btn.interactable = false;
		ratio =0;
		highlightRaito();
		disableMask.enabled = true;
//		print("lock");
		//disableMask.color = black;
	}
	public void selectButton()
	{
		//skillNameText.color = Color.yellow;
		ActionUIManager.instance.detailPanel.setText(bindData.id,bindData.getActionRef().description);
	}

	public void deSelectButton()
	{
		skillNameText.color = Color.white;
	}

    public void OnPointerClick(PointerEventData eventData)
    {
		if(isEnable)
			ActionUIManager.instance.actionBtnTouched(skillIndex);
    }
}
