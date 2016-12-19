using UnityEngine;
using System.Collections;

public class AdventureStage : MonoBehaviour {

	public string stageName;

	[SerializeField]
	AdventureScene[] normalScenes;

	//narrative related
	[SerializeField]
	AdventureScene[] storyScenes;
	void OnValidate()
	{
		normalScenes = transform.Find("NormalScenes").GetComponentsInChildren<AdventureScene>();
		storyScenes = transform.Find("StoryScenes").GetComponentsInChildren<AdventureScene>();
	}

	[SerializeField]
	int stageEventCount = 0;
	public AdventureScene getScene()
	{
		//TODO: when stage Event count to a certain level, get special scene,
		AdventureScene scene;
		if(stageEventCount == 0)
		{
			stageEventCount++;
			scene = getSpecialScene();
		}
		else
		{
			int r = Random.Range(0,normalScenes.Length);
			scene = normalScenes[r];
		}
		stageEventCount++;
		return scene;
		
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
