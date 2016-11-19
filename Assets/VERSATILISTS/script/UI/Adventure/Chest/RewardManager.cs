using UnityEngine;
using System.Collections.Generic;

public class RewardManager :Singleton<RewardManager> {
	
	public string[] consumablekeys;
	public string[] equipkeys;

	void Awake()
	{
		
	}

	public Item getRewards(string type)
	{
		
		return ItemManager.instance.getItem(consumablekeys[0]);
	}
}
