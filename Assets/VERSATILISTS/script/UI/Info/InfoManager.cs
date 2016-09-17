using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class InfoManager : Singleton<InfoManager> {
	public bool isOpen = false;
	
	public EquipSlot[] equipSlots;
	Dictionary<InfoTabType,InfoTab> tabDict;
	public InfoTab[] tabPanels;
	GameObject panel;
	
	void Awake()
	{
		_instance = this;
		isOpen = true;
		panel = transform.Find("panel").gameObject;

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

		if(currentTab==null)
		{
			currentTab = tabDict[InfoTabType.Bag];
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
		panel.SetActive(true);
		currentTab.gameObject.SetActive(true);

		DungeonPlayerStateUI.instance.descriptionText.text = "";
		isOpen = true;
		ItemUIManager.instance.Show();
		inspectCharacter(GameManager.instance.currentCh);
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
		panel.SetActive(false);
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