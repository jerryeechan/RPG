using UnityEngine;
using System.Collections;

public class DungeonPlayerStateUI : Singleton<DungeonPlayerStateUI> {

	public CompositeText descriptionText;
	public CompositeText goldText;
	public CompositeText doomText;
	AnimatableCanvas goldPanel;
	public DungeonCharacterUI[] chUIs;
	
	// Use this for initialization
	void Start () {
		chUIs = GetComponentsInChildren<DungeonCharacterUI>();
		goldPanel = goldText.GetComponentInParent<AnimatableCanvas>();
		//descriptionText.gameObject.SetActive(true);
	}
	
	
	// Update is called once per frame
	public void popUpText(string text)
	{
		descriptionText.gameObject.SetActive(true);
		descriptionText.PopText(text);
	}
	public void getGold(int g)
	{
		int startGold = DataManager.instance.curPlayerData.gold;
		goldText.DOValue(startGold,g); 
		DataManager.instance.curPlayerData.gold+=g;
	}

	public void getExp(int exp)
	{
		foreach(var ch in chUIs)
		{
			ch.getExp(exp);
		}
	}

	public DungeonDoomEvent doomEvent;
	public void getDoom(int doom)
	{
		int startDoom = DataManager.instance.curPlayerData.doom;
		int final = Mathf.Clamp(startDoom+doom,0,100);
		
		doomText.DOValue(startDoom,final - startDoom);
		DataManager.instance.curPlayerData.doom = final;
		if(final == 100)
		{
			// doom has come
			doomEvent.encounter();
		}
	}
	public void CombatMode()
	{
		goldPanel.hide(0.5f);
	}
	public void DungeonMode()
	{
		goldPanel.show(0.5f);
	}
}
