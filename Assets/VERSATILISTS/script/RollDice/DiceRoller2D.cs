using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DiceRoller2D : Singleton<DiceRoller2D> {

	// Use this for initialization
	public Sprite[] diceSprites;
	public Image[] diceImages;
	int [] diceValues;

	public AnimatableCanvas panel;
	void Awake()
	{
		diceValues = new int[diceImages.Length];
		//panel = GetComponentInChildren<AnimatableCanvas>();
	}
	public void Roll(DiceRollDelegate d)
	{
		 panel.gameObject.SetActive(true);
		 diceRollDelegate+=d;
		 
		 StartCoroutine("randomDice");
		 
	}

	IEnumerator randomDice()
	{
		for(int k=0;k<10;k++)
		{
			for(int i=0;i<diceImages.Length;i++)
			{
				RollDice(i);
			}
		 
			yield return new WaitForSeconds(0.1f);
		}
		yield return new WaitForSeconds(0.5f);
		Result();
		
	}
	void Result()
	{
		//ActionUIManager.instance.lockAllSkillBtn();
		int sum = 0;
		//isDiceReady = true;
		for(int i=0;i<diceImages.Length;i++)
		{
		//	ActionUIManager.instance.unlockSkill(diceValues[i]);
			sum+=diceValues[i]+1;
		}
		if(diceRollDelegate != null)
			diceRollDelegate(sum);

		panel.gameObject.SetActive(false);
		diceRollDelegate = null;
	}
	
	int RollDice(int index)
	{
		int r = Random.Range(0,6);
		diceImages[index].sprite = diceSprites[r];
		diceValues[index] = r; 
		return r;
	}
	DiceRollDelegate diceRollDelegate;
}

public delegate void DiceRollDelegate(int sum);