using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTokenGenerator : Singleton<TextTokenGenerator> {

	[SerializeField]
	string[] boyNameTemplate;
	string[] girlNameTemplate;
	public string boyName(){
		int r = Random.Range(0,boyNameTemplate.Length);
		return boyNameTemplate[r];
	}
	public string girlName(){
		int r = Random.Range(0,girlNameTemplate.Length);
		return girlNameTemplate[r];
	}
}
