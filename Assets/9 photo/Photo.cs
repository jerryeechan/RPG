using UnityEngine;
using System.Collections;

public class Photo : MonoBehaviour {

	int id;
	SpriteRenderer spr;
	public void init(int id)
	{
		this.id = id;
		spr = GetComponent<SpriteRenderer>();
	}
	
	void OnMouseOver()
	{
		Debug.Log("hover");
		
	}
	void OnMouseEnter()
	{
		transform.localScale = new Vector3(1.2f,1.2f,1);
		PhotoManager.instance.LockPhoto(id);
		PhotoManager.instance.PlayLoop(id);
		transform.position =new Vector3(transform.position.x,transform.position.y,-1);
		spr.sortingOrder = 5;
		
	}
	void OnMouseExit()
	{
		transform.localScale = new Vector3(1,1,1);
		transform.position =new Vector3(transform.position.x,transform.position.y,1);
		PhotoManager.instance.UnlockPhoto(id);
		PhotoManager.instance.StopLoop(id);
		spr.sortingOrder = 0;
	}
}
