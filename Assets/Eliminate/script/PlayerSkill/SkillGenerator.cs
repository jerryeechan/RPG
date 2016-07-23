using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SkillGenerator : MonoBehaviour {

	// Use this for initialization
	List<GameObject> skillPrefabs;
	
	List<GameObject>skillStack;
	GameObject GetSkill()
	{
		return (GameObject)Instantiate(skillPrefabs[Random.Range(0,skillPrefabs.Count)]);
	}
	
	public void OneRoundGenSkill()
	{
		int skillNumPerRound = 3;
		for(int i=0;i<skillNumPerRound;i++)
		{
			skillStack.Add(GetSkill());
		}
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
