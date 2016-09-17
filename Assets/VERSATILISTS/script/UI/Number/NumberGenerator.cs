using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class NumberGenerator : Singleton<NumberGenerator>{

	public NumberFont numSprites;
	public NumberFont DamageSprites;
	public NumberFont cdSprites;
	public DamageNumber damageNumPrefab;
	public GameObject numHodler;
	
	//public NumberTextStyle numSprites;
	void Awake()
	{
//		DontDestroyOnLoad(gameObject);
	}
	void Start()
	{
		SelfTest();
	}
	public int spriteWidth;
	
	public void GetDamageNum(Vector3 position, int value)
	{
		char[] vchar = value.ToString().ToCharArray();
		GameObject numText = Instantiate(numHodler);
		List<Transform> digitTransforms = new List<Transform>();
		//bool isZero = false;
		
		for(int i=0;i<vchar.Length;i++)
		{
			DamageNumber numDigit = Instantiate(damageNumPrefab);
			numDigit.index = i;
			numDigit.Show();
			SpriteRenderer spr = numDigit.GetComponent<SpriteRenderer>();
			

			spr.sprite = DamageSprites.getSingleNumSprite(vchar[i]-'0');
			spr.sortingLayerName = "UI";
			
			digitTransforms.Add(numDigit.transform);
		}
		float halfDigitLength = digitTransforms.Count/2;
		
		for(int i=0;i<digitTransforms.Count;i++)
		{
			digitTransforms[i].transform.parent = numText.transform;
			digitTransforms[i].localPosition = new Vector3(i*spriteWidth-halfDigitLength*spriteWidth,0);
		}
		numText.GetComponent<Rigidbody2D>().velocity = new Vector2(-20,30);
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
		//GetDamageNum(Vector3.up*5,12);
		//ShowNum(Vector3.zero,12);
		//ShowNum(Vector3.zero,888);
	}
}
