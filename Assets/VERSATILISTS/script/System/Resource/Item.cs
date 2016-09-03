using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public ItemData bindData;
	//public ItemUI bindUI;
	public ItemGraphicAsset asset;

	public string description;
	public string itemName;
	public int price;
	public bool stackable = true;
	public ItemType type;
	
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
		switch(type)
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
	Consume,Equip,Quest
}