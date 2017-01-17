using UnityEngine;
using System.Collections;

public class AdventureScene : MonoBehaviour {

	//background sprite
	public string scenceName;
	public AdventureEvent[] candidateEvents;
	public int weight = 1;
	
	public BaseEvent getEvent()
	{
		int r = Random.Range(0,candidateEvents.Length);
		return candidateEvents[r];
	}
	public void reset()
	{
		weight = 1;
	}
	public void addWeight()
	{
		weight++;
	}
}
