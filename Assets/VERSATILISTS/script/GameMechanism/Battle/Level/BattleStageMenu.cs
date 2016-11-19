using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BattleStageMenu : MonoBehaviour {
	
	
	public static BattleStageMenu instance;
	void Awake()
	{
		if(instance!=null&&instance!=this)
			Destroy(gameObject);
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		
	}
	public void StartStage(int stageNum)//button menu buttons press 
	{	
		//stage = genStage(stageNum);	
		PlayerPrefs.SetInt("stage",stageNum);
//		Application.LoadLevel("battle");
	}
}
