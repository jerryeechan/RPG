using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.jerrch.rpg;
public class StatChUIManager : SingletonChUIManager<StatChUIManager>,IDisplayable{

	[SerializeField]
	List<HandButton> chButtons;

	[SerializeField]
	ActionBuildUIManger actionBuildManager;
	[SerializeField]
	CharacterAbilityUIManager abilityManager;
	[SerializeField]
	EquipUIManager equipUIManager;
	
	List<IinspectPlayerable> inspectables;
	override protected void Awake()
	{
		base.Awake();
		inspectables = new List<IinspectPlayerable>();
		inspectables.Add(actionBuildManager);
		inspectables.Add(abilityManager);
		inspectables.Add(equipUIManager);
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
			ins.inspectCharacter(ch);
		}
	}
	public void getAbilityPoints(int val)
	{
		selectedCh.bindData.abilityPoints+=val;
		CharacterAbilityUIManager.instance.inspectCharacter(selectedCh);
	}

	public void getSkillPoints(int val)
	{
		selectedCh.bindData.skillPoints+=val;
		actionBuildManager.inspectCharacter(selectedCh);
	}
}

public interface IinspectPlayerable
{
	void inspectCharacter(Character ch);
}