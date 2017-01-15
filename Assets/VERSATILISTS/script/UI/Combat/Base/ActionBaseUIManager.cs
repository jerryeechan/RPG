using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.jerrch.rpg;
public class ActionBaseUIManager : MonoBehaviour {

	public ActionButton[] actionBtns;
	// Use this for initialization
	public virtual void actionBtnTouched(int index)
	{

	}
	public virtual void inspectCharacter(Character ch)
	{	
		int actionNum = ch.actionList.Count;
		//print(actionNum);
		for(int i=0;i<actionNum;i++)
		{
			bool btnEnable = actionBtns[i].bindAction = ch.actionList[i];
		}
		//ch.chRenderer.selectByUI();
	}
}
