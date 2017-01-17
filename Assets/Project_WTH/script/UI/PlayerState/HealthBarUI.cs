using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
public class HealthBarUI : MonoBehaviour {
	protected Image fillImage;
	public int fullValue = 0;
	protected int curValue = 0;
	CompositeText text;

	void setValue(int v)
	{
		if(v>fullValue)
				curValue = fullValue;
			else if(curValue < 0)
				curValue = 0;
			else
				curValue = v;
	}


	public virtual void addValue(int v,TweenCallback complete)
	{
		int oldValue = curValue;
		setValue(v);
		animatefillBar(complete);
		//if(text)
		//	text.DOValue(oldValue,curValue);
	}
	public virtual int currentValue
	{
		set{
			setValue(value);
			fillBar();
			if(text)
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
			//Debug.LogError("HealthbarUI no text");
		}
		fillImage = transform.Find("fill").GetComponent<Image>();

		if(fillImage==null)
		{
			Debug.LogError("barUI no fillImage");
		}
	}

	public void init(int fullValue)
	{
		this.fullValue = fullValue;
		currentValue = fullValue;
	}

	
	protected void fillBar()
	{
		fillImage.fillAmount = (float)curValue/fullValue;
	}
	protected void animatefillBar(TweenCallback complete)
	{
		fillImage.DOFillAmount((float)curValue/fullValue,1).OnComplete(complete);
	}
}
