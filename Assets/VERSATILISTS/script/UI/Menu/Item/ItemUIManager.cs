using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using DG.Tweening;
public class ItemUIManager : Singleton<ItemUIManager>,IDisplayable {
	ItemSlot[] itemSlots;
	public Image dragTempSlot;
	public RectTransform buttonPanel;
	void Awake()
	{
		
		itemSlots = transform.Find("bg").GetComponentsInChildren<ItemSlot>();
		for(int i=0;i<itemSlots.Length;i++)
		{
			itemSlots[i].index = i;
		}

		dragTempSlot = transform.Find("dragTempSlot").GetComponent<Image>();
	}
	public void Show()
	{
		loadItems(DataManager.instance.curPlayerData.itemDataList);
	}
	public void Hide()
	{

	}
	public void loadItems(List<ItemData> itemList)
	{
		int i=0;
		if(itemList!=null)
		{	
			foreach(var itemData in itemList)
			{
				Item item = ItemManager.instance.getItem(itemData);
				//item.transform.SetParent(saveTransform);
				ItemUIManager.instance.setItem(item,i);
				i++;
			}
		}
		for(;i<itemSlots.Length;i++)
		{
			ItemUIManager.instance.setItem(null,i);
		}
	}
	public void setItem(Item item,int slotIndex)
	{
		itemSlots[slotIndex].bindItem = item;
	}
	int findNextEmptySlot()
	{
		for(int i=0;i<itemSlots.Length;i++)
		{
			if(itemSlots[i].bindItem==null)
			{
				nextEmptyIndex = i;
				return i;
			}
		}
		return -1;
	}
	public ItemSlot selectedSlot;
	int nextEmptyIndex = 0;
	public void itemTouched(ItemSlot slot)
	{
		selectedSlot =  slot;
	}
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
			if(slot.bindItem.name == item.name)
			{
				return slot;
			}
		}
		return null;
	}
	public void pickUpItem(Item item,int amount = 1)
	{
		if(item.stackable)
		{
			ItemSlot slot = findItem(item);
			if(slot==null)
			{
				//find new for the slot
				createItemInNewSlot(item);
				slot.changeNum(amount-1);
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
			ItemManager.instance.createItemData(item);
			setItem(item,index);
			return true;
		}
	}

	public void sell()
	{
		//DataManager.instance.curPlayerData.gold

		remove();
	}
	public void remove()
	{
		
	}
}
