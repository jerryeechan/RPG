using UnityEngine;
using System.Collections;

public class Piece : MonoBehaviour {
	Block[] blocks;
	void Awake()
	{
		blocks = GetComponentsInChildren<Block>();
		foreach(Block b in blocks)
			b.isCentered = false;
		//GetComponent<Rigidbody2D>().velocity = new Vector2(0,-2);
	}
	void Collided()
	{
		Destroy(GetComponent<Rigidbody2D>());
		foreach(Block b in blocks)
		{
			b.transform.position = new Vector3(Mathf.RoundToInt(b.transform.position.x),Mathf.RoundToInt(b.transform.position.y));
			b.isCentered = true;
			b.transform.SetParent(TetrisGameManager.centerBlocksTransform);
			TetrisGameManager.instance.putBlockOnGrid(b);
		}
		TetrisGameManager.instance.CheckEliminate();
		Destroy(gameObject);
	}
	public void LaunchWithForce(Vector2 force)
	{
		GetComponent<Rigidbody2D>().AddForce(force);
	}
	public void Launch(Vector2 velocity)
	{
		GetComponent<Rigidbody2D>().velocity = velocity;
	}
}