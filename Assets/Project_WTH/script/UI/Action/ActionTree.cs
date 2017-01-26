using UnityEngine;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
namespace com.jerrch.rpg{
public class SkillTree : Singleton<SkillTree>,IDisplayable {

	// Use this for initialization
	
	CompositeText nameText;
	CompositeText descriptionText;
	CompositeText[] abilityReqTexts;
	public RectTransform detailRect;
	public HandButton learnBtn;
	public Image selector;
	public SkillButton draggingTempSkill;
	public SkillPerkUI[] perkUIs;
	Dictionary<string,SkillPerkUI> perkUIDict;
	SkillSlot[] skillSlots;
	void Awake()
	{
		nameText = detailRect.Find("name").GetComponentInChildren<CompositeText>(true);
		descriptionText = detailRect.Find("description").GetComponentInChildren<CompositeText>(true);
		abilityReqTexts = detailRect.Find("req").GetComponentsInChildren<CompositeText>(true);
		perkUIs = GetComponentsInChildren<SkillPerkUI>(true);
		skillSlots = GetComponentsInChildren<SkillSlot>(true);
		perkUIDict = new Dictionary<string,SkillPerkUI>();
		foreach(var perk in perkUIs)
		{
			perkUIDict.Add(perk.bindSkill.skillData.id,perk);
		}	

		for(int i=0;i<4;i++)
		{
			skillSlots[i].index = i; 
		}
		learnBtn.hideVec = new Vector2(0,-20);
	}
	public void Show()
	{
		print("skill tree show");
		SkillTree.instance.setCharacter(GameManager.instance.currentCh);
	}
	public void Hide()
	{
		
	}
	
	public void selectPerk(SkillPerkUI perkUI)
	{
		RectTransform rect = perkUI.GetComponent<RectTransform>();
		rect.parent.GetComponent<RectTransform>().DOAnchorPos(-rect.anchoredPosition,0.5f,true);
		

		selector.transform.SetParent(perkUI.transform);
		selector.rectTransform.anchoredPosition = Vector2.zero;
		selectedPerk = perkUI;

		switch(perkUI.bindSkill.skillData.state)
		{
			case SkillState.Locked:
				learnBtn.Hide();
			break;
			case SkillState.Avalible:
				learnBtn.Show();
				learnBtn.text.text = "Learn "+perkUI.requireSkillPoint+"pt";
			break;
			case SkillState.Learned:
				learnBtn.Hide();
			break;
		}
	}
	SkillPerkUI selectedPerk;
	public void LearnSkill()
	{
		//check skill Points enough
		selectedPerk.learn();
	}
	public void showSkill(SkillPerkUI perk,Vector2 pos)
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
			
		nameText.text =  perk.bindSkill.name;
		descriptionText.text = perk.bindSkill.description;
		descriptionText.text += "\nreq:\n";

		updateRequirement(perk);
		

	}
	void updateRequirement(SkillPerkUI perk)
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

	public SkillPerkUI draggingPerk;
	public void OnBeginDragSkillPerk(SkillPerkUI perkUI)
	{
		if(perkUI.skill.skillData.state == SkillState.Learned)
		{
			draggingPerk = perkUI;
			draggingTempSkill.gameObject.SetActive(true);
			draggingTempSkill.bindSkill = perkUI.bindSkill;
			draggingTempSkill.transform.position = perkUI.transform.position;
		}
		
	}

	public void OnDragSkillPerk(PointerEventData eventData)
	{
		if(draggingPerk)
		{
			RectTransform rect = draggingTempSkill.GetComponent<RectTransform>();
			float scale = UIManager.instance.GetComponent<Canvas>().scaleFactor;
			Vector2 d = eventData.delta/scale;
			rect.anchoredPosition+=d;
		}
		
	}

	public void OnDropSkillSlot(SkillSlot slot)
	{
		if(draggingPerk==null)
		return;

		SkillSlot swapSlot=null;
		foreach(var s in skillSlots)
		{
			
			if(s.bindSkill==draggingTempSkill.bindSkill)
			{
				swapSlot = s;
				break;
			}
		}
		
		//slot's skill was in other slot's
		if(swapSlot!=null)
		{
			//remove swapslot
			swapSlot.bindSkill = null;
			
		}
		
		//add slot's skill
		

		slot.bindSkill = draggingPerk.bindSkill;		
	}
	public void changeSkill(Character ch,int index,Skill skill)
	{
		ch.skillList[index] = skill;
	}
	public void OnEndDragSkillPerk()
	{
		draggingTempSkill.gameObject.SetActive(false);
		draggingPerk = null;
	}
	public void setCharacter(Character ch)
	{
		print("set character");
		int i=0;
		foreach(var skill in ch.skillList)
		{
			skillSlots[i].bindSkill = skill;
			i++;
		} 
		
	}
	

	public List<SkillData> exportSkillData()
	{
		List<SkillData> skillDataList = new List<SkillData>();
		foreach(var perk in perkUIs)
		{
			skillDataList.Add(perk.skill.skillData);
		}
		return skillDataList;
	}
	public void importSkillData(List<SkillData> skillDataList)
	{
		foreach(var acData in skillDataList)
		{
			  perkUIDict[acData.id].bindSkill.skillData = acData;
		}
	}
}

}