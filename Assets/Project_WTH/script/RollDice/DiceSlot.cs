using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DiceSlot : MonoBehaviour,IPointerClickHandler {
	Image image;
	public Sprite[] diceSprites;
	SkillDiceType[] _faceTypes;
	public int index;

	public SkillDiceType[] faceTypes
	{
		set{
			_faceTypes = value;
		}

	}


	[HideInInspector]
	public DiceSkillSlot skillSlot;
	
	void Awake()
	{
		image = GetComponent<Image>();
		skillSlot = GetComponentInChildren<DiceSkillSlot>();
	}


	public void clear()
	{
		skillSlot.clear();
		_belongsTo = DiceBelongsTo.Unknown;
	}

	DiceTypeResultDelegate resultCallback;
	public void Roll(int num,DiceTypeResultDelegate callback)
	{
		int firstRandom = Random.Range(0,8);
		if(firstRandom>3)
			firstRandom = 0;
		currentSpriteSpin = 0;//firstRandom;//Random.Range(0,3);
		//currentSpriteSpin = 0; //Fake	
		resultCallback = callback;
		rollNum = num;
		StartCoroutine("diceAnimation");
	}
	DiceBelongsTo _belongsTo = DiceBelongsTo.Unknown;
	public DiceBelongsTo belongsTo
	{
		get{return _belongsTo;}
	}
	
	public bool isPlayerDice
	{
		get{return (belongsTo == DiceBelongsTo.Player);}
	}
	Sprite enemySprite = null;

	//change to enemy???
	public void willHaveSpecialResult(Sprite sprite)
	{
		enemySprite = sprite;
		_belongsTo = DiceBelongsTo.Enemy;
		skillSlot.skillUnit.maximum = 10;
	}

	int rollNum;
	int min = 0;
	int max = 3;

	int currentSpriteSpin = 0;
	IEnumerator diceAnimation()
	{
		
		for(int k=0;k<rollNum;k++)
		{
			spinDice();
			yield return new WaitForSeconds(0.05f);		
		}
		Result();
	}
	
	Image img1;
	Image img2;
	Image currentImg;
	//fake, spin the dice image

	void barSlide()
	{
		currentImg.sprite = diceSprites[currentSpriteSpin];
		//img1.rectTransform.anchoredPosition.y+=10
	}
	void spinDice()
	{
		currentSpriteSpin++;
		if(currentSpriteSpin>=max)
			currentSpriteSpin = min;
		image.sprite = diceSprites[currentSpriteSpin];
	}
	void Result()
	{
		if(_belongsTo == DiceBelongsTo.Enemy)
		{
			image.sprite = enemySprite;
			enemySprite = null;
			resultCallback(SkillDiceType.Enemy);
		}
		else
		{
			_belongsTo = DiceBelongsTo.Player;
			skillSlot.diceType = _faceTypes[currentSpriteSpin];
			resultCallback((SkillDiceType)currentSpriteSpin);
		}
	}
	void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
		TurnBattleManager.instance.selectSlot(index);
        SoundEffectManager.instance.playSound(BasicSound.Drop);
    }
}

public enum DiceBelongsTo{Unknown,Player,Enemy}