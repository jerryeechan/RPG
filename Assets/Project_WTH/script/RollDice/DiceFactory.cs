using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DiceFactory : Singleton<DiceFactory> {

	[SerializeField]
	Sprite[] diceSprites;
	
	public DiceTemplate defaultDiceTemplate;
	public void MakeDefaultDice(DiceSlot dice)
	{
		dice.diceSprites = new Sprite[6];
		for(int i=0;i<6;i++)
		{
			dice.diceSprites[i] = diceSprites[(int)defaultDiceTemplate.faceTypes[i]];
		}
		dice.faceTypes = defaultDiceTemplate.faceTypes;
	}
	/*
	public void MakeDice(DiceSlot dice,DiceTemplate template)
	{
		dice.faceTypes = defaultDiceTemplate.faceTypes;
	}*/

	public bool isInSet(SkillDiceType targetType,SkillDiceType setType)
	{
		return (targetType & setType)!=0;
	}
}
