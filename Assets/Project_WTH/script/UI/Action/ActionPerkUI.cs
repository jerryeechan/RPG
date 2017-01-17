using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace com.jerrch.rpg
{

public class ActionPerkUI : ActionButton, IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler,IDragHandler,IBeginDragHandler,IEndDragHandler
{
	
	RectTransform rect;

	//requirements
	public ActionRequirement[] requirements;
	
	public AbilityRequirement[] abilityReqs;
	
	public int requireActionPoint = 1;

	public Action action;
	
	override protected void OnValidate()
	{
		base.OnValidate();
		bindAction = action;
		init();
	}
	override protected void Awake()
	{
		base.Awake();
		bindAction = action;
		init();
		rect = GetComponent<RectTransform>();
		abilityReqs = GetComponents<AbilityRequirement>();
	}
	

	public void importData(ActionData data)
	{
		bindAction.actionData = data;
		init();
	}
	
	public void init()
	{
		switch(bindAction.actionData.state)
		{
			case ActionState.Locked:
				disableMask.enabled = true;
			break;
			case ActionState.Avalible:
				disableMask.enabled = true;
			break;
			case ActionState.Learned:
				disableMask.enabled = false;
			break;
		}
	}
	public void learn()
	{
		bindAction.actionData.state = ActionState.Learned;
		//learned = true;
		disableMask.enabled = false;
		
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
		
		
		//GetComponent<ScrollRect>().content.anchoredPosition
        Vector2 position = rect.parent.GetComponent<RectTransform>().anchoredPosition+rect.anchoredPosition;
		ActionTree.instance.showAction(this,position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ActionTree.instance.hide();
	}

    public void OnPointerClick(PointerEventData eventData)
    {
		ActionTree.instance.selectPerk(this);
    }
	
    

    public void OnBeginDrag(PointerEventData eventData)
    {
        ActionTree.instance.OnBeginDragActionPerk(this);
    }
	public void OnDrag(PointerEventData eventData)
    {
		ActionTree.instance.OnDragActionPerk(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
		ActionTree.instance.OnEndDragActionPerk();
    }
}
}