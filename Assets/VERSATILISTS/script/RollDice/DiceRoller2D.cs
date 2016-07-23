using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DiceRoller2D : Singleton<DiceRoller2D> {

	// Use this for initialization
	public Sprite[] diceSprites;
	public Image[] diceImages;
	int [] diceValues;
	void Awake()
	{
		diceValues = new int[diceImages.Length];
	}
	public void Roll()
	{
		 for(int i=0;i<diceImages.Length;i++)
		 {
			 RollDice(i);
		 }
		 Result();
	}

	void Result()
	{
		CombatUIManager.instance.DiceRollDone();
		ActionUIManager.instance.lockAllSkillBtn();
		//isDiceReady = true;
		for(int i=0;i<diceImages.Length;i++)
		{
			ActionUIManager.instance.unlockSkill(diceValues[i]);
		}
	}

	int RollDice(int index)
	{
		int r = Random.Range(0,6);
		diceImages[index].sprite = diceSprites[r];
		diceValues[index] = r; 
		return r;
	}
}
