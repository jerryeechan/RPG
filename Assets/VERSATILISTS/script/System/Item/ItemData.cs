using UnityEngine;
using System.Collections;

[System.SerializableAttribute]
public class ItemData{
	public string id;
	public string imageID;
	public int num = 1;
	public void deleteData()
	{
		DataManager.instance.curPlayerData.itemDataList.Remove(this);
	}
}
