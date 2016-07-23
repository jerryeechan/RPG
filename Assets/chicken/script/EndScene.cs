using UnityEngine;
using System.Collections;

public class EndScene : MonoBehaviour {

	// Use this for initialization
	public GameObject winScene;
	public GameObject failScene;
	void Awake () {
		if(PlayerPrefs.GetInt("win")==1)
		{
			//win
			failScene.SetActive(false);
		}
		else
		{
			//fail
			winScene.SetActive(false);
		}
		Invoke("GoMenu",1);
	}
	
	// Update is called once per frame
	void GoMenu () {
		Application.LoadLevel("title");
	}
}
