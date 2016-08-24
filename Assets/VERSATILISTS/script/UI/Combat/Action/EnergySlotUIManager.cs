using UnityEngine;
using System.Collections.Generic;

public class EnergySlotUIManager : Singleton<EnergySlotUIManager> {

	int energyLeft;
	EnergySlotUI[] energySlots;
	void Awake()
	{
		energySlots = GetComponentsInChildren<EnergySlotUI>();
		
	}
	void Start()
	{
		foreach(var slot in energySlots)
		{
			slot.occupy();
		}
	}
	public bool occupyTest(int cost)
	{
		if(energyLeft>=cost)
		{
			for(int i=0;i<cost;i++)
			{
				energySlots[energyLeft-1].occupy();
				energyLeft --;
			}
			return true;
		}
		else
		{
			return false;
		}
	}
	public void removeAll()
	{
		for(int i=0;i<energyLeft;i++)
		{
			energySlots[i].occupy();
		}
	}
	public void generateEnergy(int v)
	{
		Debug.Log("generate energy:"+v);
		energyLeft = v;
		for(int i=0;i<energyLeft;i++)
		{
			energySlots[i].recover();
		}
	}
}
