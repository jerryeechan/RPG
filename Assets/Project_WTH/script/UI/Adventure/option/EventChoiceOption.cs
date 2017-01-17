using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceOption : AdventureOption {

	[SerializeField]
	BaseEvent nextEvent;
	
	private void OnValidate()
	{
		//if(nextEvent == )
		nextEvent = GetComponentInChildren<BaseEvent>();
	}

	override public void choose()
	{
		//success();
		chooseEvent();
		
	}
	
	public void chooseEvent()
	{
		AdventureManager.instance.encounterEvent(nextEvent);
	}
}
