using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class NumberGenerator : MonoBehaviour {

	public NumberFont numSprites;
	public NumberFont DamageSprites;
	public NumberFont manaSprites;
	public NumberFont cdSprites;
	public GameObject damageNumPrefab;
	
	
	public static NumberGenerator instance;
	//public NumberTextStyle numSprites;
	void Awake()
	{
		instance = this;
		DontDestroyOnLoad(gameObject);
	}
	public int spriteWidth;
	public Sprite getManaSprite(int num)
	{
		return manaSprites.getSingleNumSprite(num);
	}
	public Sprite getCDSprite(int num)
	{
		return cdSprites.getSingleNumSprite(num);
	}
	public void GetDamageNum(Vector3 position, int value)
	{
		char[] vchar = value.ToString().ToCharArray();
		GameObject numText = new GameObject("num");
		List<Transform> digitTransforms = new List<Transform>();
		bool isZero = false;
		
		
		
		for(int i=0;i<vchar.Length;i++)
		{
			GameObject numDigit = (GameObject)Instantiate(damageNumPrefab);
			SpriteRenderer spr = numDigit.GetComponent<SpriteRenderer>();
			
			spr.sprite = numSprites.getSingleNumSprite(vchar[i]-'0');
			spr.sortingLayerName = "UI";
			numDigit.transform.parent = numText.transform;
			digitTransforms.Add(numDigit.transform);
		}
		float halfDigitLength = digitTransforms.Count/2;
		
		for(int i=0;i<digitTransforms.Count;i++)
		{
			digitTransforms[i].position = new Vector3(i*spriteWidth-halfDigitLength*spriteWidth,20);
			digitTransforms[i].GetComponent<Rigidbody2D>().velocity = new Vector2(10,30);
		}
		
		numText.transform.position = position;
	}
	public void ShowNum(Vector3 position, int value)
	{
		char[] vchar = value.ToString().ToCharArray();
		GameObject numText = new GameObject("num");
		List<Transform> digitTransforms = new List<Transform>();
		bool isZero = false;
		
		
			
		for(int i=0;i<vchar.Length;i++)
		{
			GameObject numDigit = new GameObject("digit");
			SpriteRenderer spr = numDigit.AddComponent<SpriteRenderer>();
			spr.sprite = numSprites.getSingleNumSprite(vchar[i]-'0');
			spr.sortingLayerName = "UI";
			numDigit.transform.parent = numText.transform;
			digitTransforms.Add(numDigit.transform);
		}
		float halfDigitLength = digitTransforms.Count/2;
		
		for(int i=0;i<digitTransforms.Count;i++)
		{
			digitTransforms[i].position = new Vector3(i*spriteWidth-halfDigitLength*spriteWidth,0);
		}
		
		numText.transform.position = position;
	}
	
	
	void SelfTest()
	{
		
		ShowNum(Vector3.zero,12);
		ShowNum(Vector3.zero,888);
	}
}
