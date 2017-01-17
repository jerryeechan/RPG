using UnityEngine;
using System.Collections;
[System.SerializableAttribute]
public class AdventureEvent:BaseEvent {
	public AdventureEventType type;

	public  AdventureDialogueData dialogue;
	public AdventureOption[] options;
	public AdventureOption[] detailOptions;

	public bool hasOption{
		get{
			return options.Length>0||detailOptions.Length>0;
		}
	}

	// Use this for initialization
	protected override void OnValidate()
	{
		base.OnValidate();
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
		

	}
	
	// Use this for initialization
}


public enum AdventureEventType{Dialogue,ChooseReward,OptionReward}