using UnityEngine;
using System.Collections;

public class DungeonTrapEvent : DungeonEvent {
	void Awake()
	{
		confirmText = "";
		cancelText = "Accept";
	}
	override public void confirm(){
		base.confirm();
		
	}
	override public void cancel()
	{
		base.cancel();
	}
	public SkillEffect effect;
	public bool isOnce = true;
	public void trapEventTrigger()
	{
		Debug.Log("exit");
		foreach(var ch in GameManager.instance.chs)
		{
			effect.ApplyOn(ch.equipStat);
			ch.updateDungeonUI();
		}
	}	
}
