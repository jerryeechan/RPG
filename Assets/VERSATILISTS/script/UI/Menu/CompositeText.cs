using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
public class CompositeText : MonoBehaviour {

	public string prefix;
	public string postfix;

	Text[] texts;

	void OnValidate()
	{
		texts = GetComponentsInChildren<Text>();
		foreach(Text t in texts)
		{
			t.raycastTarget = false;
		}
	}
	void Awake()
	{
		texts = GetComponentsInChildren<Text>();
		foreach(Text t in texts)
		{
			t.raycastTarget = false;
		}
	}

	public string text{
		set{
			foreach(Text t in texts)
			{
				string newString = "";
				if(prefix!="")
					newString = prefix;
				newString+= value+postfix;
				t.text = newString;
				
			}
		}
		get{
			return texts[0].text;
		}
	}
	public Color color{
		set{
			foreach(Text t in texts)
			{
				t.color = value;
			}
		}
	}
	public void DOText(string str)
	{
		foreach(Text t in texts)
		{
			t.text = "";
			t.DOText(str,1f);
		}
	}
	public void PopText(string str)
	{
		
		foreach(Text t in texts)
		{
			//print("pop text");
			t.text = str;
			t.DOFade(0,0);
			pop(t);
		}
	}
	void pop(Text t)
	{	
		t.DOFade(1,0.5f).OnComplete(()=>{
			t.DOFade(0,0.5f).SetDelay(1);	
		});
	}
	public void DOValue(int startValue,int diff)
	{
		StopCoroutine(number(startValue,diff));
		StartCoroutine(number(startValue,diff));
	}
	IEnumerator number(int startValue,int diff)
	{
		float inc = 1;
		if(diff>5)
		{
			inc = (float)diff/10;
		}
		
		for(float i=startValue;i<=startValue+diff;i+=inc)
		{
			foreach(Text t in texts)
			{
				string newString = "";
					if(prefix!="")
						newString = prefix;
					newString += (int)i +postfix;
					t.text = newString;
			}
			yield return new WaitForSeconds(0.05f);
		}
	}
}
