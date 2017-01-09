using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.jerrch.rpg;
public class GameStateManager : MonoBehaviour {

	public void saveBtnTouched()
	{
		DataManager.instance.Save();
	}
	public void loadBtnTouched()
	{
		DataManager.instance.Load();
	}
	public void resetBtnTouched()
	{
		DataManager.instance.Reset();
	}
}
