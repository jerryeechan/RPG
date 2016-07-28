using UnityEngine;
using System.Collections;

public enum GameEventType{Combat,Treasure,Gold,Merchant,Fountain,Rest};
public enum RoomType{Normal,Boss,Peace};
public class GameEventGenerator : MonoBehaviour {

	
	public void GenerateRoom(RoomType type)
	{
		GameEvent[] gameEvents = new GameEvent[6];
		switch (type)
		{
			case RoomType.Normal:
				for(int i=0;i<3;i++)
				{
					//gameEvents[i] = GameEvent();
				}
			break;
		}
	}

	
}
