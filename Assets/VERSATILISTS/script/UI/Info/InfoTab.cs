using UnityEngine;
using System.Collections;

public class InfoTab : AnimatableCanvas {

	public InfoTabType type;
	public GameMode gameMode;
	IDisplayable[] displayables;
	protected override void Awake()
	{
		displayables = GetComponentsInChildren<IDisplayable>(true);
		print(gameObject.name);
		print(displayables);
		base.Awake();
	}
	void Start()
	{
		InfoManager.instance.currentTab = this;
	}

	public override void show()
	{
		base.show();
		GameManager.instance.gamemode = gameMode;
		//gameObject.SendMessage("Show",SendMessageOptions.DontRequireReceiver);
		if(displayables==null)
			return;
		foreach(var d in displayables)
		{
			print("show:"+d);
			d.Show();
		}
		
	}
	public override void hide()
	{
		base.hide();
		if(displayables==null)
			return;
		//gameObject.SendMessage("Hide",SendMessageOptions.DontRequireReceiver);
		foreach(var d in displayables)
		{
			d.Hide();
		}
	}
}
