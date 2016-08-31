using UnityEngine;
using System.Collections;

public class CombatUIManager : Singleton<CombatUIManager> {
	public GameObject actionPanel;
	public GameObject actionDetailPanel;
	
	
	public void UseActionDone()
	{
		//actionDetailPanel.SetActive(false);
	}
	
}
