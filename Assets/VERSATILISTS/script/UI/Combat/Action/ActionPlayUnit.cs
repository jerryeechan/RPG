using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.jerrch.rpg;
public class ActionPlayUnit{
	
	Queue<ActionPair> actionPairQueue = new Queue<ActionPair>();
	
	public Queue<ActionPair> actionPairs
	{
		get{
			return actionPairQueue;
		}
	}
	public int maximum = 1;
	public void AddAction(Character ch,Action action)
	{
		
		if(actionPairQueue.Count==maximum)
		{
			actionPairQueue.Dequeue();
		}
		actionPairQueue.Enqueue(new ActionPair(ch,action));

	}
}
public class ActionPair
{
	public ActionPair(Character ch,Action action)
	{
		this.actionTemplate = action;
		this.ch = ch;
	}
	Action actionTemplate;
	Character ch;
	public void PlayAction(OnCompleteDelegate completeFunc)
	{	
		if(!ch.isDead)
		{
			if(ch.battleStat.movable.randomCheck())
			{
				Action action = GameObject.Instantiate(actionTemplate);
				action.caster = ch;
				ch.doActionMove(action.chAnimation);
				action.PlayAction(completeFunc);
			}			
			else{
				//can't move
				Debug.Log("can't move");	
			}
		}
		else
			completeFunc();
	}
}