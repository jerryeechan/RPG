using UnityEngine;
using System.Collections.Generic;
using com.jerry.rpg;
public class ActionManager : Singleton<ActionManager> {

	public List<Action> actionList;
	Dictionary<string,Action> actionPool;
	//Dictionary<string,SkillData> skillDataPool;
	void Awake()
	{
		actionPool = new Dictionary<string,Action>();
		foreach(Action action in actionList)
		{
			actionPool.Add(action.name,action);
		}
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
