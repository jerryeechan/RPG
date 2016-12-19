using UnityEngine;
using System.Collections;
[System.SerializableAttribute]
public class AdventureEvent:MonoBehaviour {
	public AdventureEventType type;
	public AdventureEvent nextEvent;
	public bool triggerNextEvent = false;

	public  AdventureDialogueData dialogue;
	public AdventureOption[] options;
	public AdventureOption[] detailOptions;

	public bool hasOption{
		get{
			return options.Length>0||detailOptions.Length>0;
		}
	}

	// Use this for initialization
	void OnValidate()
	{
		for(int i=0;i<options.Length;i++)
		{
			options[i].index = i;
			options[i].parentEvent = this;
		}
		for(int i=0;i<detailOptions.Length;i++)
		{
			detailOptions[i].index = i;
			detailOptions[i].parentEvent = this;
		}
		if(nextEvent==null)
		{
			triggerNextEvent = false;
		}

	}
	
	// Use this for initialization
}


public enum AdventureEventType{Dialogue,ChooseReward,OptionReward}