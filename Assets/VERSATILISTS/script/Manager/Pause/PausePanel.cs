using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel :SingletonCanvas<PausePanel>{

	public void GiveUpTouched()
	{
		ConfirmPanel.instance.yesDelegate = WTHSceneManager.instance.BackToMainMenu;
		ConfirmPanel.instance.noDelegate = Cancel;
		hide();
		ConfirmPanel.instance.show();
	}
	void Cancel()
	{
		show();
	}
	public void Rest()
	{
		//TODO:save the progress
		WTHSceneManager.instance.BackToMainMenu();
	}
	public void Setting()
	{
		SettingManager.instance.show();
	}
}
