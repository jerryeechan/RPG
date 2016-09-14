using UnityEngine;
using System.Collections.Generic;
using com.jerry.rpg;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ActionTree : Singleton<ActionTree>,IDisplayable {

	// Use this for initialization
	
	CompositeText nameText;
	CompositeText descriptionText;
	CompositeText[] abilityReqTexts;
	public RectTransform detailRect;
	public HandButton learnBtn;
	public Image selector;
	public ActionButton draggingTempAction;
	public ActionPerkUI[] perkUIs;
	Dictionary<string,ActionPerkUI> perkUIDict;
	ActionSlot[] actionSlots;
	void Awake()
	{
		nameText = detailRect.Find("name").GetComponentInChildren<CompositeText>(true);
		descriptionText = detailRect.Find("description").GetComponentInChildren<CompositeText>(true);
		abilityReqTexts = detailRect.Find("req").GetComponentsInChildren<CompositeText>(true);
		perkUIs = GetComponentsInChildren<ActionPerkUI>(true);
		actionSlots = GetComponentsInChildren<ActionSlot>(true);
		perkUIDict = new Dictionary<string,ActionPerkUI>();
		foreach(var perk in perkUIs)
		{
			perkUIDict.Add(perk.bindAction.actionData.id,perk);
		}	

		for(int i=0;i<4;i++)
		{
			actionSlots[i].index = i; 
		}
		learnBtn.hideVec = new Vector2(0,-20);
	}
	public void Show()
	{
		print("action tree show");
		ActionTree.instance.setCharacter(GameManager.instance.currentCh);
	}
	public void Hide()
	{
		
	}
	
	public void selectPerk(ActionPerkUI perkUI)
	{
		RectTransform rect = perkUI.GetComponent<RectTransform>();
		rect.parent.GetComponent<RectTransform>().DOAnchorPos(-rect.anchoredPosition,0.5f,true);
		

		selector.transform.SetParent(perkUI.transform);
		selector.rectTransform.anchoredPosition = Vector2.zero;
		selectedPerk = perkUI;

		switch(perkUI.bindAction.actionData.state)
		{
			case ActionState.Locked:
				learnBtn.Hide();
			break;
			case ActionState.Avalible:
				learnBtn.Show();
				learnBtn.text.text = "Learn "+perkUI.requireActionPoint+"pt";
			break;
			case ActionState.Learned:
				learnBtn.Hide();
			break;
		}
	}
	ActionPerkUI selectedPerk;
	public void LearnAction()
	{
		//check action Points enough
		selectedPerk.learn();
	}
	public void showAction(ActionPerkUI perk,Vector2 pos)
	{
		pos.y = Mathf.Clamp(pos.y,-40,40);
		detailRect.gameObject.SetActive(true);
		if(pos.x>0)
		{
			detailRect.pivot = new Vector2(1,0.5f);
			pos.x-=15;
			detailRect.anchoredPosition = pos;
			
		}
		else
		{
			detailRect.pivot = new Vector2(0,0.5f);
			pos.x += 15;	
			detailRect.anchoredPosition = pos;
		}
			
		nameText.text =  perk.bindAction.name;
		descriptionText.text = perk.bindAction.description;
		descriptionText.text += "\nreq:\n";

		updateRequirement(perk);
		

	}
	void updateRequirement(ActionPerkUI perk)
	{
		for(int i=0;i<2;i++)
		{
			if(i<perk.abilityReqs.Length)
			{
				AbilityRequirement req = perk.abilityReqs[i];
				abilityReqTexts[i].prefix = req.type.ToString()+":";
				abilityReqTexts[i].text = req.reqValue.ToString();
				
				int chAbilityValue = GameManager.instance.currentCh.equipStat.getValue<int>("_"+req.type.ToString().ToLower());
				if(chAbilityValue<req.reqValue)
				{
					abilityReqTexts[i].color = Color.red;
				}
				else
				{
					abilityReqTexts[i].color = Color.white;
				}
					
			}
			else
			{
				abilityReqTexts[i].prefix = "";
				abilityReqTexts[i].text="";
			}
		}
	}
	public void hide()
	{
		detailRect.gameObject.SetActive(false);
	}

	public ActionPerkUI draggingPerk;
	public void OnBeginDragActionPerk(ActionPerkUI perkUI)
	{
		if(perkUI.action.actionData.state == ActionState.Learned)
		{
			draggingPerk = perkUI;
			draggingTempAction.gameObject.SetActive(true);
			draggingTempAction.bindAction = perkUI.bindAction;
			draggingTempAction.transform.position = perkUI.transform.position;
		}
		
	}

	public void OnDragActionPerk(PointerEventData eventData)
	{
		if(draggingPerk)
		{
			RectTransform rect = draggingTempAction.GetComponent<RectTransform>();
			float scale = UIManager.instance.GetComponent<Canvas>().scaleFactor;
			Vector2 d = eventData.delta/scale;
			rect.anchoredPosition+=d;
		}
		
	}

	public void OnDropActionSlot(ActionSlot slot)
	{
		if(draggingPerk==null)
		return;

		ActionSlot swapSlot=null;
		foreach(var s in actionSlots)
		{
			
			if(s.bindAction==draggingTempAction.bindAction)
			{
				swapSlot = s;
				break;
			}
		}
		
		//slot's action was in other slot's
		if(swapSlot!=null)
		{
			//remove swapslot
			swapSlot.bindAction = null;
			GameManager.instance.currentCh.removeAction(swapSlot.index);
		}
		
		//add slot's action
		

		slot.bindAction = draggingPerk.bindAction;
		GameManager.instance.currentCh.changeAction(slot.index,slot.bindAction);
		
	}
	public void changeAction(Character ch,int index,Action action)
	{
		ch.actionList[index] = action;
	}
	public void OnEndDragActionPerk()
	{
		draggingTempAction.gameObject.SetActive(false);
		draggingPerk = null;
	}
	public void setCharacter(Character ch)
	{
		print("set character");
		int i=0;
		foreach(var action in ch.actionList)
		{
			actionSlots[i].bindAction = action;
			i++;
		} 
		
	}
	

	public List<ActionData> exportActionData()
	{
		List<ActionData> actionDataList = new List<ActionData>();
		foreach(var perk in perkUIs)
		{
			actionDataList.Add(perk.action.actionData);
		}
		return actionDataList;
	}
	public void importActionData(List<ActionData> actionDataList)
	{
		foreach(var acData in actionDataList)
		{
			  perkUIDict[acData.id].bindAction.actionData = acData;
		}
	}
}
