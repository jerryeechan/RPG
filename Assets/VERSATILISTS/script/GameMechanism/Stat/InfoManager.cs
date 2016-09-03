using UnityEngine;
using System.Collections;

public class InfoManager : Singleton<InfoManager> {
	public bool isOpen = false;

	void Awake()
	{
		isOpen = true;
	}
	void Start()
	{
		if(isOpen)
		{
			Show();
		}
	}
	public void Show()
	{
		gameObject.SetActive(true);
		DungeonPlayerStateUI.instance.descriptionText.text = "";
		isOpen = true;
	}

	public void Hide(){
		gameObject.SetActive(false);
		isOpen = false;
	}
}
