using UnityEngine;
using System.Collections;

public class AdventureScene : MonoBehaviour {

	//background sprite
	public string scenceName;
	public AdventureEvent[] candidateEvents;
	
	public AdventureEvent getEvent()
	{
		int r = Random.Range(0,candidateEvents.Length);
		return candidateEvents[r];
	}
}
