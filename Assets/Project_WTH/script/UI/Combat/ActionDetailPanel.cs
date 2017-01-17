using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ActionDetailPanel : MonoBehaviour {

	public CompositeText titleText;
	public CompositeText detailText;

	void Awake()
	{
		//transfom.find...
	}
	
	public void setText(string titleStr,string detailStr)
	{
		titleText.text = titleStr;
		detailText.text = detailStr;
	}
}
