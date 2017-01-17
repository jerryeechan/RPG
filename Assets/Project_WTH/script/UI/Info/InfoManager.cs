using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using com.jerrch.rpg;
public class InfoManager : Singleton<InfoManager> {
	public bool isOpen = false;
	
	
	Dictionary<InfoTabType,InfoTab> tabDict;
	InfoTab[] tabPanels;
	GameObject panel;
	
	void Awake()
	{
		_instance = this;
		isOpen = true;
		panel = transform.Find("panel").gameObject;
		tabPanels = GetComponentsInChildren<InfoTab>(true);
		tabDict = new Dictionary<InfoTabType,InfoTab>();
		foreach(var tab in tabPanels)
		{
			tabDict.Add(tab.type,tab);
			if(tab.gameObject.activeSelf)
			{
				currentTab = tab;
			}
		}

		if(currentTab==null)
		{
			currentTab = tabDict[InfoTabType.Adventure];
		}
	}
	
	public void init()
	{
		foreach(var tab in tabPanels)
		{
			if(tab != currentTab)
			{
				tab.gameObject.SetActive(true);
				tab.gameObject.SetActive(false);
			}
		}
		if(isOpen)
		{
			Show();
		}
	}
	public void Show()
	{
		panel.SetActive(true);
		currentTab.show();
		PlayerStateUI.instance.descriptionText.text = "";
		isOpen = true;
		//ItemUIManager.instance.Show();
		//inspectCharacter(GameManager.instance.currentCh);
		
		
		
	}




	public void Hide(){
		panel.SetActive(false);
		isOpen = false;
	}

	
	
	public void inspectCharacter(Character ch)
	{	
		if(ch == null)
			return;
	}

	public InfoTab currentTab;
	public void switchTab(InfoTabType type)
	{
		if(currentTab!=tabDict[type])
		{
			tabDict[type].show();
			currentTab.hide();
			currentTab = tabDict[type];
		}
	}
}

public enum InfoTabType{Bag,Action,Adventure,Shop,DiceFactory};
