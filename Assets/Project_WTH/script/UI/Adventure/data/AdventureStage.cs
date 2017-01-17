using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AdventureStage : MonoBehaviour {

	public string stageName;

	[SerializeField]
	AdventureScene[] normalScenes;

	//narrative related
	[SerializeField]
	AdventureScene[] storyScenes;

	AdventureScene[] fakeScenes;
	void OnValidate()
	{
		
		
		normalScenes = transform.Find("NormalScenes").GetComponentsInChildren<AdventureScene>(true);
		foreach(var scen in normalScenes)
		{
			scen.gameObject.SetActive(false);
		}
		foreach(var scene in storyScenes)
		{
			scene.gameObject.SetActive(false);
		}
		storyScenes = transform.Find("StoryScenes").GetComponentsInChildren<AdventureScene>(true);
	}
	void Awake()
	{
		fakeScenes = transform.Find("FakeScenes").GetComponentsInChildren<AdventureScene>(true);
	}

	[SerializeField]
	int stageEventCount = 0;
	public bool isFake = false;
	public AdventureScene getScene()
	{
		//TODO: when stage Event count to a certain level, get special scene,
		if(isFake)
		{
			var scene = getSpecialScene();
			return scene;
		}
		else
		{
			AdventureScene scene;
			//AdventureScene scene;
			if(stageEventCount == 0)
			{
				stageEventCount++;
				print("special");
				scene = getSpecialScene();
			}
			else
			{
				var randomList = new List<int>();
				for(int i=0;i<normalScenes.Length;i++)
				{
					var sc = normalScenes[i];
					for(int j=0;j<sc.weight;j++)
					{
						randomList.Add(i);
					}
				}
				
				int r = Random.Range(0,randomList.Count);
				//normalScenes.Length
				
				//int r = 0;//Fake
				scene = normalScenes[randomList[r]];
			}

			scene.reset();
			foreach(var sc in normalScenes)
			{
				if(sc!=scene)
				sc.addWeight();
			}
			stageEventCount++;
			return scene;
		}
		
		
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
