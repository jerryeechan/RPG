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
			equipGraphicDict.Add(eqG.name,eqG);
		}
	}
	public Equip getEquip(string name,string imageID)
	{
		if(equipDict.ContainsKey(name))
		{
			Equip eq = equipDict[name];
			if(equipGraphicDict.ContainsKey(imageID))
			eq.bindGraphic = equipGraphicDict[imageID];
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
	 
}
