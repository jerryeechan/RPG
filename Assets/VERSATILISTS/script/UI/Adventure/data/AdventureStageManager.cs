using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureStageManager:Singleton<AdventureStageManager> {

	public AdventureStage[] stages;
	int currentStageIndex=0;
	public AdventureStage currentStage{
		get{return stages[currentStageIndex];}
	}
	
	void GotoNextStage()
	{
		currentStageIndex++;
	}

}
