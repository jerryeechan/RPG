using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipIcon : MonoBehaviour {

	EquipGraphicData bindData;
	void equip()
	{
		//bindData.
		Character currentPlayer = RandomBattleRound.instance.currentPlayer;
		currentPlayer.chRenderer.changeEquip(bindData);
	}
	
	
	public void Click()
	{

	}
}
