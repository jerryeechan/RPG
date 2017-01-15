using UnityEngine;
using System.Collections;
using com.jerrch.rpg;
public class Item : MonoBehaviour {

	[HideInInspector]
	public ItemData bindData;
//TODO:	remove bindData??

	//public ItemUI bindUI;
	public ItemGraphicAsset asset;
	public string description;
	public string id;
	public string itemName;
	public string recommendationText;
	public int price;
	public bool stackable = true;
	public ItemType itemType;

	void OnValidate()
	{
		id = name;
	}
	
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

	//TODO ??
	public virtual void remove()
	{
		if(stackable)
		{
			bindData.num--;
			if(bindData.num == 0)
			{
				//DataManager.instance.curPlayerData[bindData];
			}
		}
	}
	public ItemData gerateData()
	{
		return new ItemData(id,asset.id);
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