using UnityEngine;
using System.Collections.Generic;

public class Action : MonoBehaviour {
	Skill[] skills;
	public string description;
	
	[HideInInspector]
	public ActionData actionData;
	public Character caster;
	void Awake()
	{
		//foreach (Skill skill in  GetComponentsInChildren<Skill>())
		skills = GetComponentsInChildren<Skill>();
	}
	public void move()
	{
		skills[0].init(caster);
		skills[0].Use();
	}
}
