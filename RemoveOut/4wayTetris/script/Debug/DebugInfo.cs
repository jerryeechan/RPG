using UnityEngine;
using System.Collections;

public class DebugInfo : Singleton<DebugInfo> {

	public bool isDebugEnabled;
	
	public GameObject textmeshPrefab;
	TextMesh[,] debugGrid;
	Transform debugInfoTransform;
	
	public void enable()
	{
		isDebugEnabled = true;
	}
	public void disable()
	{
		isDebugEnabled = false;
	}
	void Awake()
	{
		
	}
	
	public void initDebugInfo(int gridW){
		
		Debug.Log("show grid debug text");
		//instantiate the text of is block on grid
		debugInfoTransform = GameObject.Find("DebugInfo").transform;
		debugGrid = new TextMesh[gridW,gridW];
		//i horizontal
		//j vertical
		for(int i=0;i<gridW;i++)
		{
			for(int j=0;j<gridW;j++)
			{
				GameObject gobj = Instantiate(textmeshPrefab,new Vector3(i,j),Quaternion.identity) as GameObject;
				gobj.transform.SetParent(debugInfoTransform);
				debugGrid[i,j] = gobj.GetComponent<TextMesh>();
			}
		}
	}
	public void setGrid(int x,int y,bool hasBlock)
	{
		if(hasBlock)
			debugGrid[x,y].text = "1";
		else
			debugGrid[x,y].text = "0";
	}
}
