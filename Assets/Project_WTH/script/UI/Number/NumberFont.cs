using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class NumberFont : MonoBehaviour {
	public List<Sprite> numberSprites;
	public GameObject NumberPrefab;
	public Sprite getSingleNumSprite(int num)
	{
		if(num<0)
		num = 0;
		else if(num>9)
		num = 9;
		return numberSprites[num];
	}
}
