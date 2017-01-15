using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.jerrch.rpg;
public class ChUIManager : MonoBehaviour {

	[SerializeField]
	protected HealthBarUI health;
	[SerializeField]
	protected ExpBarUI exp;
	[SerializeField]
	protected Image headIcon;
	[SerializeField]
	protected CompositeText level;
	[SerializeField]
	protected CompositeText chName;
	protected virtual void Awake()
	{
		
	}
	public Character selectedCh;
	public void setCharacter()
	{
		setCharacter(selectedCh);
	}
	public virtual void setCharacter(Character ch)
	{
		selectedCh = ch;
		health.fullValue = ch.battleStat.hp.finalValue;
		health.currentValue = ch.battleStat.hp.currentValue;
		exp.bindData = ch.bindData;
		exp.currentValue = ch.bindData.exp;
		exp.levelText = level;

		if(headIcon != null)
		{
			headIcon.sprite = ch.getEquip(EquipType.Helmet).bindGraphic.iconSprite;
		}
		if(chName != null)
		{
			chName.text = ch.bindData.nickName;
		}
		if(level != null)
		{
			level.text = ch.bindData.level.ToString();
		}

		
	}
	
}
