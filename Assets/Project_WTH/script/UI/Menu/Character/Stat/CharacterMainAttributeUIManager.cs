using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace com.jerrch.rpg
{
public class CharacterMainAttributeUIManager:MonoBehaviour,IinspectPlayerable{
	Dictionary<MainAttributeType,CharacterMainAttributeUI> chStatUIDict;
	List<CharacterAttributeUI> propertyUIs;
	public CompositeText pointText;
	public Transform statPanel;
	void Awake()
	{
		chStatUIDict = new Dictionary<MainAttributeType,CharacterMainAttributeUI>();
		foreach(CharacterMainAttributeUI statUI in GetComponentsInChildren<CharacterMainAttributeUI>())
		{
			chStatUIDict.Add(statUI.type,statUI);
		}
		propertyUIs = new List<CharacterAttributeUI>();
		foreach(var text in statPanel.GetComponentsInChildren<CharacterAttributeUI>())
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
		foreach(MainAttributeType type in chStatUIDict.Keys)
		{
			print(type);
			var val = chData.getValue(type.ToString().ToLower()+"Val");
			chStatUIDict[type].updateUI(val);
		}
			
		pointText.text = chData.abilityPoints.ToString();
		
		//stats
		foreach(var property in propertyUIs)
		{
//			print(property.name );
			Attribute attr = ch.equipStat.getAttribute(property.type);
			//Attribute attr = ch.equipStat.getValue<Attribute>(property.name);
			property.setValue(attr.finalValue);
		}
	}
	
	
	public void statUIClicked(MainAttributeType statType)
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
