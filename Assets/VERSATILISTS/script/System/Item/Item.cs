using UnityEngine;
using System.Collections;
using com.jerrch.rpg;
public class Item : MonoBehaviour {

	[HideInInspector]
	public ItemData bindData;
	//public ItemUI bindUI;
	public ItemGraphicAsset asset;

	public string description;
	public string itemName;
	public string recommendationText;
	public int price;
	public bool stackable = true;
	public ItemType itemType;
	
	protected virtual void Reset()
	{
		
	}

	public virtual void use()
	{
		
	}
	public virtual void sell()
	{
		DataManager.instance.curPlayerData.gold += price;
		remove();
	}
	public virtual void remove()
	{
		if(stackable)
		{
			bindData.num--;
			if(bindData.num == 0)
			{
				DataManager.instance.curPlayerData.itemDataList.Remove(bindData);
			}
		}
	}



	public string getUseText()
	{
		switch(itemType)
		{
			case ItemType.Consume:
				return "Use";
			case ItemType.Equip:
				return "Equip";
			case ItemType.Quest:
				return "..";

			default:
				return "NONE";
		}
		
	}
}
public enum ItemType
{
	Consume,Equip,Quest,Place
}