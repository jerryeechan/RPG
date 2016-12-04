using UnityEngine;
using System.Collections;

public class CursorManager : Singleton<CursorManager> {

	public Texture2D cursorPointerTexture;
	public Texture2D cursorNormalTexture;
	public Texture2D attackTexture;

	void Awake()
	{
		NormalMode();
	}
	public void PointerMode()
	{
	//	print("PointerMode");
		Cursor.SetCursor(cursorPointerTexture,new Vector2(8,0),CursorMode.Auto);
	}

	public void NormalMode()
	{
	//	print("normalMode");
		Cursor.SetCursor(cursorNormalTexture,new Vector2(8,0),CursorMode.Auto);
	}
	public void AttackMode()
	{
		Cursor.SetCursor(attackTexture,new Vector2(8,0),CursorMode.Auto);
	}
}