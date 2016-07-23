using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	Transform healthbar;
	SpriteRenderer healthBarSpr;
	int fullValue;
	int nowValue;
	void Awake () {
		healthbar = transform.Find("healthbarIn");
		healthBarSpr = healthbar.GetComponent<SpriteRenderer>();
		SetFullValue(20);
		//SelfTest();
	}
	public void SetFullValue(int fv)
	{
		fullValue = fv;
		healthBarSpr.color = new Color32(123,195,113,255);//green
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
		healthbar.localScale = new Vector3((float)nowValue/fullValue,1);
		if(nowValue<=(float)fullValue/4)
		{
			healthBarSpr.color = new Color32(255,23,12,255);//red
		}
		else if(nowValue<=(float)fullValue/2)
		{
			healthBarSpr.color = new Color32(232,201,99,255);//yellow
		}
	}
	
	
	/*
	self test scripts below
	
	*/
	void SelfTest()
	{
		//for(int i=20;i>=0;i--)
		InvokeRepeating("minus",1,0.5f);
	}
	int v = 20;
	void minus()
	{
		if(v<=0)
			CancelInvoke("minus");
		SetNowValue(v);
		v-=2;
		
	}
}
