using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
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
		//transform.Find("cover").gameObject.SetActive(false);
		transform.Find("cover").GetComponent<Image>().DOFade(0,0.5f);
	}
	public void playEvent()
	{

	}
	public void encounter()
	{
		if(dungeonEvent&&dungeonEvent.type!= DungeonEventType.None)
		{
			dungeonEvent.encounter();
			DungeonOptionSelector.instance.showPanel(dungeonEvent);
		}
		else
		{
			//DungeonManager.instance.showFloor();
		}
		//dungeonEvent
	}
}

