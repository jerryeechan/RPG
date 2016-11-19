using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
public class DungeonManager : Singleton<DungeonManager> {

	public DungeonGenerator dungeonGenerator;
	RoomTile[,] tileMap;
	RectTransform map;
	// Use this for initialization
	public CharacterTile player;
	int level;
	void Awake()
	{
		map = transform.Find("map").GetComponent<RectTransform>();
		_instance = this;
	}
	
	public void newDungeon()
	{
		level = 0;
		genMap();
	}
	public int genMap()
	{
		
		Debug.Log("generate new map");
		level++;
		tileMap = dungeonGenerator.generateMap(9,9);
		player.x = 4;
		player.y = 4;
		tileMap[player.x,player.y].reveal();
		map.anchoredPosition = player.rect.anchoredPosition = darkness.anchoredPosition = Vector2.zero;
		return level;
	}
	int dis = 32;
	public void keyPress(KeyCode key)
	{
//		print("dungeon:"+key);
		switch(key)
		{
			case KeyCode.UpArrow:
				if(testMove(player,0))
					player.y++;
			break;
			case KeyCode.LeftArrow:
				if(testMove(player,1))
					player.x--;
			break;
			case KeyCode.DownArrow:
				if(testMove(player,2))
					player.y--;
				
			break;	
			case KeyCode.RightArrow:
				if(testMove(player,3))
					player.x++;
			break;
		}
		tileMap[player.x,player.y].reveal();
		showTile(player.x,player.y);
	}
	void showTile(int x,int y)
	{
		for(int i=0;i<4;i++)
		{
			int open = tileMap[x,y].side[i];
			if(open!=0)
			{
				tileMap[x+xMoveMap[i],y+yMoveMap[i]].reveal();
			}
		}
	}
	public void keyDown(KeyCode key)
	{
		switch(key)
		{
			case KeyCode.Z:

			break;
			case KeyCode.X:

			break;
		}
	}
	Vector2[] dirMapVec={Vector3.up,Vector3.left,Vector3.down,Vector3.right};
	int[] xMoveMap={0,-1,0,1};
	int[] yMoveMap={1,0,-1,0};
	bool isplaying = false;
	public RectTransform darkness;
	public bool testMove(CharacterTile tile,int dir)
	{
		if(isplaying)
			return false;
		int isOpen = tileMap[tile.x,tile.y].side[dir];
		
		if(isOpen > 0)
		{
			DungeonPlayerStateUI.instance.getDoom(10);

			RectTransform tileTF = tile.GetComponent<RectTransform>();
			//tileTF.anchoredPosition += dirMapVec[dir]*dis;
			
			map.DOAnchorPos(map.anchoredPosition - dirMapVec[dir]*dis,0.4f,true);
			tileTF.DOAnchorPos(tileTF.anchoredPosition + dirMapVec[dir]*dis,0.2f,true);

			
			darkness.DOAnchorPos(darkness.anchoredPosition + dirMapVec[dir]*dis,0.4f,true).OnComplete(()=>{
				isplaying = false;
				tileMap[player.x,player.y].encounter();
			});
			isplaying = true;

			
			//transform.DOMove(new Vector3(2,3,4), 1);
			//LeanTween.move(tileTF,dirMapVec[dir]*dis,0.5f);	
		}
		return isOpen>0;
	}
	
	public void dungeonEventComplete(bool removeTile = false){
		if(removeTile)
			tileMap[player.x,player.y].removeEvent();	
	}

	

}
