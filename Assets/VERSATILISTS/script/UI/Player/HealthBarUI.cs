using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HealthBarUI : MonoBehaviour {
	Image fillImage;
	int fullValue = 0;
	int curValue = 0;
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
		if(text==null)
		{
			Debug.LogError("HealthbarUI no text");
		}
		fillImage = transform.Find("fill").GetComponent<Image>();

		if(fillImage==null)
		{
			Debug.LogError("HealthbarUI no fillImage");
		}
	}

	public void init(int fullValue)
	{
		this.fullValue = fullValue;
		healthValue = fullValue;
	}
}
