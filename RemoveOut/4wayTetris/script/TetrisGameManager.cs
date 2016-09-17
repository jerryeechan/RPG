using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EliminatePara{
	public static int minEliminateWidth = 5;
	public static int minEliminateArea = 25;
}
public class TetrisGameManager : Singleton<TetrisGameManager> {

//	public Block blockPrefab;
	//Block[,] blockGrid;
	
	public Block[,] blockGrid;
	int[][]cache;
	int gridW = 5;
	public Transform centerBlocks;
	public static Transform centerBlocksTransform;
	
	public List<Piece> piecePrefab;
	void Awake()
	{
		//TestEliminate();
		isGridFilled = new bool[gridW,gridW];
		//GenerateCenterPiece();
		blockGrid = new Block[gridW,gridW];
		
		DebugInfo.instance.initDebugInfo(gridW);
		
		LoadCenterPieces();
		centerBlocksTransform = centerBlocks;
		
	}
	
	//load the initial blocks of the stage in the center
	void LoadCenterPieces()
	{
		Block[] blocks = centerBlocks.GetComponentsInChildren<Block>();
		for(int i=0;i<blocks.Length;i++)
		{
			putBlockOnGrid(blocks[i]);
		}
	}
	
	//set if there is block on the grid position
	void setBlockOnGrid(int x,int y,bool hasBlock)
	{
		if(DebugInfo.instance.isDebugEnabled)
		{
			DebugInfo.instance.setGrid(x,y,hasBlock);
		}
		isGridFilled[x,y] = hasBlock;
	}
	public Piece GenerateRandomPiece()
	{
		int r = Random.Range(0,piecePrefab.Count);
		return Instantiate(piecePrefab[r]) as Piece;
	}
	
	//put a block on the grid
	public void putBlockOnGrid(Block b)
	{
		int _x = (int)b.transform.position.x;
		int _y = (int)b.transform.position.y;
		blockGrid[_x,_y] = b;
		setBlockOnGrid(_x,_y,true);
	}
	void GenerateCenterPiece()
	{
		JPoolManager.instance.GetObject("block").transform.position = new Vector3(7,7);
		//blockGrid[7,7] = true;
	}
	bool[,] isGridFilled;
	void TestEliminate()
	{
		blockGrid = new Block[10,10];
		isGridFilled = new bool[10,10];
		for(int i=0;i<10;i++)
			for(int j=0;j<10;j++)
			{
			isGridFilled[i,j] = false;
			}
		for(int i=2;i<4;i++)
		{
			for(int j=0;j<3;j++)
			{
				isGridFilled[i,j] = true;
			}
		}
	}
	public Rectangle CheckEliminate()
	{
		int w = blockGrid.GetLength(0);
		int h = blockGrid.GetLength(1);
		
		cache = new int[w][];
		
		Rectangle maxRect = Rectangle.zero;
		for(int i=0;i<w;i++)
		{
			cache[i] = new int[h];
			for(int j=0;j<h;j++)
			{
				if(isGridFilled[i,j]==false)
				{
					cache[i][j] = 0;
				}
				else
				{
					if(i>0)
						cache[i][j] = cache[i-1][j]+1;
					else
						cache[i][j] =1;
				}
				
			}
			Rectangle rowMaxRect = findRowMax(cache[i],i);
			
			Debug.Log("row:"+i);
			Debug.Log("rowMax area:"+rowMaxRect.rectArea);
			Debug.Log(rowMaxRect.rectPos);
			
			if(rowMaxRect.rectArea>maxRect.rectArea||rowMaxRect.rectArea==maxRect.rectArea&&rowMaxRect.rectEdgeLengths()<maxRect.rectEdgeLengths())
				maxRect = rowMaxRect;
		}
		
		Debug.Log("max rect position"+maxRect.rectPos);
		Debug.Log("max rect size"+maxRect.rectArea);
		Debug.Log("w and h:"+maxRect.rectW+maxRect.rectH);
		
		Eliminate(maxRect);
		
		return maxRect;
	}
	
	//Eliminate x*y
	private void Eliminate(Rectangle maxRect)
	{
		int left = (int)maxRect.rectPos.x;
		int top = (int)maxRect.rectPos.y;
		int right = left+(int)maxRect.rectW;
		int bottom = top+(int)maxRect.rectH;
		
		if(maxRect.rectArea>EliminatePara.minEliminateArea)
		for(int i=left;i<right;i++)
		{
			for(int j=top; j<bottom;j++)
			{
				Debug.Log("Destroy at "+i+" "+j);
				Destroy(blockGrid[i,j].gameObject);
				isGridFilled[i,j] = false;
			}
		}
	}
	
	private Rectangle findRowMax(int[] height,int index){
		Stack<int> heights = new Stack<int>();//height of each row of the grid
		Stack<int> indices = new Stack<int>();//
		Vector2 maxRect= Vector2.zero;//x:w, y:h
		Vector2 position = new Vector2(index,0);
		int max = 0;
		int lastIndex = 0;
		int posY = 0;
		int heightofMax;
		
		for(int i=0;i<height.Length;i++){
			if(heights.Count==0 || height[i]>heights.Peek()){ //current > top,just push in
				heights.Push(height[i]);
				indices.Push(i);
			}
			else if(height[i]<heights.Peek())
			{ //current < top
				while(heights.Count!=0 && heights.Peek()>height[i])
				{
					lastIndex=indices.Pop();
					int temp = heights.Pop();
					Debug.Log("width:"+temp);
					Debug.Log("lastIndex:"+lastIndex+" i:"+i);
					Debug.Log("max"+max);
					if((i-lastIndex)*temp > max)
					{
						maxRect = new Vector2(temp,i-lastIndex);
						max = (i-lastIndex)*temp;
						posY = lastIndex;
						heightofMax = temp;
					}
				}
				heights.Push(height[i]);
				indices.Push(lastIndex);
			}
			//note:if current = top , just ignore it.
		}
		/**
         * note: after processing, there still maybe elements in the stack.
         *       but they MUST BE in ascending order in the stack from bottom to top.
         */
         
		while(heights.Count!=0){
			//int value= (height.Length-indices.Pop())*heights.Pop();
			//Vector2 pos = new Vector2();
			lastIndex = indices.Pop();
			int h = heights.Pop();
			Vector2 rect = new Vector2(h,height.Length-lastIndex);
			
			//Debug.Log(rect);
			int temp = (int)(rect.x*rect.y);
			if(temp>max)
			{
				posY = lastIndex;
				heightofMax = h;
				maxRect = new Vector2(rect.x,rect.y);
				max = temp;
			}
		}
		
		Rectangle R = new Rectangle(new Vector2(index-maxRect.x+1,posY),maxRect.x,maxRect.y);
		return R;
		/*
		while(heights.Count!=0){
			int temp = (height.Length-indices.Pop())*heights.Pop();
			if(temp>max)
				max = temp;
		}
		return max;    
		*/
	}
	
}
