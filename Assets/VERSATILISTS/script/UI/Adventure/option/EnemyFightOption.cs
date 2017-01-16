using UnityEngine;
using System.Collections;
using com.jerrch.rpg;
public class EnemyFightOption: AdventureOption {

	public string enemySetID;
	void Awake()
	{
		
	}
	void OnValidate(){
		init();
	}
	void init()
	{
		
	}
	override public void choose()
	{
		AdventureManager.instance.optionSuccess(index);
		CursorManager.instance.NormalMode();
		//success();
		this.myInvoke(1,
		()=>{
			PauseMenuManager.instance.Transition(()=>{
				chooseEvent();
			});
		});	
	}
	
	
	public void chooseEvent()
	{
		EnemySet enemySet;
		print(enemySetID);
		enemySet = MonsterDataEditor.instance.getMonsterSet(enemySetID);
		//RandomBattleRound.instance.StartBattle(enemySet);
		TurnBattleManager.instance.StartBattle(enemySet);
	}
}




