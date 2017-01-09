using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WTHSceneManager : Singleton<WTHSceneManager> {

	void Awake()
	{
		SceneManager.sceneLoaded+=Loading;
		//SceneManager.activeSceneChanged+=ActiveSceneChanged;
	}
	public void startNewGame()
	{
		loadSceneName = "selectch";
		PauseMenuManager.instance.Transition(sceneTransition);
		//SceneManager.LoadSceneAsync("selectch",LoadSceneMode.Additive);
		
		//LoadScene("selectch");
	}
	public void adventureStart()
	{
		//SceneManager.LoadSceneAsync("battle",LoadSceneMode.Additive);
		//LoadScene("battle");
		loadSceneName = "battle";
		PauseMenuManager.instance.Transition(sceneTransition);
	}
	public void BackToMainMenu()
	{
		print("back to menu");
		loadSceneName = "menu";
		PauseMenuManager.instance.Transition(sceneTransition);
	}

	OnCompleteDelegate loadComplete;
	public void sceneTransition(OnCompleteDelegate d)
	{
		loadComplete = d;
		StartCoroutine("LoadScene");
	}
	
	string loadSceneName;
	IEnumerator LoadScene() {
		
        AsyncOperation async = SceneManager.LoadSceneAsync(loadSceneName,LoadSceneMode.Additive);
        yield return async;
        Loaded();
    }
	
	
	Scene newLoadedScene;
	//
	void Loading(Scene scene,LoadSceneMode mode)
	{
		newLoadedScene = scene;
	}
	void Loaded()
	{
		print("scene loaded");
		var scene = newLoadedScene;
		SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
		if(scene.name=="selectch"||scene.name == "battle"||scene.name=="menu")
		{
			if(SceneManager.SetActiveScene(scene))
			{
				print("Succ");
			}
			
			print("active scene"+scene.name);
		}
		loadComplete();
		//if(scene.name == "battle")
		//{
			
		//}
	}
}
