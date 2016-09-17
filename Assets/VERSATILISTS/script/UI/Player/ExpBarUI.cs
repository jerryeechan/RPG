using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
public class ExpBarUI : MonoBehaviour {

	CompositeText expText;
	Image fillImage;
	int fullValue;
	int curValue;
	//CompositeText text;
	
	CharacterData bindData;
	
	public int expValue
	{
		set{
			if(bindData==null)
				return;
			
			if(value>=fullValue)
			{
				curValue = value%fullValue;
				int levelIncrease = value/fullValue;
				bindData.level+=levelIncrease;
				fillImage.DOFillAmount(1,1).OnComplete(()=>{
					//level up
					fillImage.fillAmount = 0;
					fullValue = getLevelFullExp(bindData.level);
					fillImage.DOFillAmount((float)curValue/fullValue,1);
					if(expText)
						expText.text = bindData.level.ToString();
					bindData.statPoints += 3;
				});
			}
			else if(curValue < 0)
				curValue = 0;
			else
			{
				curValue = value;
				fillImage.DOFillAmount((float)curValue/fullValue,1);
			}
			
			bindData.exp = curValue;
			
			//text.DOValue() = curValue.ToString();
		}
		get{
			return curValue;
		}
	}

	

	void Awake()
	{
		//text = GetComponentInChildren<CompositeText>();
		fillImage = transform.Find("fill").GetComponent<Image>();
		expText = GetComponentInChildren<CompositeText>();
	}

	public void init(CharacterData chData)
	{	
		bindData = chData;
		fullValue = getLevelFullExp(chData.level);
		expValue = chData.exp;
		if(expText)
			expText.text = chData.level.ToString();
	}


	public int getLevelFullExp(int level)
	{
		return level*50;
	}
}
