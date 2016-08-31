using UnityEngine;
using System.Collections.Generic;

public class UIManager : Singleton<UIManager>{
	Canvas canvas;
	public AnimatableCanvas[] panels;
	Dictionary<string,AnimatableCanvas> panelsDict;
	void Awake()
	{
		canvas = GetComponent<Canvas>();
		canvas.pixelPerfect = true;
		
		//canvas.scaleFactor = 1;
		//if(Screen.width>=960)
		//canvas.scaleFactor = 2;
		panelsDict = new Dictionary<string,AnimatableCanvas>();
		foreach(var panel in panels)
		{
			panelsDict.Add(panel.name,panel);
		}	
		cover = getPanel("cover"); 
	}
	public void setCanvasScale(float s)
	{
		canvas.scaleFactor = s;
	}
	void Start()
	{
		//cover.hide(1f);
	}

	AnimatableCanvas cover;
	public AnimatableCanvas getPanel(string name)
	{
		return panelsDict[name]; 
	}

	public void setCursor()
	{
		//Cursor.
		//Cursor.SetCursor;
	}
	public void ShowCover(OnCompleteDelegate d=null)
	{
		cover.show(1,d);
		//onComplete +=d;
		//onComplete();
	}
	//public delegate void ShowCompleteDelegate();
	//ShowCompleteDelegate onComplete;
	
	public void HideCover(OnCompleteDelegate d=null)
	{
		cover.hide(1,d);
	}
}
