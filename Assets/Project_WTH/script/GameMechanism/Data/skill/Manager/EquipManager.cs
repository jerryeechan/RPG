using UnityEngine;
using System.Collections.Generic;

public class EquipManager :Singleton<EquipManager> {
	Equip[] equipList;
	Dictionary<string,Equip> equipDict;

	EquipGraphicAsset[] equipGraphicList;
	Dictionary<string,EquipGraphicAsset> equipGraphicDict;
	void Awake()
	{
		equipDict = new Dictionary<string,Equip>();
		equipList = GetComponentsInChildren<Equip>();
		equipGraphicList = GetComponentsInChildren<EquipGraphicAsset>();
		equipGraphicDict = new Dictionary<string,EquipGraphicAsset>();
		foreach(var eq in equipList)
		{
			equipDict.Add(eq.name,eq);
		}

		foreach(var eqG in equipGraphicList)
		{
			if(equipGraphicDict.ContainsKey(eqG.name))
			{
				print("Equip exist:"+eqG.name);
			}
			else
				equipGraphicDict.Add(eqG.name,eqG);
		}
	}
	public Equip getEquip(ItemData data)
	{
		if(data == null)
		{
			return null;
		}
		else
			return getEquip(data.id,data.imageID);
	}
	public Equip getEquip(string id,string imageID)
	{
		if(equipDict.ContainsKey(id))
		{
			//TODO: may not need Instantiate
			Equip eq = Instantiate(equipDict[id]);
			if(equipGraphicDict.ContainsKey(imageID))
			{
				eq.bindGraphic = equipGraphicDict[imageID];
				//eq.asset = equipGraphicDict[imageID];
			}
			return eq;
		}
		else if(id=="")
		{
			return null;
		}
		else
		{
			Debug.LogError("No equip data"+id);
			return null;
		}	
	}
	public Equip getEquipFromData(EquipData data)
	{
		
		if(equipDict.ContainsKey(data.id))
		{
			Equip eq = Instantiate(equipDict[data.id]);
			if(equipGraphicDict.ContainsKey(data.imageID))
			{
				eq.bindGraphic = equipGraphicDict[data.imageID];
				//eq.asset = equipGraphicDict[data.imageID];
			}
			return eq;
		}
		else
			return null;
	}
	
	public EquipData generateRandomEquipData(string name)
	{ 
		//for random stat
		return null;
	}

	public List<Item> generateShopEquip(int level)
	{
		//TODO
		return generateShopEquip(shopEquipTemplatesLevel1);
	}

	public List<Item> generateShopEquip(List<ItemDropRate> templates)
	{
		List<Item> items = new List<Item>();
		int totalNum = Random.Range(4,16);
		foreach (var itemDropRate in templates)
		{
			if(itemDropRate.dropTest())
			{
				items.Add(getEquip(itemDropRate.data));
			}
		}
		return items;
	}

	[SerializeField]
	List<ItemDropRate> shopEquipTemplatesLevel1;
	 
}
