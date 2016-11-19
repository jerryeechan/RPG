using UnityEngine;
using System.Collections;
using DG.Tweening;
public class DungeonEvent : MonoBehaviour {

	public Sprite icon;
	public DungeonEventType type = DungeonEventType.None;
	public string confirmText;
	public string cancelText; 
	public string descriptionText;
	public virtual void encounter()
	{
		DungeonOptionSelector.instance.showPanel(this);
		describe(descriptionText);
		
	}
	// Use this for initialization
	public virtual void confirm()
	{
		GameManager.instance.LockMode();
		DungeonOptionSelector.instance.hidePanel();
		DungeonManager.instance.dungeonEventComplete();
	}
	public virtual void cancel()
	{
		GameManager.instance.LockMode();
		DungeonOptionSelector.instance.hidePanel();
	}
	
	public void describe(string text)
	{
		//
		DungeonPlayerStateUI.instance.popUpText(text);
		//DungeonPlayerStateUI.instance.descriptionText.text = text;
	}
}
