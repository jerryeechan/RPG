using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
public class CompositeText : MonoBehaviour {

	public string prefix;
	public string postfix;
	Text[] texts;
	void Awake()
	{
		texts = GetComponentsInChildren<Text>();
	}

	public string text{
		set{
			foreach(Text t in texts)
			{
				string newString = "";
				if(prefix!="")
					newString = prefix+":";
				newString+= value+postfix;
				t.text = newString;
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
		this.startValue = startValue;
		this.diff = diff; 
		StartCoroutine("number");
	}
	int startValue;
	int diff;
	IEnumerator number()
	{
		float inc = (float)diff/10;
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
