using UnityEngine;
using System.Collections;

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

	public int energy{
		get{
			return energyLeft;
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
		StartCoroutine(recover());
	}

	IEnumerator recover()
	{
		for(int i=0;i<energyLeft;i++)
		{
			energySlots[i].recover();
			yield return new WaitForSeconds(0.1f);
		}
	}
}
