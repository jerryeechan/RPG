using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBtnPanel : MonoBehaviour {

	HandButton[] switchBtns;
	void Awake()
	{
		switchBtns = GetComponentsInChildren<HandButton>();
		if(delegateObj)
			switchDelegate = delegateObj.GetComponent<ISwitchBtnTouchDelegate>();
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
		if(switchDelegate!=null)
		{
			switchDelegate.switchBtnTouched(index);
		}
		else{
			Debug.LogWarning("No switch Btn delegate");
		}
		
		_curIndex = index;
	}
	[SerializeField]
	GameObject delegateObj;
	public ISwitchBtnTouchDelegate switchDelegate;
}

public interface ISwitchBtnTouchDelegate
{
	void switchBtnTouched(int index);
}

