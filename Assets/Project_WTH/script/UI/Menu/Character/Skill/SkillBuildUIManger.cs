using System;
using System.Collections;
using System.Collections.Generic;
using com.jerrch.rpg;
using UnityEngine;

public class SkillBuildUIManger : SkillBaseUIManager,IinspectPlayerable{

	[SerializeField]
	CompositeText spText;

	[SerializeField]
	List<HandButton> levelBtns;
	

	void Awake()
	{
		skillBtns = GetComponentsInChildren<SkillButton>();
		//levelBtns = new List<HandButton>();
		foreach(var skillBtn in skillBtns)
		{
			levelBtns.Add(skillBtn.transform.parent.Find("levelUp").GetComponent<HandButton>());
		}
	}

   public void levelUpBtnTouched(int index)
   {
	   var curCh = GameManager.instance.currentCh;
	   if(curCh.bindData.skillPoints>0)
	   {
		    curCh.bindData.skillPoints--;
			spText.text = curCh.bindData.skillPoints.ToString();
			curCh.skillList[index].level++;
			DescriptionUIManager.instance.showDescribable(skillBtns[index]);
	   }
   }

   public override void inspectCharacter(Character ch)
   {
	   base.inspectCharacter(ch);
	   spText.text = ch.bindData.skillPoints.ToString();
   }
   
	
}
