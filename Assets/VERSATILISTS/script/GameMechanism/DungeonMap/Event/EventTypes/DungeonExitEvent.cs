using UnityEngine;
using System.Collections;

public class DungeonExitEvent : DungeonEvent {
	override public void confirm(){
		base.confirm();
		UIManager.instance.ShowCover(confirmEvent);
	}
	override public void cancel()
	{
		base.cancel();
	}
	public void confirmEvent()
	{
		Debug.Log("exit");
		DungeonManager.instance.genMap();
		UIManager.instance.HideCover();
		GameManager.instance.DungeonMapMode();
	}

}