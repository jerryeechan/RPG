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
		describe(descriptionText);
		GameManager.instance.keymode = KeyMode.DungeonSelect;
	}
	// Use this for initialization
	public virtual void confirm()
	{
		GameManager.instance.LockMode();
		DungeonOptionSelector.instance.hidePanel();
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
