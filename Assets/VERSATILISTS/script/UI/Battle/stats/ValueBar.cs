using UnityEngine;
using System.Collections;

public class ValueBar : MonoBehaviour {
	Transform valueBarIn;
	SpriteRenderer barSpr;
	int fullValue;
	int nowValue;
	void Awake () {
		valueBarIn = transform.Find("valueBarIn");
		barSpr = valueBarIn.GetComponent<SpriteRenderer>();
		SetFullValue(fullValue);
	}
	public void SetFullValue(int fv)
	{
		fullValue = fv;
		barSpr.color = new Color32(123,195,113,255);//green
	}
	/*void ChangeValue(int d)
	{
		nowValue+=d;
		if(nowValue>fullValue)
			nowValue = fullValue;
		if()
	}*/
	
	public void SetNowValue(int value)
	{
		nowValue = value;
		valueBarIn.localScale = new Vector3((float)nowValue/fullValue,1);
		if(nowValue<=(float)fullValue/4)
		{
			barSpr.color = new Color32(255,23,12,255);//red
		}
		else if(nowValue<=(float)fullValue/2)
		{
			barSpr.color = new Color32(232,201,99,255);//yellow
		}
	}
	
	//interval change value (start corutine)
	/*
	self test scripts below
	
	*/
	
	int v = 20;
	void minus()
	{
		if(v<=0)
			CancelInvoke("minus");
		SetNowValue(v);
		v-=2;
		
	}
}
