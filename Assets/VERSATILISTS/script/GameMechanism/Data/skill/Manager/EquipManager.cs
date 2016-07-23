using UnityEngine;
using System.Collections.Generic;

public class EquipManager :Singleton<EquipManager> {
	List<Equip> equipList;
	Dictionary<string,Equip> equipDict;
	public Equip getEquip(string name)
	{
		return equipDict[name];
	}
	
	public EquipData generateRandomEquipData(string name)
	{ 
		//for random stat
		return null;
	}
	 
}
