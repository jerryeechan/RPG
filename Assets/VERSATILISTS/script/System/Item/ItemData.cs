using UnityEngine;
using System.Collections;
using com.jerrch.rpg;
[System.SerializableAttribute]
public class ItemData{
	public ItemData(string id,string imageID)
    {
        this.id = id;
		this.imageID = imageID;
    }
	public string id;
	public string imageID;
	public int num = 1;

	public Item getItem(){
		return ItemManager.instance.getItem(this);
	}
}
