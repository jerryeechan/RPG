using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRollerAll : Singleton<DiceRollerAll> {

	[SerializeField]
	Sprite enemySprite;
	[SerializeField]
	DiceSlot[] dices;
	DiceRollResultDelegate diceRollDelegate;
	int[] rollNum =  {15,21,26,30};
	int enemyPos;
	public DiceSlot[] getDices{
		get{
			return dices;
		}
	}
	void Start()
	{
		foreach(var dice in dices)
			DiceFactory.instance.MakeDefaultDice(dice);
	}
	public void Roll(DiceRollResultDelegate d)
	{
		 print("Roll");
		 diceRollDelegate+=d;
		 currentDice = 0;
		 enemyPos = Random.Range(0,4);
		 dices[enemyPos].willHaveSpecialResult(enemySprite);
		 for(int i=0;i<4;i++)
		 {
			 dices[i].Roll(rollNum[i],rollDoneCallBack);
		 }
	}
	SkillDiceType[] result = {0,0,0,0};
	int currentDice = 0;
	public void rollDoneCallBack(SkillDiceType value)
	{
		result[currentDice] = value; 
		currentDice++;
		if(currentDice==4)
		{
			diceRollDelegate(result,enemyPos);
		}
	}

	
}

public delegate void DiceTypeResultDelegate(SkillDiceType type);
public delegate void DiceRollResultDelegate(SkillDiceType[] values,int monsterIndex);