using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerTeam : MonoBehaviour {

	public List<Character> members;
	// Use this for initialization
	public static PlayerTeam instance;
	void Awake()
	{
		if(instance!=null)
		Destroy(gameObject);
		else
		{
			instance = this;
			members = new List<Character>();
			DontDestroyOnLoad(gameObject);
		}
	}
	
}
