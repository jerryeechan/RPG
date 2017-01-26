using System;
using System.Collections.Generic;
[Serializable]
public class PlayerData{
	public string name;
	public List<CharacterData> chData;
	
	//public CharacterData curChData;
	public ItemData[] itemDataList = new ItemData[10];
	public EquipData[] equipDataList = new EquipData[10];
	bool addToFirstEmpty<T>(T[] array,T element)
	{
		for(int i=0;i<array.Length;i++)
		{
			if(array[i]==null)
			{
				array[i] = element;
				return true;
			}
		}
		return false;
	}
	public bool addItem(ItemData item)
	{
		return addToFirstEmpty<ItemData>(itemDataList,item);
	}
	public bool addEquip(EquipData item)
	{
		return addToFirstEmpty<EquipData>(equipDataList,item);
	}

	public List<SkillData> skillDataList;
	public int gold;
	
	//when reach 100, bad things happens
	public int doom;

	//game progress
}
