using UnityEngine;
using System.Collections;

public class DungeonExitEvent : DungeonEvent {
	override public void confirm(){
		base.confirm();
		
		UIManager.instance.ShowCover(exitEvent);
	}
	override public void cancel()
	{
		base.cancel();
		DungeonManager.instance.dungeonEventComplete();
	}
	public void exitEvent()
	{
		Debug.Log("exit");
		int level = DungeonManager.instance.genMap();
		DungeonPlayerStateUI.instance.popUpText("Floor: "+level);
		DungeonManager.instance.dungeonEventComplete();
		UIManager.instance.HideCover();	
	}

}