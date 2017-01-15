using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBtnPanel : MonoBehaviour {

	HandButton[] switchBtns;
	void Awake()
	{
		switchBtns = GetComponentsInChildren<HandButton>();
	}

	int _curIndex = 0;
	public int currentIndex{
		get{
			return _curIndex;
		}
	}
	public void setup()
	{
		switchBtnsTouched(_curIndex);
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
		_curIndex = index;
	}
	public SwitchBtnTouchDelegate switchDelegate;
}

public interface SwitchBtnTouchDelegate
{
	void switchBtnTouched(int index);
}