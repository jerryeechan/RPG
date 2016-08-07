using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public ItemData bindData;
	//public ItemUI bindUI;
	public ItemGraphicAsset asset;

	public string description;
	public int price;

	public void sell()
	{
		DataManager.instance.curPlayerData.gold += price;
	}
}
