using UnityEngine;
using System.Collections;

public class ChickenGameManager : MonoBehaviour {

	public GameObject heartPrefab;
	public GameObject applePrefab;
	public GameObject stonePrefab;
	public GameObject largeStonePrefab;
	public GameObject fensPrefab;
	public Chicken chicken;
	
	public Transform bg1;
	public Transform bg2;
	int[,] map;
	
	void Awake()
	{
		/*
		map = new int[,]{
			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{0,1,1,1,1,1,1,0,1,1,0,1,1,0,0,1,0,0,0,1,0},
			{0,0,0,1,0,0,1,1,1,1,1,1,1,1,0,1,0,0,0,1,0},
			{0,0,0,1,0,0,0,1,1,1,1,1,1,1,0,1,0,0,0,1,0},
			{0,0,0,1,0,0,0,0,1,1,1,1,1,0,0,1,0,0,0,1,0},
			{0,0,0,1,0,0,0,0,0,1,1,1,0,0,0,1,0,0,0,1,0},
			{2,1,1,1,1,1,0,0,0,0,1,0,0,0,0,1,1,1,1,1,0},
			{0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0},
		};
		*/
		map = new int[,]{
			{2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
			{0,0,0,0,0,0,0,0,1,1,0,1,1,0,0,1,0,0,0,1,0,0,1,0,0,0,1,0,1,1,1,0,0,0,2,1,0,0,0},
			{0,0,0,0,0,0,0,1,1,1,1,1,1,1,0,1,0,0,0,1,0,0,0,1,0,1,0,0,1,0,0,1,0,0,2,1,2,0,0},
			{0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,1,0,0,0,1,0,0,0,0,1,0,0,0,1,0,0,1,0,0,2,1,2,0,2},
			{0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,0,1,0,0,1,0,0,2,1,3,0,0},
			{0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,1,1,0,0,0,1,0,0,0,1,0,1,1,1,0,0,0,2,1,0,2,0},
			{2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
		};
		
		int setMapL = map.GetLength(1);
		
		chicken.transform.position = new Vector3(0,-30);
		for(int i=1;i<80;i++)
		{
			int stoneCount = 0;
			for(int j=0;j<map.GetLength(0);j++)		
			{
				Vector3 pos = new Vector3(i*10+5*10,-j*10);
				if(j==0||j==6)
				{
					Instantiate(fensPrefab,pos,Quaternion.identity);
				}
				else
				{
					float rv = Random.value*20;
					if(rv>=8&&rv<=13)
						Instantiate(applePrefab,pos,Quaternion.identity);
					else if(rv>=1&&rv<=2)
						Instantiate(heartPrefab,pos,Quaternion.identity);
					else if(rv>=3&&rv<=5.5)
					{
						if(stoneCount!=3)
						{
							Instantiate(stonePrefab,pos,Quaternion.identity);
							stoneCount++;
						}
					}
					//else if(rv==3)
					//	Instantiate(largeStonePrefab,pos,Quaternion.identity);
				}
		}
		
			
			
		}
		for(int j=0;j<map.GetLength(0);j++)
		for(int i=0;i<map.GetLength(1);i++)
		{
			Vector3 pos = new Vector3(i*10+80*10,-j*10);
			if(map[j,i]==3)
			{
				Instantiate(applePrefab,pos,Quaternion.identity);
			}
			else if(map[j,i]==1)
			{
				Instantiate(heartPrefab,pos,Quaternion.identity);
			}
			else if(map[j,i]==2)
			{
				Instantiate(fensPrefab,pos,Quaternion.identity);
			}
			else if(map[j,i]==4)
			{
				Instantiate(stonePrefab,pos,Quaternion.identity);
			}
			else if(map[j,i]==5)
			{
				Instantiate(largeStonePrefab,pos,Quaternion.identity);
			}
			
		}
	}
	
	float lastx = 0;
	bool turn = true;
	void Update()
	{
		if(chicken.transform.position.x-lastx>192)
		{
			if(turn==false)
			bg1.Translate(new Vector3(384,0));
			else
			bg2.Translate(new Vector3(384,0));
			
			lastx +=192;
			turn = !turn;
		}
	}
}
