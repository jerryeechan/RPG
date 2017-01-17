using UnityEngine;
namespace com.jerrch.rpg
{
public class InfoTab :AnimatableCanvas{

	public InfoTabType type;
	public GameMode gameMode;
	IDisplayable[] displayables;
	override protected void Awake()
	{
		displayables = GetComponentsInChildren<IDisplayable>(true);
		base.Awake();
	}
	override protected void Start()
	{
		InfoManager.instance.currentTab = this;
		base.Start();
	}
	public override void show(OnCompleteDelegate completeEvent=null)
	{	
		GameManager.instance.gamemode = gameMode;
		base.show();
		//gameObject.SendMessage("Show",SendMessageOptions.DontRequireReceiver);
		if(displayables==null)
			return;
		foreach(var d in displayables)
		{
			d.Show();
		}
		
	}
	public override void hide(OnCompleteDelegate completeEvent)
	{
		base.hide(completeEvent);
		if(displayables==null)
			return;
		//gameObject.SendMessage("Hide",SendMessageOptions.DontRequireReceiver);
		foreach(var d in displayables)
		{
			d.Hide();
		}
	}
}
}