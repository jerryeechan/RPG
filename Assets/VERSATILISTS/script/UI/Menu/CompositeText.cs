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
	public void animateValue(int startValue,int diff)
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
						newString = prefix+":";
					newString += (int)i +postfix;
					t.text = newString;
			}
			yield return new WaitForSeconds(0.05f);
		}
	}
}
