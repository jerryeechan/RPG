using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoomTile : MonoBehaviour {

	public int[] side;
	public int x;
	public int y;
	public DungeonEvent dungeonEvent;
	public void setEvent(DungeonEvent de)
	{
		dungeonEvent = de;
		transform.Find("image").GetComponent<Image>().sprite = de.icon;
	}
	public void removeEvent()
	{
		dungeonEvent = null;
		transform.Find("image").GetComponent<Image>().enabled = false;
	}
	public void reveal()
	{
		transform.Find("cover").gameObject.SetActive(false);
	}
	public void playEvent()
	{

	}
	public void encounter()
	{
		if(dungeonEvent&&dungeonEvent.type!= DungeonEventType.None)
		{
			DungeonManager.instance.descriptionText.text = dungeonEvent.descriptionText;
			DungeonOptionSelector.instance.showPanel(dungeonEvent);
			GameManager.instance.keymode = KeyMode.DungeonSelect;
			
		}
		//dungeonEvent
	}
}

