using UnityEngine;
using System.Collections;
using com.jerrch.rpg;
public class PlayerStateUI : Singleton<PlayerStateUI> {

	public CompositeText descriptionText;
	public CompositeText goldText;
	public CompositeText doomText;
	AnimatableCanvas doomPanel;
	AnimatableCanvas goldPanel;
	public CharacterUI[] chUIs;
	
	// Use this for initialization
	void Awake () {
		chUIs = GetComponentsInChildren<CharacterUI>();
		goldPanel = goldText.GetComponentInParent<AnimatableCanvas>();
		//doomPanel = doomText.GetComponentInParent<AnimatableCanvas>();
		//lastUI = chUIs[0];
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

	
	public void getDoom(int doom)
	{
		int startDoom = DataManager.instance.curPlayerData.doom;
		int final = Mathf.Clamp(startDoom+doom,0,100);
		
		doomText.DOValue(startDoom,final - startDoom);
		DataManager.instance.curPlayerData.doom = final;
		if(final == 100)
		{
			// doom has come
			//doomEvent.encounter();
		}
	}
	public void CombatMode()
	{
		goldPanel.hide(0.5f);
//		doomPanel.hide(0.5f);
	}
	public void AdventureMode()
	{
		goldPanel.show(0.5f);
	//	doomPanel.show(0.5f);
	}

	public CharacterUI lastUI;
	
}
