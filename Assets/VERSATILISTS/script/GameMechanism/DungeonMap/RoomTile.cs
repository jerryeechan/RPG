using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
public class RoomTile : MonoBehaviour {

	public int[] side;
	public int x;
	public int y;
	bool hasReveal = false;
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
		if(!hasReveal)
		{
			transform.Find("cover").GetComponent<Image>().DOFade(0,0.5f);
			foreach(var chUI in DungeonPlayerStateUI.instance.chUIs)
				chUI.getExp(1);
			hasReveal = true;
		}
			
		
	}
	public void playEvent()
	{

	}
	public void encounter()
	{
		if(dungeonEvent&&dungeonEvent.type!= DungeonEventType.None)
		{
			dungeonEvent.encounter();
			
		}
		else
		{
			//DungeonManager.instance.showFloor();
		}
		//dungeonEvent
	}
}

