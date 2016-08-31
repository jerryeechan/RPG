using UnityEngine;
using System.Collections;

public class ItemUIManager : Singleton<ItemUIManager> {
	ItemSlot[] itemSlots;
	public CompositeText goldText;
	void Awake()
	{
		itemSlots = GetComponentsInChildren<ItemSlot>();
		for(int i=0;i<itemSlots.Length;i++)
		{
			itemSlots[i].index = i;
		}
	}
	void Start()
	{
		updateGold();
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
	
	public void selected()
	{
		//show description
		DescriptionUIManager.instance.showItem(selectedItem.bindItem);
	}
	public void wear()
	{
		
		remove();
	}

	public void use()
	{
		//use 

		remove();
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

	public void updateGold()
	{
		goldText.text = DataManager.instance.curPlayerData.gold.ToString();
	}
}
