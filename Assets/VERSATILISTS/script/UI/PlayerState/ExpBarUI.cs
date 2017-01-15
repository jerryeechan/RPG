using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
public class ExpBarUI : HealthBarUI {

	public CompositeText levelText;
	//CompositeText text;
	
	CharacterData _bindData;

	public CharacterData bindData{
		set
		{
			fullValue = getLevelFullExp(value.level);
			curValue = value.exp;
			_bindData = value;
			fillBar();
		}
	}
	

	public override void addValue(int val,TweenCallback completeFunc)
	{
		int value = curValue+val;

		if(value>=fullValue)
			{
				curValue = value%fullValue;
				int levelIncrease = value/fullValue;

				fillImage.DOFillAmount(1,1).OnComplete(()=>{
					//level up
					fillImage.fillAmount = 0;
					fullValue = getLevelFullExp(_bindData.level);
					fillImage.DOFillAmount((float)curValue/fullValue,1).OnComplete(completeFunc);
					if(levelText)
						levelText.text = _bindData.level.ToString();
				});
			}
			else if(curValue < 0)
			{
				curValue = 0;
			}
			else
			{
				curValue = value;
				fillImage.DOFillAmount((float)curValue/fullValue,1).OnComplete(completeFunc);
			}
			_bindData.exp = curValue;
	}

	public override int currentValue
	{
		set{			
			if(value>=fullValue)
			{
				curValue = value%fullValue;
				int levelIncrease = value/fullValue;

				fillImage.DOFillAmount(1,1).OnComplete(()=>{
					//level up
					fillImage.fillAmount = 0;
					fullValue = getLevelFullExp(_bindData.level);
					fillImage.DOFillAmount((float)curValue/fullValue,1);
					if(levelText)
						levelText.text = _bindData.level.ToString();
				});
			}
			else if(curValue < 0)
			{
				curValue = 0;
			}
			else
			{
				curValue = value;
				fillImage.DOFillAmount((float)curValue/fullValue,1);
			}
			_bindData.exp = curValue;
		}
		get{
			return curValue;
		}
	}


	public int getLevelFullExp(int level)
	{
		return level*50;
	}
}
