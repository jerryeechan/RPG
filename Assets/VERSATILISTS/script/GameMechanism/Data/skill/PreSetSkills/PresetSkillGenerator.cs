using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PresetSkillGenerator : MonoBehaviour {

	public GameObject[] presetSkills;
	Dictionary<string,GameObject> skillDic;
	public static PresetSkillGenerator instance;
	// Use this for initialization
	void Awake () {
		if(instance!=null)
		{
			throw new UnityException("double singleton");
		}
		instance = this;
		skillDic = new Dictionary<string, GameObject>();
		for(int i=0;i<presetSkills.Length;i++)
		{
			skillDic.Add(presetSkills[i].name,presetSkills[i]);
		}
	}
	public Skill GetSKill(string skillName)
	{
		GameObject skillPrefab;
		if(skillDic.TryGetValue(skillName,out skillPrefab))
		{
			GameObject newSkill = (GameObject)Instantiate(skillPrefab);
			return newSkill.GetComponent<Skill>();
		}
		else
			throw new UnityException("skill name not found");
	}
	// Update is called once per frame
	void Update () {
	
	}
}
