using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class StartMenuManager : MonoBehaviour {
	public void settingBtnTouched()
	{

	}
	public void startGameBtnTouched()
	{
		WTHSceneManager.instance.startNewGame();
	}
	public void fakeGameBtnTouched()
	{

	}
	
}
