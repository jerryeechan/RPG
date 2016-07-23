using UnityEngine;
using System.Collections;

public class Rectangle{
	public float rectW;
	public float rectH;
	public Vector2 rectPos;
	
	public int rectArea;
	public Rectangle(Vector2 _rectPos,float rectW,float rectH)
	{
		rectPos = _rectPos;
		this.rectW = rectW;
		this.rectH = rectH;
		rectArea =(int)(rectW*rectH);
	}
	public void setPos(int x,int y)
	{
		rectPos = new Vector2(x,y);
	}
	public void setPosX(int x)
	{
		rectPos = new Vector2(x,rectPos.y);
	}
	public int rectEdgeLengths()
	{
		return (int)(rectH+rectW);
	}
	public static Rectangle zero
	{
		get{ return new Rectangle(new Vector2(0,0),0,0);}
	}
	
	
}
