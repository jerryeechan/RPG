using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.jerrch.rpg;
public class SkillBaseUIManager : MonoBehaviour {

	public SkillButton[] skillBtns;
	// Use this for initialization
	public virtual void skillBtnTouched(int index)
	{

	}
	public virtual void inspectCharacter(Character ch)
	{	
		int skillNum = ch.skillList.Count;
		//print(skillNum);
		for(int i=0;i<skillNum;i++)
		{
			bool btnEnable = skillBtns[i].bindSkill = ch.skillList[i];
		}
		//ch.chRenderer.selectByUI();
	}
}
