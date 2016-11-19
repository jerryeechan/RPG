using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class DungeonTileGenerator : MonoBehaviour {
	

	Dictionary<DungeonEventType,DungeonEvent> dungeonEventDict;
	public GameObject eventPrefabHolder;
	void Awake()
	{
		var events = eventPrefabHolder.GetComponentsInChildren<DungeonEvent>();
		dungeonEventDict = new Dictionary<DungeonEventType,DungeonEvent>();
		foreach (var de in events)
		{
			if(dungeonEventDict.ContainsKey(de.type))
			{
				Debug.Log("Same dungeon event type!");
			}
			dungeonEventDict.Add(de.type,de);
		}

		openSidePrefabs = openSidePrefabHolder.GetComponentsInChildren<Image>();
		closeSidePrefabs = closeSidePrefabHolder.GetComponentsInChildren<Image>();
	}
	public void setTile(RoomTile tile,DungeonEventType type)
	{
		tile.setEvent(dungeonEventDict[type]);
	}

	public GameObject tilePrefab;
	public GameObject openSidePrefabHolder;
	public GameObject closeSidePrefabHolder;

	Image[] openSidePrefabs;
	Image[] closeSidePrefabs;
	public RoomTile generateRoomTile(int[] open)
	{
		GameObject tile = Instantiate(tilePrefab);
		RoomTile roomTile = tile.GetComponent<RoomTile>();
		roomTile.side = open;
		bool someOpen = false;
		foreach(var side in open)
		{
			if(side>0)
			{
				someOpen = true;
				break;
			}
		}
		if(someOpen)
		{	
			for(int i=0;i<4;i++)
			{
				Image side;
				if(open[i]==1)
				{
					side = Instantiate(openSidePrefabs[i]);
					side.transform.SetParent(tile.transform);	
				}
				else if(open[i]==0)
				{
					side = Instantiate(closeSidePrefabs[i]);
					side.transform.SetParent(tile.transform);
				}
				else
				{
					//no wall nor path, connect room
				}
				
				//side.transform.localScale = new Vector3(1,1,1);
			}
			tile.transform.Find("cover").SetAsLastSibling();
		}
		
		//tile.transform.lossyScale = new Vector3(1,1,1);
		return roomTile;	
	}
}


