using UnityEngine;
using System.Collections.Generic;
using com.jerry.rpg;
public class ActionManager : Singleton<ActionManager> {

	
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

	public List<Action> getActions(List<string> ids)
	{
		List<Action> actionList = new List<Action>();
		int i = 0;
		foreach(var id in ids)
		{
			if(actionPool.ContainsKey(id))
			{
				actionList.Add(actionPool[id]);
				i++;
			}
			else{
				Debug.LogError("Character's action does not exist anymore");
			}
		}
		for(;i<4;i++)
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
		if(actionPool[name])
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
}
