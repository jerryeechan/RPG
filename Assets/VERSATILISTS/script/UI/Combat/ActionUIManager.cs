using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using com.jerrch.rpg;
public class ActionUIManager : Singleton<ActionUIManager> {

	public ActionCombatButton[] actionBtns;
	public ActionDetailPanel detailPanel;
	RectTransform selectMask;
	void Awake()
	{
		actionBtns = GetComponentsInChildren<ActionCombatButton>();
		selectMask = transform.Find("selectMask").GetComponent<RectTransform>();
	}
	void Start()
	{
		
	}
	public void unlockSkill(int index)
	{
//		print(index);
		actionBtns[index].Unlock();
	}
	//use Skill
	public void actionBtnTouched(int index)
	{
		//lockAllSkillBtn();
		//RandomBattleRound.instance.currentPlayer.useAction(index);
		if(selectedActionIndex == -1)
		{
			selectMask.gameObject.SetActive(true);
			selectMask.GetComponent<Image>().DOFade(1,0);
			//first time
			selectMask.SetParent(actionBtns[index].transform);
			selectMask.anchoredPosition = Vector2.zero;
			selectMask.GetComponent<Image>().DOFade(0,0.5f).SetLoops(-1,LoopType.Yoyo);
			
		}
		else
		{
			selectMask.SetParent(actionBtns[index].transform);
			selectMask.DOAnchorPos(Vector2.zero,0.2f,true);
		}

		actionBtns[index].selectButton();
		selectedActionIndex = index;
		print("action btn touched:"+index);

		RandomBattleRound.instance.selectAction(index);
	}

	int actionNum;

	
	public void setCharacter(Character ch)
	{
		print("set ch");	
		isReadyToDoAction = true;
		actionNum = ch.actionList.Count;
		for(int i=0;i<actionNum;i++)
		{
			bool btnEnable = actionBtns[i].setAction(i,ch,ch.actionList[i]);
			if(selectedActionIndex==i&&btnEnable == false)
			{
				selectedActionIndex = -1;
				selectMask.gameObject.SetActive(false);
				
			}
		}
		for(int i=actionNum;i<4;i++)
		{
			actionBtns[i].disableButton();
		}
	}
	

	public void useAction()
	{
		if(isReadyToDoAction)
		{
//			print("use action");
			if(actionBtns[selectedActionIndex].isEnable)
			{
				if(EnergySlotUIManager.instance.occupyTest(actionBtns[selectedActionIndex].bindAction.energyCost))
				{
					actionBtns[selectedActionIndex].useAction();
					
					lockAllSkillBtn();
				}
			}
			else{

			}
			
		}
		
		//CombatUIManager.instance.UseActionDone();
	}

	bool isReadyToDoAction = false;
	int selectedActionIndex = -1;
	public void lockAllSkillBtn()
	{
		isReadyToDoAction = false;
		for(int i=0;i<actionNum;i++)
		{
			actionBtns[i].Lock();
		}
	}
	
}