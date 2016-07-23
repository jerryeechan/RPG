using UnityEngine;
using System.Collections;

public class ActionUIManager : Singleton<ActionUIManager> {

	public ActionButton[] actionBtns;
	void Awake()
	{
		actionBtns = GetComponentsInChildren<ActionButton>();
	}
	public void unlockSkill(int index)
	{
		print(index);
		actionBtns[index].Unlock();
	}
	//use Skill
	public void acionBtnTouched(int index)
	{
		//lockAllSkillBtn();
		//RandomBattleRound.instance.currentPlayer.useAction(index);
		if(selectedActionIndex != -1)
			actionBtns[selectedActionIndex].deSelectButton();

		actionBtns[index].selectButton();
		
		selectedActionIndex = index;
		print("action btn touched:"+index);
	}
	public void useAction()
	{
		print("use action");
		RandomBattleRound.instance.currentPlayer.useAction(selectedActionIndex);
		CombatUIManager.instance.UseActionDone();
	}

	int selectedActionIndex = -1;
	public void lockAllSkillBtn()
	{
		for(int i=0;i<skillNum;i++)
		{
			actionBtns[i].Lock();
		}
	}
	static int skillNum = 6;
	public void setCharacter(Character ch)
	{
		for(int i=0;i<skillNum;i++)
		{
			actionBtns[i].skillNameText.text = ch.actionData[i].name;
		}
	}
}