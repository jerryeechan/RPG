using UnityEngine;
using System.Collections;

public class CDBar : MonoBehaviour {

	Transform cdbar;
	SpriteRenderer cdBarSpr;
	float fullValue;
	float nowValue;
	void Awake () {
		cdbar = transform.Find("cdbarIn");
		cdBarSpr = cdbar.GetComponent<SpriteRenderer>();
		SetFullValue(10);
		//SelfTest();
	}
	public void SetFullValue(float fv)
	{
		fullValue = fv;
		//cdBarSpr.color = new Color32(123,195,113,255);//green
	}
	/*void ChangeValue(int d)
	{
		nowValue+=d;
		if(nowValue>fullValue)
			nowValue = fullValue;
		if()
	}*/
	public void SetNowValue(float value)
	{
		nowValue = value;
		cdbar.localScale = new Vector3(nowValue/fullValue,1);
	}
}
