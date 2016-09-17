using UnityEngine;
using System.Collections;

public class LaunchArea : MonoBehaviour {

	public enum LaunchDirection{Left,Right,Up,Down};
	public LaunchDirection direction;
	Vector2 dir;
	float launchForce = 1000;
	void Awake()
	{
		switch(direction)
		{
			case LaunchDirection.Left:
			dir = -Vector2.right;
			break;
			
			case LaunchDirection.Right:
			dir = Vector2.right;
			break;
			
			case LaunchDirection.Down:
			dir = -Vector2.up;
			break;
			
			case LaunchDirection.Up:
			dir= Vector2.up;
			break;
		}
	}
	
	void Launch(Piece piece)
	{
		piece.LaunchWithForce(dir*launchForce);
	}
	
	Piece holdingPiece;
	void roundToIntMoveWithMouse(Transform t)
	{
		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		t.position = new Vector3(Mathf.RoundToInt(pos.x),Mathf.RoundToInt(pos.y));
	}
	void OnMouseDown()
	{
		Debug.Log("onMouseDown");
		holdingPiece = TetrisGameManager.instance.GenerateRandomPiece();
		roundToIntMoveWithMouse(holdingPiece.transform);
	}
	void OnMouseUp()
	{
		Debug.Log("onMouseUp");
		Launch(holdingPiece);
	}
	void OnMouseDrag()
	{
		roundToIntMoveWithMouse(holdingPiece.transform);
	}
	
}
