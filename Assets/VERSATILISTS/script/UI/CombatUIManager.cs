using UnityEngine;
using System.Collections;

public class CombatUIManager : Singleton<CombatUIManager> {
	public GameObject actionPanel;
	public GameObject actionDetailPanel;
	
	public void DiceRollDone()
	{
		actionPanel.SetActive(true);
		//actionDetailPanel.SetActive(true);
	}
	public void UseActionDone()
	{
		actionPanel.SetActive(false);
		//actionDetailPanel.SetActive(false);
	}
	
}
