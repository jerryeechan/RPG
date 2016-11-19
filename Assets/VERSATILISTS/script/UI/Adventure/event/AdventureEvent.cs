using UnityEngine;
using System.Collections;
[System.SerializableAttribute]
public class AdventureEvent:MonoBehaviour {
	public AdventureEventType type;
	public AdventureEvent  nextEvent;
	public bool triggerNextEvent = false;

	public  AdventureDialogueData dialogue;
	public AdventureOption[] options;
	public AdventureOption[] detailOptions;

	// Use this for initialization
	void Awake()
	{
		int i=0;
		foreach(var option in options)
		{
			option.index = i;
			option.parentEvent = this;
			i++;
		}

		i=0;
		foreach(var detailOption in detailOptions)
		{
			detailOption.index = i;
			detailOption.parentEvent = this;
			i++;
		}

	}
	
	// Use this for initialization
}


public enum AdventureEventType{Dialogue,Reward}