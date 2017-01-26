using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class DiceSelectionDisplayer:MonoBehaviour{

	[SerializeField]
	Image indicator;
	
	DiceSlot[] dices;
	void Awake()
	{
		dices = GetComponentsInChildren<DiceSlot>();
		for(int i=0;i<4;i++)
		{
			dices[i].index = i;
		}
	}
	public DiceSlot currentSlot{
		get{
			return _currentSlot;
		}
	}
	DiceSlot _currentSlot;
	
	Queue<DiceSlot> diceQueue;

	//prepare
	public void reset()
	{
		indicator.enabled = false;
		indicator.rectTransform.anchoredPosition = dices[0].GetComponent<RectTransform>().anchoredPosition;


		foreach(var dice in dices)
		{
			dice.clear();
		}
	}

	public void rollDone()
	{
		indicator.enabled = true;
		selectDice(0);
	}
	//Selecting
	public void selectDice(int index)
	{
		if(dices[index].isPlayerDice)
		{
			indicator.rectTransform.DOMove(dices[index].transform.position,0.2f);
			selectedDice = dices[index].skillSlot;
			currentSlotIndex = index;
		}
	}
	int currentSlotIndex;
	public void selectNextDice()
	{
		var nextIndex = currentSlotIndex+1;
		if(nextIndex>3)
		{
			nextIndex = 3;
		}
		
		if(!dices[nextIndex].isPlayerDice)
		{
			if(nextIndex == 3)
			{
				nextIndex = 2;
			}
			else
			{
				nextIndex++;
			}
		}
		
		selectDice(nextIndex);
		
	}

	public DiceSkillSlot selectedDice;



	//Playing
	public DiceSkillSlot getSkillSlot(int index){
		
		return dices[index].skillSlot; 
	}
	public void PrepareToPlay()
	{
		diceQueue = new Queue<DiceSlot>();

		foreach(var dice in dices)
		{
			diceQueue.Enqueue(dice);
		}
	}
	public DiceSlot playNextDice()
	{
		if(diceQueue.Count==0)
			return null;
		else
		{
			_currentSlot = diceQueue.Dequeue();
			indicator.rectTransform.DOMove(_currentSlot.transform.position,0.2f);
			return _currentSlot;
		}		
	}

	public void end()
	{
		indicator.enabled = false;
	}
}
