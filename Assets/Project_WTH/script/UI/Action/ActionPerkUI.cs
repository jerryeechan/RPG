using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace com.jerrch.rpg
{

public class SkillPerkUI : SkillButton, IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler,IDragHandler,IBeginDragHandler,IEndDragHandler
{
	
	RectTransform rect;

	//requirements
	public SkillRequirement[] requirements;
	
	public AbilityRequirement[] abilityReqs;
	
	public int requireSkillPoint = 1;

	public Skill skill;
	
	override protected void OnValidate()
	{
		base.OnValidate();
		bindSkill = skill;
		init();
	}
	override protected void Awake()
	{
		base.Awake();
		bindSkill = skill;
		init();
		rect = GetComponent<RectTransform>();
		abilityReqs = GetComponents<AbilityRequirement>();
	}
	

	public void importData(SkillData data)
	{
		bindSkill.skillData = data;
		init();
	}
	
	public void init()
	{
		switch(bindSkill.skillData.state)
		{
			case SkillState.Locked:
				disableMask.enabled = true;
			break;
			case SkillState.Avalible:
				disableMask.enabled = true;
			break;
			case SkillState.Learned:
				disableMask.enabled = false;
			break;
		}
	}
	public void learn()
	{
		bindSkill.skillData.state = SkillState.Learned;
		//learned = true;
		disableMask.enabled = false;
		
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
		
		
		//GetComponent<ScrollRect>().content.anchoredPosition
        Vector2 position = rect.parent.GetComponent<RectTransform>().anchoredPosition+rect.anchoredPosition;
		SkillTree.instance.showSkill(this,position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SkillTree.instance.hide();
	}

    public void OnPointerClick(PointerEventData eventData)
    {
		SkillTree.instance.selectPerk(this);
    }
	
    

    public void OnBeginDrag(PointerEventData eventData)
    {
        SkillTree.instance.OnBeginDragSkillPerk(this);
    }
	public void OnDrag(PointerEventData eventData)
    {
		SkillTree.instance.OnDragSkillPerk(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
		SkillTree.instance.OnEndDragSkillPerk();
    }
}
}