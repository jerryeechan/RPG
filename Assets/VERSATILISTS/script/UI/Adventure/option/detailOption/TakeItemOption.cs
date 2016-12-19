using UnityEngine;
using System.Collections;
using com.jerrch.rpg;
public class TakeItemOption :AdventureOption {
	override public void choose()
	{
		print(name);
		print(item);
		//if(item==null)
		//	item = RewardManager.instance.getRewards("default");

		ItemUIManager.instance.pickUpItem(item,1);
	}
	public Item item;
	public Item getItemInfo()
	{
		if(item != null)
		{
			return item;
		}
		else
			return RewardManager.instance.getRewards("default");
		
	}
}
