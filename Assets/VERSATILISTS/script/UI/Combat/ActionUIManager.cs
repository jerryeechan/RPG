using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using com.jerrch.rpg;
public class ActionUIManager : Singleton<ActionUIManager> {

	public ActionCombatButton[] actionBtns;
	public ActionDetailPanel detailPanel;
	public ActionButton draggingActionBtn;
	RectTransform selectMask;
	void Awake()
	{
		actionBtns = GetComponentsInChildren<ActionCombatButton>(false);
		selectMask = transform.Find("selectMask").GetComponent<RectTransform>();
		draggingActionBtn.gameObject.SetActive(false);
	}
	void Start()
	{
		foreach(var btn in actionBtns)
		{
			btn.bindAction = null;
			btn.Lock();
		}
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

		/*
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
		*/
		selectedActionIndex = index;
		print("action btn touched:"+index);

		//RandomBattleRound.instance.selectAction(index);
	}

	int actionNum;

	
	public void setCharacter(Character ch)
	{
		isReadyToDoAction = true;
		actionNum = ch.actionList.Count;
		//print(actionNum);
		for(int i=0;i<actionNum;i++)
		{
			bool btnEnable = actionBtns[i].setAction(i,ch,ch.actionList[i]);
			if(btnEnable)
			{
				actionBtnTouched(i);
			}
			if(selectedActionIndex==i&&btnEnable == false)
			{
				selectedActionIndex = -1;
				//selectMask.gameObject.SetActive(false);
			}
		}
		for(int i=actionNum;i<ActionManager.action_max_num;i++)
		{
			actionBtns[i].disableButton();
		}
		ch.chRenderer.selectByUI();
	}

	/*
	public void useAction()
	{
		print("use Action");
		if(isReadyToDoAction)
		{
			print("action readly");
			if(selectedActionIndex!=-1 && actionBtns[selectedActionIndex].isEnable)
			{
				//if(EnergySlotUIManager.instance.occupyTest(actionBtns[selectedActionIndex].bindAction.energyCost))
				actionBtns[selectedActionIndex].useAction();
				lockAllSkillBtn();
			}
			else{

			}
		}
	}
	*/

	public bool isReadyToDoAction = false;
	int selectedActionIndex = -1;
	public void lockAllSkillBtn()
	{
		isReadyToDoAction = false;
		for(int i=0;i<actionNum;i++)
		{
			actionBtns[i].Lock();
		}
	}
	
	public bool isDraggingAction = false;

	
	public void OnBeginDrag(PointerEventData eventdata, Action action,Vector2 ori_pos)
	{
		print("OnBeginDrag");
		isDraggingAction = true; 
		draggingActionBtn.GetComponent<RectTransform>().anchoredPosition = ori_pos + Vector2.up*20;
		draggingActionBtn.bindAction = action;
		draggingActionBtn.gameObject.SetActive(true);
	}
	
	public void OnDrag(PointerEventData eventdata)
	{
		draggingActionBtn.GetComponent<RectTransform>().anchoredPosition += eventdata.delta/UIManager.instance.canvas.scaleFactor;
	}
	public void OnEndDrag()
	{
		isDraggingAction = false;
		draggingActionBtn.gameObject.SetActive(false);
	}
	/*
	public bool testDrop()
	{
		print("Target character"+targetCharacter);
		isDraggingAction = false;
		draggingActionBtn.gameObject.SetActive(false);
		if(targetCharacter)
			return true;
		return false;
	}*/
	public Character targetCharacter = null;

	/*
	public void OverCharacter(Character ch)
	{
		if(isDraggingAction)
		{
			targetCharacter = ch;
		}
	}*/

}