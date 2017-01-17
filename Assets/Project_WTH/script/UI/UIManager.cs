using UnityEngine;
using System.Collections.Generic;
namespace com.jerrch.rpg
{
public class UIManager : Singleton<UIManager>{
	public Canvas canvas;
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
		//cover = getPanel("cover"); 
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
		if(panelsDict.ContainsKey(name))
		{
			return panelsDict[name]; 	
		}
		else
		{
			Debug.LogError(name+"not exist");
			return null;
		}
		
	}

	public void setCursor()
	{
		//Cursor.
		//Cursor.SetCursor;
	}
	//public delegate void ShowCompleteDelegate();
	//ShowCompleteDelegate onComplete;
	
	
}
}