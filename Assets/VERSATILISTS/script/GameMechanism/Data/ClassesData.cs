using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassesData : MonoBehaviour {

	public string classID;
	public string[] attackActionCandidateIDs;
	public string[] defenseActionCandidateIDs;
	public string[] specialActionCandidateIDs;

	
	void Awake()
	{
		
	}
	List<string> mergeActionsList()
	{
		List<string> allActionIDs = new List<string>();
		allActionIDs.AddRange(attackActionCandidateIDs);
		allActionIDs.AddRange(defenseActionCandidateIDs);
		allActionIDs.AddRange(specialActionCandidateIDs);
		return allActionIDs;
	}
	const int actionNumPerClass = 3;
	public List<string> getRandomActionIDs()
	{
		var actionIDs = mergeActionsList();
		var randomActions = new List<string>();
		for(int i=0;i<actionNumPerClass;i++)
		{
			int r = Random.Range(0,actionIDs.Count);
			randomActions.Add(actionIDs[r]);
			actionIDs.RemoveAt(r);
		}
		return randomActions; 
	}
}
