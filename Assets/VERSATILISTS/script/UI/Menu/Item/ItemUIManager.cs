using UnityEngine;
using System.Collections;

public class ItemUIManager : Singleton<ItemUIManager> {
	ItemUI[] itemSlots;
	public CompositeText goldText;
	void Awake()
	{
		itemSlots = GetComponentsInChildren<ItemUI>();
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
	ItemUI currentItem;
	public void itemTouched(int id)
	{
		currentItem = itemSlots[id];
		
		//select the item
		selected();
	}
	
	public void selected()
	{
		//show description
		DescriptionUIManager.instance.showItem(currentItem.bindItem);
	}
	public void wear()
	{
		
		remove();
	}

	public void use()
	{
		remove();
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
