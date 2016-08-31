using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DungeonCharacterUI : MonoBehaviour {

	Image head;
	HealthBarUI healthBar;
	ExpBarUI expBar;
	Character ch;

	void Awake()
	{
		healthBar = GetComponentInChildren<HealthBarUI>();
		expBar = GetComponentInChildren<ExpBarUI>();
	}

	public Character bindCh{
		set{
			ch = value;
			healthBar.init((int)ch.equipStat.hp);
			expBar.init(ch.chData);
		}
	}

	public void updateUI(CharacterStat stat)
	{
		healthBar.healthValue = (int)stat.hp;
	}

	public void getExp(int exp)
	{
		expBar.expValue += exp;
	}

}
