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
#if UNITY_EDITOR

#else
	void Start()
	{
	
		BackToMainMenu();
	}
#endif
	//bool isStart = false;

	
	public void seeMovie()
	{
		//isStart = true;
		loadSceneName = "_2_movie";
		PauseMenuManager.instance.Transition(sceneTransition);
	}
	public void buildTeam()
	{
		loadSceneName = "_3_selectch";
		PauseMenuManager.instance.Transition(sceneTransition);
	}
	
	public void adventureStart()
	{	
		loadSceneName = "_4_battle";
		PauseMenuManager.instance.Transition(sceneTransition);
	}
	public void BackToMainMenu()
	{
		print("back to menu");
		loadSceneName = "_1_menu";
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
		
		if(scene.name[0]=='_')
		{
			SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
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
