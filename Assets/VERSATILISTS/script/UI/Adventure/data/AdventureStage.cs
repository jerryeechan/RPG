using UnityEngine;
using System.Collections;

public class AdventureStage : MonoBehaviour {

	public string stageName;
	AdventureScene[] normalScenes;

	//narrative related
	AdventureScene[] storyScenes;
	void Awake()
	{
		normalScenes = transform.Find("NormalScenes").GetComponentsInChildren<AdventureScene>();
		storyScenes = transform.Find("StoryScenes").GetComponentsInChildren<AdventureScene>();
	}
	int stageEventCount = 0;
	public AdventureScene getScene()
	{
		stageEventCount++;
		if(stageEventCount == 0)
		{
			return getSpecialScene();
		}
		int r = Random.Range(0,normalScenes.Length);
		return normalScenes[r];
	}

	int storySceneIndex = 0;
	AdventureScene getSpecialScene()
	{
		return storyScenes[storySceneIndex++];
	} 

	public bool shouldMoveToNextStage
	{
		get{
			return storySceneIndex==storyScenes.Length;
		}
	}
}
