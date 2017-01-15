using System;
using System.Collections;
using System.Collections.Generic;
using com.jerrch.rpg;
using UnityEngine;

public class ActionBuildUIManger : ActionBaseUIManager,IinspectPlayerable{

	[SerializeField]
	CompositeText spText;

	[SerializeField]
	HandButton[] levelBtns;

   public void levelUpBtnTouched(int index)
   {
	   var curCh = GameManager.instance.currentCh;
	   if(curCh.bindData.skillPoints>0)
	   {
		    curCh.bindData.skillPoints--;
			spText.text = curCh.bindData.skillPoints.ToString();
			curCh.actionList[index].level++;
	   }
	   
	   
   }

   public override void inspectCharacter(Character ch)
   {
	   base.inspectCharacter(ch);
	   spText.text = ch.bindData.skillPoints.ToString();

   }
   
	
}
