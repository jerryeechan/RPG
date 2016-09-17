using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Chicken : MonoBehaviour {

	public Sprite dead;
	public Sprite[] sprites;
	SpriteRenderer spr;
	Animator anim;
	void Awake()
	{
		spr = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		AddScore(0);
	}
	// Update is called once per frame
	bool isDead = false;
	void Update () {
		/*if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			transform.Translate(Vector3.left*10);
			spr.sprite = sprites[0];
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			transform.Translate(Vector3.right*10);
			spr.sprite = sprites[1];
		}
		*/
		if(!isDead)
		{
			transform.Translate(Vector3.right);
			
			if(Input.GetKeyDown(KeyCode.UpArrow))
			{
				if(transform.position.y<-10)
				{
					transform.Translate(Vector3.up*10);
				}
				
				//spr.sprite = sprites[2];
			}
			else if(Input.GetKeyDown(KeyCode.DownArrow))
			{
				if(transform.position.y>-50)
				transform.Translate(Vector3.down*10);
//				spr.sprite = sprites[3];
			}
		}
		
	}
	public Text scoreText;
	public int score =0;
	void AddScore(int value)
	{
		score+=value;
		scoreText.text = "SCORE:"+score;
	}
	void OnTriggerEnter2D(Collider2D collider2D)
	{
		if(collider2D.tag=="heart")
		{
			Destroy(collider2D.gameObject);
			AddScore(5);
		}
		else if(collider2D.tag=="apple")
	    {
			Destroy(collider2D.gameObject);
			AddScore(1);
		}
		else if(collider2D.tag=="stone")
		{
			Die();
		}
		else if(collider2D.tag=="GOAL")
		{
			Win();
		}
	}
	void Die()
	{
		anim.enabled = false;
		spr.sprite = dead;
		isDead = true;
		PlayerPrefs.SetInt("win",2);
		Invoke("End",1);
	}
	void Win()
	{
		PlayerPrefs.SetInt("win",1);
		Invoke("End",1);
	}
	void End()
	{
		Application.LoadLevel("EndScene");
	}
}
