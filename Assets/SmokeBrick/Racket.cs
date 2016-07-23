using UnityEngine;
using System.Collections;

public class Racket : MonoBehaviour {

	// Use this for initialization
	public Transform ball;
	void Start () {
		ball.GetComponent<Rigidbody2D>().velocity = new Vector2(2,2);
		myTransform = transform;
	}
	Transform myTransform;
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Translate(Vector3.left*Time.fixedDeltaTime*2);
		}
		else if(Input.GetKey(KeyCode.RightArrow))
		{
			transform.Translate(-Vector3.left*Time.fixedDeltaTime*2);
		}
	}
	
	void OnCollisionEnter2D(Collision2D collision2D)
	{
		Collider2D c2D = collision2D.collider;
		if(c2D.tag=="ball")
		{
			// transform.position - collision2D.contacts[0].point
				
			
		}
	}
	
}
