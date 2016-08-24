using UnityEngine;
using System.Collections;

public class DungeonEvent : MonoBehaviour {

	public Sprite icon;
	public DungeonEventType type = DungeonEventType.None;
	public string confirmText;
	public string cancelText; 
	public string descriptionText;
	// Use this for initialization
	public virtual void confirm()
	{
		DungeonOptionSelector.instance.hidePanel();
		GameManager.instance.keymode = KeyMode.Dungeon;
		
	}
	public virtual void cancel()
	{
		DungeonOptionSelector.instance.hidePanel();
		GameManager.instance.keymode = KeyMode.Dungeon;
	}
	
}
