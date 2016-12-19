using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG;
public class DiceRollerSingle : Singleton<DiceRollerSingle> {

	// Use this for initialization
	public Sprite[] diceSprites;
	public Image[] diceImages;
	int [] diceValues;
	public Image indicator;
	
	public Button rollButton;
	CompositeText rollButtonText;
	public AnimatableCanvas panel;
	public int currentValue{
		get {
			return diceValues[currentIndex];
		}
	}
	public bool isRoundDone{
		get{return currentIndex == 3;}
	}
	void Awake()
	{
		diceValues = new int[diceImages.Length];
		rollButtonText = rollButton.GetComponentInChildren<CompositeText>();
		//panel = GetComponentInChildren<AnimatableCanvas>();
	}
	public void init()
	{
		foreach(var diceImage in diceImages)
		{
			diceImage.color = Color.gray;
		}
		currentIndex = 0;
	}
	public void Roll(DiceRollDelegate d)
	{
		 diceRollDelegate+=d;
		 Roll();
	}

	//The real roll
	public void Roll()
	{
		 isActionUsed =  false;
		 //panel.gameObject.SetActive(true);
		 indicator.GetComponent<RectTransform>().SetParent(diceImages[currentIndex].transform);
		 indicator.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		 diceImages[currentIndex].color = Color.white;
		 //currentSpriteSpin = Random.Range(min,max);
		 currentSpriteSpin = 0;
		 StartCoroutine("randomDice");
		// isActionUsed = true;
		 rollButtonText.text = "re-roll";
		 rollButton.enabled = false;
	}
	//if the action used after roll a dice
	bool isRolling;
	bool isActionUsed = false;
	public void RollButtonTouched()
	{
		if(!isActionUsed)
		{
			ReRoll();
		}
		else
		{
			Roll();
		}
	}
	public void ReRoll()
	{
		StartCoroutine("randomDice");
	}
	public void next()
	{
		currentIndex++;
		isActionUsed = true;
		rollButtonText.text = "roll";
	}
	int currentIndex = 0;

	IEnumerator randomDice()
	{
		for(int k=0;k<9;k++)
		{
			spinDice(currentIndex);
			yield return new WaitForSeconds(0.1f);
		}
		yield return new WaitForSeconds(0.5f);
		Result();
	}
	void Result()
	{
		diceValues[currentIndex] = currentSpriteSpin;
		if(diceRollDelegate != null)
			diceRollDelegate(diceValues);
		rollButton.enabled = true;
		//panel.gameObject.SetActive(false);
		//diceRollDelegate = null;
	}
	int min = 0;
	int max = 3;

	int currentSpriteSpin;

	//fake, spin the dice image
	void spinDice(int index)
	{
		diceImages[index].sprite = diceSprites[++currentSpriteSpin];
		if(currentSpriteSpin==max)
			currentSpriteSpin = min;
	}
	DiceRollDelegate diceRollDelegate;
}