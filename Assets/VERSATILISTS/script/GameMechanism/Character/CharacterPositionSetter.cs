using UnityEngine;
using System.Collections;

public class CharacterPositionSetter : MonoBehaviour {
	
	public static CharacterPositionSetter instance;
	public float baseY;
	public Vector3 playerPosition;
	public Vector3 enemyPosition;
	public Transform enemyPositionTransform;
	
	void Awake()
	{
		instance = this;
		playerPosition = transform.position;
		baseY = transform.position.y;
		enemyPosition = enemyPositionTransform.position;
		//DontDestroyOnLoad(gameObject);
	}
	public Vector3[] getEnemyPosition(int num)
	{
		Vector3[] positionArray = new Vector3[num];
		switch(num)
		{
			case 1:
			positionArray[0] =enemyPosition ;
			break;
			case 2:
			positionArray[0]= enemyPosition-Vector3.left*20;
			positionArray[1]= enemyPosition+Vector3.left*20;
			break;
		}
		return positionArray;
	}
}
