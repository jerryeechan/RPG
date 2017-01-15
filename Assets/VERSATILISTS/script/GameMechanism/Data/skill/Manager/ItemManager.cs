using UnityEngine;
using System.Collections.Generic;
using com.jerrch.rpg;
public class ItemManager : Singleton<ItemManager> {

	Dictionary<string,ItemGraphicAsset> itemGraphicDict;
	Dictionary<string,Item> itemDict;
	public ItemGraphicAsset[] itemGraphicTemplates;
	public Item[] itemTemplates;
	void OnValidate()
	{
		getItemTemplates();
	}
	//public List<Item> 
	void Awake()
	{
		getItemTemplates();
	}

	void getItemTemplates()
	{
		itemGraphicDict = new Dictionary<string,ItemGraphicAsset>();
		itemDict = new Dictionary<string,Item>();
		itemTemplates = GetComponentsInChildren<Item>();
		itemGraphicTemplates = GetComponentsInChildren<ItemGraphicAsset>();

		foreach(var itemGraphic in itemGraphicTemplates)
		{
			itemGraphicDict.Add(itemGraphic.id,itemGraphic);
		}
		foreach(var item in itemTemplates)
		{
			itemDict.Add(item.id,item);
		}
	}
	
	
	//public Item 
	public Item getItem(string key)
	{
		return itemDict[key];
	}
	public Item getItem(ItemData data)
	{
		if(itemDict.ContainsKey(data.id))
		{
			Item item = itemDict[data.id];
			//item.bindData = data;
			item.asset = itemGraphicDict[data.imageID];
			return item;	
		}
		else{
			Debug.LogError("Item Key not found"+data.id);
			return null;
		}
	} 
	public void playerGetItem(Item item)
	{
		switch(item.itemType)
		{
			case ItemType.Equip:
				Equip eq = item as Equip;
				//EquipData equipData = eq.gerateData();
				DataManager.instance.curPlayerData.addEquip(eq.gerateData());
				
				//eq.bindData = equipData;
			break;
			case ItemType.Consume:
				//ItemData itemData = 
				DataManager.instance.curPlayerData.addItem(item.gerateData());
				//item.bindData = itemData;
			break;
		}
	}
	public List<Item> generateShopItem(int level)
	{
		//TODO: level for different item set
		List<Item> items = new List<Item>();
		int totalNum = Random.Range(4,16);
		foreach (var itemDropRate in shopItemTemplatesLevel1)
		{
			
			if(itemDropRate.dropTest())
			{
				
				items.Add(getItem(itemDropRate.data));
			}
		}
		return items;
	}
	[SerializeField]
	//List<DropRate<ItemData>> shopItemTemplatesLevel1;
	List<ItemDropRate> shopItemTemplatesLevel1;

	public List<Item> generateShopItem(List<ItemDropRate> templates)
	{
		//TODO: level for different item set
		List<Item> items = new List<Item>();
		//int totalNum = Random.Range(4,16);
		foreach (var itemDropRate in templates)
		{
			if(itemDropRate.dropTest())
			{
				items.Add(getItem(itemDropRate.data));
			}
		}
		return items;
	}
	
	
}

[System.SerializableAttribute]
public class DropRate<T>
{
	public T data;
	public float rate = 1;
	public bool dropTest()
	{
		float r = Random.Range(0.0f,1.0f);
		Debug.Log("rate:"+rate+"random:"+r);
		if(rate >= r)
		{
			return true;
		}
		return false;
	}
}

[System.SerializableAttribute]
public class ItemDropRate
{
	public ItemData data;
	public float rate = 1;
	public bool dropTest()
	{
		float r = Random.Range(0.0f,1.0f);
		if(rate >= r)
		{
			return true;
		}
		return false;
	}
}
