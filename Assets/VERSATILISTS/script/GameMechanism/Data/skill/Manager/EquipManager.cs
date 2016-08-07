using UnityEngine;
using System.Collections.Generic;

public class EquipManager :Singleton<EquipManager> {
	Equip[] equipList;
	Dictionary<string,Equip> equipDict;

	EquipGraphicAsset[] equipGraphicList;
	Dictionary<string,Equip> equipGraphicDict;
	void Awake()
	{
		equipDict = new Dictionary<string,Equip>();
		equipList = GetComponentsInChildren<Equip>();
		foreach(var eq in equipList)
		{
			equipDict.Add(eq.name,eq);
		}
	}
	public Equip getEquip(string name)
	{
		if(equipDict.ContainsKey(name))
			return equipDict[name];
		else
			return null;
	}
	
	public EquipData generateRandomEquipData(string name)
	{ 
		//for random stat
		return null;
	}
	 
}
