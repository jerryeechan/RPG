using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using com.jerrch.rpg;
using UnityEngine.UI;
using System;

public class DiceSkillSlot : MonoBehaviour{
	public SkillDiceType diceType;
	public SkillPlayUnit skillUnit;

	[SerializeField]
	Image skillIcon;
	
	void Awake(){
		skillUnit = new SkillPlayUnit();
	}
	public void clear()
	{
		GetComponentInChildren<CanvasGroup>().alpha = 0;
		skillIcon.sprite = SpriteManager.instance.emptySprite;
		skillUnit.maximum = 1;
	}
	
	public void setSkill(Skill skill)
	{
		print(""+skill.diceType+diceType);
		if(skill.diceType == diceType)
		{
			GetComponentInChildren<CanvasGroup>().alpha = 1;
			skillIcon.sprite = skill.icon;
			skillUnit.AddSkill(skill.caster,skill);
			SoundEffectManager.instance.playSound(BasicSound.Drop);
		}
	}
	public void setSkill()
	{
		Skill dropSkill = SkillUIManager.instance.selectedBnt.bindSkill;
		print(""+dropSkill.diceType+diceType);
		if(dropSkill.diceType == diceType)
		{
			
			GetComponentInChildren<CanvasGroup>().alpha = 1;
			skillIcon.sprite = dropSkill.icon;
			skillUnit.AddSkill(TurnBattleManager.instance.selectedPlayer,dropSkill);
			//TurnBattleManager.instance.SkillReady();

			SoundEffectManager.instance.playSound(BasicSound.Drop);
		}
	}

    


    // Use this for initialization

}