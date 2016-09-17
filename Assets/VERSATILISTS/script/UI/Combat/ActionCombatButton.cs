using UnityEngine;
using System.Collections;
using com.jerry.rpg;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ActionCombatButton : ActionButton,IPointerClickHandler {
	
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

		if(bindAction.type == ActionType.Passive||action.energyCost>EnergySlotUIManager.instance.energy||action.cd_count>0)
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
	public void useAction()
	{
		bindAction.cd_count = bindAction.cd; 
		bindCh.useAction(skillIndex);
	}
	//Color32 white = new Color32(255,255,255,0);
	//Color32 black = new Color32(0,0,0,150);
	//Color32 red = new Color32(255,0,0,150);
	public void Unlock()
	{
		isLocked = false;
		btn.interactable = true;
		disableMask.enabled = false;
		//disableMask.color = white;
	}
	
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
		disableMask.enabled = true;
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
			ActionUIManager.instance.actionBtnTouched(skillIndex);
    }
}
