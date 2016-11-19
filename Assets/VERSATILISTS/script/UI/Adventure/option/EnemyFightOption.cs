using UnityEngine;
using System.Collections;
using com.jerrch.rpg;
public class EnemyFightOption: AdventureOption {

	public EnemySet specificEnemySet;
	void Awake()
	{
		
	}
	void OnValidate(){
		init();
	}
	void init()
	{
		text = "Fight";
	}
	override public void choose()
	{
		print("Fight");
		UIManager.instance.ShowCover(chooseEvent);
		success();
	}
	
	
	public void chooseEvent()
	{
		if(specificEnemySet==null)
			specificEnemySet = MonsterDataEditor.instance.getMonsterSet();

		RandomBattleRound.instance.StartBattle(specificEnemySet);
		UIManager.instance.HideCover(); 
	}

	
}
