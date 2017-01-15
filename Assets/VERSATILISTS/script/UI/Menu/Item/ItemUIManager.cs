using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

namespace com.jerrch.rpg
{
public class ItemUIManager : Singleton<ItemUIManager>,IDisplayable,IItemSlotManager,SwitchBtnTouchDelegate {
	ItemSlot[] itemSlots;
	public Image dragTempSlot;
	public RectTransform buttonPanel;
	[SerializeField]
	SwitchBtnPanel btnPanel;
	
	void Awake()
	{
		_instance = this;
		itemSlots = transform.Find("bg").GetComponentsInChildren<ItemSlot>();
		for(int i=0;i<itemSlots.Length;i++)
		{
			itemSlots[i].index = i;
			itemSlots[i].manager = this;
		}
		
		dragTempSlot = transform.Find("dragTempSlot").GetComponent<Image>();
		btnPanel.switchDelegate = this;
	}
	void Start()
	{
		Show();
	}

	[SerializeField]
	
	public void Show()
	{
		btnPanel.setup();
		//loadItems(DataManager.instance.curPlayerData.itemDataList);
		//loadEquips(DataManager.instance.curPlayerData.equipDataList);		
	}

	public void switchBtnTouched(int index)
	{
		if(index == 0)
		{
			loadEquips(DataManager.instance.curPlayerData.equipDataList);
		}
		else if(index == 1)
		{
			loadItems(DataManager.instance.curPlayerData.itemDataList);
		}
	}
	public void Hide()
	{

	}
	public void loadEquips(EquipData[] equipList)
	{
		for (int i = 0; i < equipList.Length; i++)
		{
			setItem(equipList[i],i);
		}

	}
	public void loadItems(ItemData[] itemList)
	{
		
		for (int i = 0; i < itemList.Length; i++)
		{
			setItem(itemList[i],i);	
		}
	}
	public void setItem(ItemData itemData,int slotIndex)
	{
		if(itemData==null)
		{
			itemSlots[slotIndex].bindItem = null;
		}
		else
		{
			EquipData eq = itemData as EquipData;
			if(eq!=null)
			{
				itemSlots[slotIndex].bindItem =eq.getItem();	
			}
			else
				itemSlots[slotIndex].bindItem = itemData.getItem();
		}
		
	}
	int findNextEmptySlot()
	{
		for(int i=0;i<itemSlots.Length;i++)
		{
			if(itemSlots[i].bindItem==null)
			{
				return i;
			}
		}
		return -1;
	}
	public ItemSlot selectedSlot;
	
	ItemSlot draggingSlot;
	bool dropSuccess;
	bool startDrag = false;

	Item draggingItem;
	public void itemBeginDrag(ItemSlot slot)
	{
		startDrag = true;
		dropSuccess = false;
		draggingItem = slot.bindItem;
		dragTempSlot.enabled = true;
		dragTempSlot.sprite = draggingItem.asset.iconSprite;
		
		//dragItem.enabled = true;
		dragTempSlot.transform.position = slot.transform.position;
		draggingSlot = slot;
	}
	public void itemOnDrag(PointerEventData eventData)
	{
		
		//this is the ui element
		RectTransform rectT = dragTempSlot.GetComponent<RectTransform>();
		float scale = UIManager.instance.GetComponent<Canvas>().scaleFactor;
		Vector2 d = eventData.delta/scale;
		rectT.anchoredPosition+=d;
	}
	
	//TODO refactor switch tab (seperate item and equip behavior);
	//TODO: maybe: ItemDataHolder, eaiser to switch?????
	public void itemOnDrop(ItemSlot dropSlot)
	{
		switch(draggingSlot.slotType)
		{
			//in bag
			case ItemSlotType.Item:
				
				if(dropSlot.slotType==ItemSlotType.Item)
				{	
					//itemslot to itemslot: switch
					print("itemslot to itemslot: switch");
					dropSuccess = true;
				}
				else if((draggingItem as Equip)&&dropSlot.slotType == ItemSlotType.Equip)
				{
					//equip to equipslot  :wear or switch
					//is a equipslot, and the equip are same part
					if((draggingItem as Equip).equipType == (dropSlot as EquipSlot).equipType)
					{
						//wear
						GameManager.instance.currentCh.wear(draggingItem as Equip);
						DataManager.instance.curPlayerData.equipDataList[draggingSlot.index] = (dropSlot._bindItem as Equip).gerateData();
						dropSuccess = true;
					}
				}
			break;
			case ItemSlotType.Equip:
				//equipslot to empty   : takeoff
				if(dropSlot.isEmpty&&dropSlot.slotType == ItemSlotType.Item)
				{
					
					dropSuccess = true;
				}
			break;
			
		}
		if(dropSuccess == true)
		{
			print(draggingItem);
			print(dropSlot.bindItem);	
			draggingSlot.bindItem = dropSlot.bindItem;
			dropSlot.bindItem = draggingItem;
		}
	}
	public bool itemEndDrag()
	{	
		startDrag = false;
		draggingItem = null;
		dragTempSlot.enabled = false;
		return dropSuccess;
	}

	
	
	//equip to equipslot  :wear or switch
	
	public void OnPointerEnter(ItemSlot hoverSlot)
	{
		
		
	}
	public void itemSlotTouched(int index)
	{
		selectedSlot =  itemSlots[index];
	}
	public void showItem(bool doshowButton)
	{
		DescriptionUIManager.instance.showItem(selectedSlot.bindItem);
		if(doshowButton)
		{
			if(selectedSlot.bindItem!=null)
				showButton();
		}
	}
	

	void useBtnTouched()
	{
		
	}

	void dropBtnTouched()
	{

	}

	void showButton()
	{
		
		buttonPanel.DOAnchorPosY(0,0.5f,true);
		
		buttonPanel.Find("Use button").GetComponentInChildren<CompositeText>().text = selectedSlot.bindItem.getUseText();
		buttonPanel.Find("Drop button").GetComponentInChildren<CompositeText>().text = "Drop";
	}
	void hideButton()
	{
		buttonPanel.DOAnchorPosY(-10,0.5f,true);
	}


	public void wear()
	{
		
		remove();
	}
	public void takeOff(EquipSlot eqSlot)
	{

	}

	public void useItem()
	{
		//use 
		selectedSlot.remove();
	}
	public void dropItem()
	{
		selectedSlot.remove();
	}
	public ItemSlot findItem(Item item)
	{
		foreach(var slot in itemSlots)
		{
			if(slot.bindItem && slot.bindItem.itemName == item.itemName)
			{
				return slot;
			}
		}
		return null;
	}
	/*
	public void pickUpItem(Item item,int amount = 1)
	{
		if(item.stackable)
		{
			ItemSlot slot = findItem(item);
			if(slot==null)
			{
				//find new for the slot
				createItemInNewSlot(item);
				//slot.changeNum(amount-1);
			}
			else
			{
				slot.changeNum(amount);
			}
		}
		else
		{
			createItemInNewSlot(item);
		}
		//Item item = ItemManager.instance.getItem(data);
	}
	public bool createItemInNewSlot(Item item)
	{
		int index = findNextEmptySlot();
		if(index == -1)
		{
			//bag full
			return false;
		}
		else
		{
			//ItemManager.instance.createItemData(item);
			setItem(item,index);
			return true;
		}
	}*/

	public void sell()
	{
		//DataManager.instance.curPlayerData.gold

		remove();
	}
	public void remove()
	{
		
	}

       
    }
	
}