using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MenuButton : MonoBehaviour {

	public Image selectHighlight;
	public MenuManager.Panel panel;
	
	public void Select()
	{
		selectHighlight.enabled = true;
	}
	public void deSelect()
	{
		selectHighlight.enabled = false;
	}
}
