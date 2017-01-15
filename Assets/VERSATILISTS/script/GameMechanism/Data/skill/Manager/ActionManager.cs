using UnityEngine;
using System.Collections.Generic;
using com.jerrch.rpg;
public class ActionManager : Singleton<ActionManager> {

	public static int action_max_num = 3;
	Dictionary<string,Action> actionPool;
	public Action[] actions;
	//Dictionary<string,SkillData> skillDataPool;
	
	void Awake()
	{
		actionPool = new Dictionary<string,Action>();
		actions = GetComponentsInChildren<Action>();		
		foreach(Action action in actions)
		{
			actionPool.Add(action.name,action);
		}
	}

	public List<Action> getActions(List<ActionData> dataList)
	{
		List<Action> actionList = new List<Action>();
		int i = 0;
		foreach(var data in dataList)
		{
			if(actionPool.ContainsKey(data.id))
			{
				var action = Instantiate(actionPool[data.id]);
				action.level = data.level;
				actionList.Add(action);
				i++;
			}
			else{
				Debug.LogError(data.id+":Character's action does not exist anymore");
			}
		}
		for(;i<action_max_num;i++)
		{
			actionList.Add(null);
		}
		return actionList;
	}

	public Action GenAction(string name)
	{
		if(actionPool[name])
		{
			Action action = Instantiate(actionPool[name]);
			return action;
		}	
		else
		{
			Debug.LogError("No action:"+name);
			return null;
		}
		
	}
	public Action getAction(string name)
	{
		if(actionPool.ContainsKey(name))
		{
			Action action = actionPool[name];
			return action;
		}
		else
		{
			Debug.LogError("No action:"+name);
			return null;
		}
	}

	public static Color getDiceTypeColor(ActionDiceType type)
	{
		switch(type)
		{
			case ActionDiceType.Attack:
				return new Color32(188,51,51,255);
			case ActionDiceType.Defense:
				return new Color32(73,149,212,255);
			case ActionDiceType.Special:
				return new Color32(255,247,155,255);
			default:
				return new Color32(188,51,51,255);
		}
	}
}
