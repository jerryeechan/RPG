using UnityEngine;
using System.Collections;

public class EnemyEscapeOption : AdventureOption {
	
	public string enemySetID;
	override public void choose()
	{	
		print("逃跑");
		success();
		
	}
	public void cancelEvent(int values)
	{
		if(values > 5)
		{
			//succeed
			Debug.Log("Run away succeed");
			success();
			/*
			this.myInvoke(1,
			()=>{
				PauseMenuManager.instance.Transition(()=>{
					
				});
			});	*/
			
		}
		else
		{
			//fail
			Debug.Log("Run away fail");
			fail();
		}
	}

	public void failEvent()
	{
		EnemySet enemySet;
		print(enemySetID);
		enemySet = MonsterDataEditor.instance.getMonsterSet(enemySetID);
		//RandomBattleRound.instance.StartBattle(enemySet);
		TurnBattleManager.instance.StartBattle(enemySet);
	}
}
