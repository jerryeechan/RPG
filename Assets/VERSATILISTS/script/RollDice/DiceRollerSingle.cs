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
	void Awake()
	{
		diceValues = new int[diceImages.Length];
		rollButtonText = rollButton.GetComponentInChildren<CompositeText>();
		//panel = GetComponentInChildren<AnimatableCanvas>();
	}
	
	public void Roll(DiceRollDelegate d)
	{
		 diceRollDelegate+=d;
		 Roll();
	}
	public void Roll()
	{
		 isActionUsed =  false;
		 //panel.gameObject.SetActive(true);
		 indicator.GetComponent<RectTransform>().SetParent(diceImages[currentIndex].transform);
		 indicator.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		 diceImages[currentIndex].color = Color.white;
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
		for(int k=0;k<10;k++)
		{
			RollDice(currentIndex);
//			print("dice roll");
			yield return new WaitForSeconds(0.1f);
		}
		yield return new WaitForSeconds(0.5f);
		Result();
	}
	void Result()
	{
		//ActionUIManager.instance.lockAllSkillBtn();
//		int sum = 0;
		//isDiceReady = true;
		
		//for(int i=0;i<diceImages.Length;i++)
		//{
		//	sum+=diceValues[i]+1;
		//}
		if(diceRollDelegate != null)
			diceRollDelegate(diceValues);
		rollButton.enabled = true;
		//panel.gameObject.SetActive(false);
		//diceRollDelegate = null;
	}
	int min = 0;
	int max = 1;
	int RollDice(int index)
	{
		
		int r = Random.Range(min,max);
		diceImages[index].sprite = diceSprites[r];
		diceValues[index] = r; 
		
		return r;
	}
	DiceRollDelegate diceRollDelegate;
}