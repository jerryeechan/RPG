using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InitLoader : MonoBehaviour {

	// Use this for initialization
	List<string> additiveScenes = new List<string>();
	void loadInitScenes()
	{
		additiveScenes.Add("character_skill_build");
		additiveScenes.Add("setting");
		
		foreach(var scene in additiveScenes)
		{
			SceneManager.LoadScene(scene,LoadSceneMode.Additive);
		}
		//additiveScenes.Add("_1_menu");
	}

	void Awake()
	{
		//SceneManager.activeSceneChanged+=ActiveSceneChanged;
		loadInitScenes();
	}
}
