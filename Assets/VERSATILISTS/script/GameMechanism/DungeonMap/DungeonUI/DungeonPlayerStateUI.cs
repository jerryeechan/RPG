using UnityEngine;
using System.Collections;

public class DungeonPlayerStateUI : Singleton<DungeonPlayerStateUI> {

	public CompositeText descriptionText;
	public CompositeText goldText;
	AnimatableCanvas goldPanel;
	public DungeonCharacterUI[] chUIs;
	
	// Use this for initialization
	void Start () {
		chUIs = GetComponentsInChildren<DungeonCharacterUI>();
		goldPanel = goldText.GetComponentInParent<AnimatableCanvas>();
	}
	
	
	// Update is called once per frame
	public void popUpText(string text)
	{
		descriptionText.PopText(text);
	}
	public void getGold(int g)
	{
		int startGold = DataManager.instance.curPlayerData.gold;
		goldText.DOValue(startGold,g); 
		DataManager.instance.curPlayerData.gold+=g;
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
