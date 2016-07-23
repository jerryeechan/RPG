using UnityEngine;
using System.Collections;

public class CharacterButton : MonoBehaviour {
	
	//Character ch;
	public Transform skillPanel;
	bool isLocked =true;
	void Awake()
	{
		//ch = GetComponentInParent<Character>();
		//Lock();
		//
	}
	
	public void OnClick()
	{
		if(!isLocked)
		skillPanel.gameObject.SetActive(true);
	}
	public void Lock()
	{
		isLocked = true;
		
		skillPanel.gameObject.SetActive(false);
	}
	public void Unlock()
	{
		isLocked = false;
	}
	public void ShowSKillButtonPanel()
	{
		
	} 
}
