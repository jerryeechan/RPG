using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class DungeonGenerator : MonoBehaviour {

	RoomTile tiles;
	RoomTile [,] tileMap;
	// Use this for initialization
	List<RoomTile> edgeTiles;
	
	DungeonTileGenerator tileGenerator;
		
	void Awake()
	{
		//generateRoomTile(new int[4]{1,0,1,1});
		tileGenerator = GetComponent<DungeonTileGenerator>();
	}
	// Update is called once per frame
	void setEvents()
	{
		foreach(var tile in edgeTiles)
		{
			if(Random.value>0.2)
				tileGenerator.setTile(tile,DungeonEventType.Gold);
		}
		tileGenerator.setTile(edgeTiles[edgeTiles.Count-1],DungeonEventType.Exit);
		tileGenerator.setTile(edgeTiles[edgeTiles.Count-2],DungeonEventType.Boss);
		tileGenerator.setTile(edgeTiles[0],DungeonEventType.Exp);
		tileGenerator.setTile(edgeTiles[1],DungeonEventType.Exp);
	}

	static int tileSize = 32;
	public RoomTile[,] generateMap(int w,int h)
	{
		if(tileMap!=null)
		{
			foreach(var r in tileMap)
			{
				if(r)
				{
					Destroy(r.gameObject);
				}
			}
		}

		tileMap = new RoomTile[w,h];
		RoomTile room = tileGenerator.generateRoomTile(new int[]{1,1,1,1});
		edgeTiles = new List<RoomTile>();
		room.transform.SetParent(transform,false);
		width = w;
		height = h;
		centerX = room.x = (w-1)/2;
		centerY = room.y = (h-1)/2;
		tileMap[centerX,centerY] = room;
		
		//randomExpand(room);
		BFSExpand(room);
		setEvents();
		//transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(tileSize*width*2,tileSize*height*2);
		return tileMap;
	}
	
	public int[] randomArray()
	{
		int[] array = new int[4];
		for(int i=0;i<4;i++)
		{
			if(Random.value>0.9f)
				array[i] = 1;//Mathf.RoundToInt(Random.value);
			else if(Random.value>0.7f)
				array[i] = 2;
			else
				array[i] = 0;
		}
		return array;
	}

	int[] opposite = {2,3,0,1}; 
	int width;
	int height;
	int centerX;
	int centerY;
	
	//up 0
	//left 1
	//down 2
	//right 3
	
	int[] NearTileTest(RoomTile tile,int dir,int[] array)
	{
		int x = tile.x;
		int y = tile.y;
		switch (dir)
		{
			case 0:
				y++;break;
			case 1:
				x--;break;
			case 2:
				y--;break;
			case 3:
				x++;break;
			default:break;
		}
		//out of bound
		if(x<0||y<0||x==width||y==height)
		{
			return null;
		}
		//already have tile
		if(tileMap[x,y]!=null)
			return null;
		int[] point = {x,y};

		if(y==0)
		{
			array[2] = 0;
		}
		else if(y==height-1)
		{
			array[0] = 0;	
		}
		if(x==0)
		{
			array[1] = 0;
		}
		else if(x==height-1)
		{
			array[3] = 0;	
		}

		return point;
	}
	RoomTile lastTile;
	void BFSExpand(RoomTile startTile)
	{
		Queue<RoomTile> expandTiles = new Queue<RoomTile>();
		expandTiles.Enqueue(startTile);
		while(expandTiles.Count>0)
		{
			bool anyExpand = false;
			RoomTile tile = expandTiles.Dequeue();
			for(int i=0;i<4;i++)
			{
				//wall
				if(tile.side[i]==0)
				continue;

				int[] array = randomArray();
				int[] point = NearTileTest(tile,i,array);
				if(point == null)
				{
					continue;
				}
				int x = point[0];
				int y = point[1];				
				checkTileOpen(x,y,array);
				expandTiles.Enqueue(setUpNewTile(array,x,y));
				anyExpand = true;
			}	 
			if(anyExpand == false)
			{
				edgeTiles.Add(tile);
			}
		}
	}
	RoomTile setUpNewTile(int[] array,int x,int y)
	{
		RoomTile newTile = tileGenerator.generateRoomTile(array);
		newTile.x = x;
		newTile.y = y;
		RectTransform tileRT = newTile.GetComponent<RectTransform>();
		tileRT.SetPositionX((x-centerX)*tileSize);
		tileRT.SetPositionY((y-centerY)*tileSize);
		tileRT.SetPositionZ(0);
		tileRT.SetParent(transform,false);
		tileMap[x,y] = newTile;
		return newTile;
	}
	public void randomExpand(RoomTile tile)
	{
	
		
		bool anyExpand = false;
		for(int i=0;i<4;i++)
		{
			if(tile.side[i]==0)
				continue;
			int[] array = randomArray();
			int x = tile.x;
			int y = tile.y;
			switch (i)
			{
				case 0:
					y++;break;
				case 1:
					x--;break;
				case 2:
					y--;break;
				case 3:
					x++;break;
				default:break;
			}
			//out of bound
			if(x<0||y<0||x==width||y==height)
			{
				return;
			}
			//already have tile
			if(tileMap[x,y]!=null)
				continue;
			//at bound
			if(y==0)
			{
				array[2] = 0;
			}
			else if(y==height-1)
			{
				array[0] = 0;	
			}
			if(x==0)
			{
				array[1] = 0;
			}
			else if(x==height-1)
			{
				array[3] = 0;	
			}

			
			//array[openSide] = 1;
			checkTileOpen(x,y,array);
			

			RoomTile newTile = tileGenerator.generateRoomTile(array);//generateRoomTile(array);
			newTile.x = x;
			newTile.y = y;
			RectTransform tileRT = newTile.GetComponent<RectTransform>();
			
			tileRT.SetPositionX((x-centerX)*tileSize);
			tileRT.SetPositionY((y-centerY)*tileSize);
			tileRT.SetPositionZ(0);
			tileRT.SetParent(transform,false);
			tileMap[x,y] = newTile;
			//StartCoroutine(Delay());
			anyExpand = true;
			
			randomExpand(newTile);
		}

		//the edge tiles
		if(anyExpand == false)
		{
			edgeTiles.Add(tile);
		}
			
		//tile.transform.x
	}
	void checkTileOpen(int x,int y, int[] array)
	{
		if(x-1>=0&&tileMap[x-1,y])//left tile right open
		{
			array[1] = tileMap[x-1,y].side[3];
		}
		if(x+1<width&&tileMap[x+1,y])//right tile
		{
			array[3] = tileMap[x+1,y].side[1];
		}
		if(y+1<height&&tileMap[x,y+1])//up tile 
		{
			array[0] = tileMap[x,y+1].side[2];
		}
		if(y-1>=0&&tileMap[x,y-1])//up tile 
		{
			array[2] = tileMap[x,y-1].side[0];
		}
	}
	
}
public enum DungeonEventType{None,Monster,Boss,Tressure,Trap,Exit,Gold,Exp,Doom};