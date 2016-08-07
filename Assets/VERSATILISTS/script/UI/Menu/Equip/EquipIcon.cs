using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipIcon : MonoBehaviour {

	
	void equip(EquipGraphicAsset asset)
	{		
		//bindData.
		Character currentPlayer = RandomBattleRound.instance.currentPlayer;
		currentPlayer.chRenderer.wearEquip(asset);
	}
	public void Click()
	{

	}
}
