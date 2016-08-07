using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class CharacterStatUIManager : Singleton<CharacterStatUIManager> {

	
	Dictionary<string,CharacterStatUI> chStatUIDict;
	List<CompositeText> statTexts;
	public CompositeText pointText;
	public Transform statPanel;
	void Awake()
	{
		chStatUIDict = new Dictionary<string,CharacterStatUI>();
		foreach(CharacterStatUI statUI in GetComponentsInChildren<CharacterStatUI>())
		{
			chStatUIDict.Add(statUI.name,statUI);
		}
		statTexts = new List<CompositeText>();
		foreach(var text in statPanel.GetComponentsInChildren<CompositeText>())
		{
			statTexts.Add(text);
		}
		
	}
	
	void Start()
	{
		
	}
	public void viewCharacter(Character ch)
	{
		currentCh = ch;
		CharacterData chData = ch.chData;
		if(chData.statPoints>0)
			enableStatButtons();
		
		//attributes
		foreach(string type in chStatUIDict.Keys)
			chStatUIDict[type].updateUI(chData.getValue(type));
		pointText.text = chData.statPoints.ToString();
		
		//stats
		foreach(var text in statTexts)
		{
//			print(text.name);
			text.text = ((int)ch.equipStat.getValue<float>(text.name)).ToString();
		}

	}
	Character currentCh;
	public void statUIClicked(string statType)
	{
		CharacterData chData = currentCh.chData;
		if(chData.statPoints==0)
		{
			return;
		}	
		chData.statPoints--;
		//statUIs[]
		pointText.text = chData.statPoints.ToString();

		int v = chData.addValue(statType,1);
		chStatUIDict[statType].updateUI(v);
		
		if(chData.statPoints==0)
		{
			disableStatButtons();
		}
	}

	public void enableStatButtons()
	{

	}
	public void disableStatButtons()
	{

	}
}
