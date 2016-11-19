using UnityEngine;
using System.Collections;

public class AdventureOption : MonoBehaviour {
	public string text;
	
	public int index;
	
	public string successStr;
	public string failStr;
	[HideInInspector]
	public AdventureEvent parentEvent;
	
	public virtual void choose()
	{
		
	}
	public void success()
	{
		AdventureManager.instance.optionSuccess(index);
		CursorManager.instance.NormalMode();
	}
	public void fail()
	{
		AdventureManager.instance.optionFail(index);
	}
}

