using UnityEngine;
using System.Collections;
using UnityEngine.UI;
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
}
