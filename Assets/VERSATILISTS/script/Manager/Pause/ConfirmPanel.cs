using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmPanel : SingletonCanvas<ConfirmPanel> {

	// Use this for initialization
	[HideInInspector]
	public OnCompleteDelegate yesDelegate = null;
	[HideInInspector]
	public OnCompleteDelegate noDelegate = null;
	public void YesTouched() {
		print("yesTouched");
		hide(yesDelegate);
		yesDelegate = null;
	}
	
	// Update is called once per frame
	public void NoTouched() {
		hide(noDelegate);
		noDelegate = null;
	}
}
