using UnityEngine;
using System.Collections;

public class CombatUIManager : Singleton<CombatUIManager> {
	public GameObject actionPanel;
	public GameObject actionDetailPanel;
	
	public void OnDiceRollDone(int sum)
	{
		actionPanel.SetActive(true);
		EnergySlotUIManager.instance.removeAll();
		EnergySlotUIManager.instance.generateEnergy(sum);
		//actionDetailPanel.SetActive(true);
	}
	public void UseActionDone()
	{
		actionPanel.SetActive(false);
		//actionDetailPanel.SetActive(false);
	}
	
}
