using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.jerrch.rpg;
using System;

public class StatChUIManager : SingletonChUIManager<StatChUIManager>,IDisplayable,ISwitchBtnTouchDelegate {

	[SerializeField]
	List<HandButton> chButtons;

	[SerializeField]
	SkillBuildUIManger skillBuildManager;
	[SerializeField]
	CharacterMainAttributeUIManager mainAttributeManager;

	[SerializeField]
	EquipUIManager equipUIManager;
	
	[SerializeField]
	List<IinspectPlayerable> inspectables;
	
	
	override protected void Awake()
	{
		base.Awake();
		inspectables = new List<IinspectPlayerable>();
		inspectables.Add(skillBuildManager);
		inspectables.Add(mainAttributeManager);
		inspectables.Add(equipUIManager);
	}
	public void init()
	{
		base.Awake();
	}
	public void Show()
	{
		var ch = GameManager.instance.currentCh;
		inspectCharacter(ch);
		chBtnDisplay(0);
	}
	public void Hide()
	{

	}
	public void characterBtnTouched(int index)
	{
		var ch = GameManager.instance.chs[index];
		inspectCharacter(ch);
		chBtnDisplay(index);
		//abilityManager.inspectCharacter(ch);
	}

	void chBtnDisplay(int index)
	{
		for (int i = 0; i < chButtons.Count; i++)
		{
			if(i==index){
				//chButtons[i].interactable = true;
				chButtons[i].maskEnable = false;
			}
			else{
				//chButtons[i].interactable = false;
				chButtons[i].maskEnable = true;
			}
		}
	}
	void inspectCharacter(Character ch)
	{
		GameManager.instance.currentCh = ch;
		setCharacter(ch);
		foreach(var ins in inspectables)
		{
			if(ins!=null)
			{
				ins.inspectCharacter(ch);
			}
			
		}
	}
	public void getAbilityPoints(int val)
	{
		selectedCh.bindData.abilityPoints+=val;
		mainAttributeManager.inspectCharacter(selectedCh);
	}

	public void getSkillPoints(int val)
	{
		selectedCh.bindData.skillPoints+=val;
		skillBuildManager.inspectCharacter(selectedCh);
	}

    public void switchBtnTouched(int index)
    {
		if(index == 0)
		{
			mainAttributeManager.gameObject.SetActive(false);
			skillBuildManager.gameObject.SetActive(true);
		}
		else if(index==1)
        {
			mainAttributeManager.gameObject.SetActive(true);
			skillBuildManager.gameObject.SetActive(false);
		}
    }
	public void switchToCh(int index)
	{
		inspectCharacter(GameManager.instance.chs[index]);
	}
}

public interface IinspectPlayerable
{
	void inspectCharacter(Character ch);
}