using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace com.jerrch.rpg
{
public class CharacterAbilityUIManager : Singleton<CharacterAbilityUIManager>,IinspectPlayerable{

	
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

	public void inspectCharacter(Character ch)
	{
		CharacterData chData = ch.bindData;
		if(chData.abilityPoints>0)
			enableStatButtons();
		
		//attributes
		foreach(AbilityType type in chStatUIDict.Keys)
			chStatUIDict[type].updateUI(chData.getValue(type.ToString().ToLower()+"Val"));
		pointText.text = chData.abilityPoints.ToString();
		
		//stats
		foreach(var property in propertyUIs)
		{
//			print(property.name );
			Attribute attr = ch.equipStat.getValue<Attribute>(property.name);
			property.setValue(attr.finalValue);
		}
	}
	
	
	public void statUIClicked(AbilityType statType)
	{
		CharacterData chData = GameManager.instance.currentCh.bindData;
		if(chData.abilityPoints==0)
		{
			return;
		}	
		chData.abilityPoints--;
		//statUIs[]
		pointText.text = chData.abilityPoints.ToString();

		//find value in chData, ex:(str+Val)
		int v = chData.addValue(statType.ToString().ToLower()+"Val",1);
		chStatUIDict[statType].updateUI(v);
		
		if(chData.abilityPoints==0)
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
