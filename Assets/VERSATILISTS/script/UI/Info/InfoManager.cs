using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class InfoManager : Singleton<InfoManager> {
	public bool isOpen = false;
	
	public EquipSlot[] equipSlots;
	Dictionary<InfoTabType,InfoTab> tabDict;
	public InfoTab[] tabPanels;
	
	void Awake()
	{
		isOpen = true;
		equipSlotDict = new Dictionary<EquipType,EquipSlot>();
		foreach(var eqslot in equipSlots)
		{
			equipSlotDict.Add(eqslot.equipType,eqslot);
		}

		tabDict = new Dictionary<InfoTabType,InfoTab>();

		foreach(var tab in tabPanels)
		{
			tabDict.Add(tab.type,tab);
			if(tab.gameObject.activeSelf)
			{
				currentTab = tab;
			}
		}

	}
	public void init()
	{
		if(isOpen)
		{
			Show();
		}
	}
	public void Show()
	{
		gameObject.SetActive(true);
		DungeonPlayerStateUI.instance.descriptionText.text = "";
		isOpen = true;
		inspectCharacter(GameManager.instance.currentCh);
		ItemUIManager.instance.Show();
		CharacterAbilityUIManager.instance.viewCharacter(GameManager.instance.currentCh);
		
		if(tabDict[InfoTabType.Action].gameObject.activeSelf == true)
		{
			switchTab(InfoTabType.Action);
		}
		else{
			switchTab(InfoTabType.Bag);
		}
	}

	public void Hide(){
		gameObject.SetActive(false);
		isOpen = false;
	}

	
	Dictionary<EquipType,EquipSlot> equipSlotDict;
	public void inspectCharacter(Character ch)
	{	
		if(ch == null)
			return;
		foreach(var eqtype in Equip.AllEquipType)
		{
			Equip eq = ch.getEquip(eqtype);
			if(eq != null)
			{
				equipSlotDict[eqtype].bindItem = eq;	
			}
		}
		CharacterAbilityUIManager.instance.viewCharacter(ch);
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

public enum InfoTabType{Bag,Action,Quest};