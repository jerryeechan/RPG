using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
	// public TextMesh[] texts;
	SpriteRenderer healthBarSpr;
	SpriteRenderer shieldBarSpr;

	float fullValue;
	float nowValue;
	public void init(int fvalue)
	{
		fullValue = fvalue;
		nowValue = fvalue;
	}
	void Awake () {
		Transform healthbar = transform.Find("healthbarIn");
		Transform shieldBar = transform.Find("shieldbarIn");
		shieldBarSpr = shieldBar.GetComponent<SpriteRenderer>();
		healthBarSpr = healthbar.GetComponent<SpriteRenderer>();
		//SelfTest();
	}
	public void SetFullValue(float fv)
	{
		fullValue = fv;
		//healthBarSpr.color = new Color32(123,195,113,255);//green
	}
	public void SetNowValue(float value)
	{
		nowValue = value;
		healthBarSpr.transform.localScale = new Vector3((float)nowValue/fullValue,1);

		/*
		foreach (TextMesh t in texts)
		{
			t.text = value.ToString();
		}
		*/
	}
	
	
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
