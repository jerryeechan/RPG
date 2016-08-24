using UnityEngine;
using System.Collections;

public class DungeonPlayerStateUI : Singleton<DungeonPlayerStateUI> {

	public CompositeText goldText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void getGold(int g)
	{
		int startGold = DataManager.instance.curPlayerData.gold;
		goldText.animateValue(startGold,g); 
		DataManager.instance.curPlayerData.gold+=g;
	}
}
