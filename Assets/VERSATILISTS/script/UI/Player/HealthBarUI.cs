using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HealthBarUI : MonoBehaviour {
	Image fillImage;
	int fullValue;
	int curValue;
	CompositeText text;

	public int healthValue
	{
		set{
			if(value>fullValue)
				curValue = fullValue;
			else if(curValue < 0)
				curValue = 0;
			else
				curValue = value;
			
			fillImage.fillAmount  = (float)curValue/fullValue;
			text.text = curValue.ToString();
		}
		get{
			return curValue;
		}
	}


	void Awake()
	{
		text = GetComponentInChildren<CompositeText>();
		fillImage = transform.Find("fill").GetComponent<Image>();
	}

	public void init(int fullValue)
	{
		this.fullValue = fullValue;
		healthValue = fullValue;
	}
}
