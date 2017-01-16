using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEvent : MonoBehaviour {

	public BaseEvent nextEvent;
	public bool triggerNextEvent = false;
	protected virtual void OnValidate()
	{	
		if(nextEvent)
		{
			triggerNextEvent = true;
		}
		else
		{
			triggerNextEvent = false;
		}
	}
	// Use this for initialization
	
}
