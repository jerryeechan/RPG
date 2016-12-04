using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace com.jerrch.rpg
{
public class CharacterAbilityUIManager : Singleton<CharacterAbilityUIManager>,IDisplayable{

	
	Dictionary<AbilityType,CharacterAbilityUI> chStatUIDict;
	List<CharacterPropertyUI> propertyUIs;
	public CompositeText pointText;
	public Transform statPanel;
	void Awake()
	{
		chStatUIDict = new Dictionary<AbilityType,CharacterAbilityUI>();
		foreach(CharacterAbilityUI statUI in GetComponentsInChildren<CharacterAbilityUI>())
		{
			chStatUIDict.Add(statUI.type,statUI);
		}
		propertyUIs = new List<CharacterPropertyUI>();
		foreach(var text in statPanel.GetComponentsInChildren<CharacterPropertyUI>())
		{
			propertyUIs.Add(text);
		}
	}
	public void Show()
	{
		inspectCharacter(GameManager.instance.currentCh);
	}
	public void Hide()
	{

	}
	public void inspectCharacter(Character ch)
	{
		CharacterData chData = ch.chData;
		if(chData.statPoints>0)
			enableStatButtons();
		
		//attributes
		foreach(AbilityType type in chStatUIDict.Keys)
			chStatUIDict[type].updateUI(chData.getValue(type.ToString().ToLower()+"Val"));
		pointText.text = chData.statPoints.ToString();
		
		//stats
		foreach(var property in propertyUIs)
		{
			
			float v = ch.equipStat.getValue<float>(property.name);
			
				property.setValue(v);
		}
	}

	
	
	public void statUIClicked(AbilityType statType)
	{
		CharacterData chData = GameManager.instance.currentCh.chData;
		if(chData.statPoints==0)
		{
			return;
		}	
		chData.statPoints--;
		//statUIs[]
		pointText.text = chData.statPoints.ToString();

		//find value in chData, ex:(str+Val)
		int v = chData.addValue(statType.ToString().ToLower()+"Val",1);
		chStatUIDict[statType].updateUI(v);
		
		if(chData.statPoints==0)
		{
			disableStatButtons();
		}
		chDataModified();
	}
	public void chDataModified()
	{
		GameManager.instance.currentCh.updateValues();
		//GameManager.instance.currentCh.chUI.updateUI(GameManager.instance.currentCh.equipStat);
		inspectCharacter(GameManager.instance.currentCh);
	}
	public void enableStatButtons()
	{

	}
	public void disableStatButtons()
	{

	}
}
}
