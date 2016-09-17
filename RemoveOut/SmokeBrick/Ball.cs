using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter2D(Collision2D collision2D)
	{
		Collider2D c2D = collision2D.collider;
		if(c2D.tag=="brick")
		{
			Destroy(c2D.gameObject);
		}			
		
	}		
}
