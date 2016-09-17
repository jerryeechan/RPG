using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

	// Use this for initialization
 	public bool isCentered = true;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//OnTriggerEnter2D(Collider2D collider2D)
	void OnCollisionEnter2D(Collision2D collision2D)
	{
		if(!isCentered)
			transform.parent.SendMessage("Collided");
	}
}
