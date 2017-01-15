using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBtnPanel : MonoBehaviour {

	HandButton[] switchBtns;
	void Awake()
	{
		switchBtns = GetComponentsInChildren<HandButton>();
	}

	public void switchBtnsTouched(int index)
	{
		for(int i=0;i<switchBtns.Length;i++)
		{
			if(i == index)
			{
				switchBtns[i].maskEnable = false;
			}
			else
			{
				switchBtns[i].maskEnable = true;
			}
		}
		switchDelegate.switchBtnTouched(index);
	}
	public SwitchBtnTouchDelegate switchDelegate;
}

public interface SwitchBtnTouchDelegate
{
	void switchBtnTouched(int index);
}