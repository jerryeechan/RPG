using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using DG.Tweening;
public class ItemUIManager : Singleton<ItemUIManager> {
	ItemSlot[] itemSlots;
	public Image dragItem;
	public RectTransform buttonPanel;
	void Awake()
	{
		itemSlots = GetComponentsInChildren<ItemSlot>();
		for(int i=0;i<itemSlots.Length;i++)
		{
			itemSlots[i].index = i;
		}

		dragItem = transform.Find("dragItem").GetComponent<Image>();
	}
	void Start()
	{	
		loadItems(DataManager.instance.curPlayerData.itemDataList);
	}
	public void loadItems(List<ItemData> itemList)
	{
		int i=0;
		foreach(var itemData in itemList)
		{
			Item item = ItemManager.instance.getItem(itemData);
			//item.transform.SetParent(saveTransform);
			ItemUIManager.instance.setItem(item,i);
			i++;
		}
		for(;i<itemSlots.Length;i++)
		{
			ItemUIManager.instance.setItem(null,i);
		}
	}
	public void setItem(Item item,int slotIndex)
	{
		itemSlots[slotIndex].setItem(item);
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
	ItemSlot selectedItem;
	int nextEmptyIndex = 0;
	public void itemTouched(int id)
	{
		selectedItem = itemSlots[id];
		//select the item
		selected();
	}
	ItemSlot draggingSlot;
	bool dropSuccess;
	public void itemBeginDrag(ItemSlot slot)
	{
		dropSuccess = false;
		dragItem.sprite = slot.bindItem.asset.iconSprite;
		dragItem.enabled = true;
		dragItem.transform.position = slot.transform.position;
		draggingSlot = slot;
	}
	public void itemOnDrag(PointerEventData eventData)
	{
		
		//this is the ui element
		RectTransform UI_Element = dragItem.GetComponent<RectTransform>();
		//first you need the RectTransform component of your canvas
		
		float scale = UIManager.instance.GetComponent<Canvas>().scaleFactor;
		Vector2 d = eventData.delta/scale;
		UI_Element.anchoredPosition+=d;
	}
	
	public void itemOnDrop()
	{
		dropSuccess = true;
		Item item = draggingSlot.bindItem;
		print("dragItem"+item);
		print("dropbSlot"+currentHoverSlot.bindItem);
		draggingSlot.setItem(currentHoverSlot.bindItem);
		currentHoverSlot.setItem(item);
	}
	public bool itemEndDrag()
	{
		dragItem.enabled = false;
		return dropSuccess;
	}
	ItemSlot currentHoverSlot;
	public void OnPointerEnter(ItemSlot item)
	{
		currentHoverSlot = item;
	}
	public void selected()
	{
		//show description
		DescriptionUIManager.instance.showItem(selectedItem.bindItem);
		
		showButton();
		
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
		
		buttonPanel.Find("Use button").GetComponentInChildren<CompositeText>().text = selectedItem.bindItem.getUseText();
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

	public void useItem()
	{
		//use 

		remove();
	}
	public void dropItem()
	{
		
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
