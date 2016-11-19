using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
namespace com.jerrch.rpg{
public class CharacterUI : MonoBehaviour,IPointerClickHandler {

	Image head;
	HealthBarUI healthBar;
	ExpBarUI expBar;
	Character ch;

	Image indicator;
	void Awake()
	{
		healthBar = GetComponentInChildren<HealthBarUI>();
		expBar = GetComponentInChildren<ExpBarUI>();
		indicator = transform.Find("indicator").GetComponent<Image>();
	}

	public Character bindCh{
		set{
			ch = value;
			healthBar.init((int)ch.equipStat.hp);
			expBar.init(ch.chData);
		}
		get{
			if(!ch)
			{
				Debug.LogError("ch not bind");
				return null;
			}
			else
				return ch;
			
				
		}
	}

	public void updateUI(CharacterStat stat)
	{
		healthBar.init(stat.maxHP);
		healthBar.healthValue = (int)stat.hp;
	}

	public void getExp(int exp)
	{
		expBar.expValue += exp;
	}

	

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.instance.currentCh = bindCh;
		
		switch(GameManager.instance.gamemode)
		{
			case GameMode.Combat:
				ActionUIManager.instance.setCharacter(bindCh);
				bindCh.chRenderer.selectByUI();
				PlayerStateUI.instance.lastUI.ch.chRenderer.deselect();
				
				//show character's action
			break;	
			case GameMode.ActionTree:
				//show character's action slot to change
				ActionTree.instance.setCharacter(bindCh);
			break;
			case GameMode.Bag:
				//show equips and stats
				InfoManager.instance.inspectCharacter(bindCh);
			break;
		}
		PlayerStateUI.instance.lastUI.indicator.enabled = false;
		indicator.enabled = true;
		PlayerStateUI.instance.lastUI = this;
    }
}
}