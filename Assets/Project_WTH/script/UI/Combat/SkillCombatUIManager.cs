using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using com.jerrch.rpg;
public class SkillCombatUIManager : Singleton<SkillCombatUIManager> {

	public SkillCombatButton[] skillBtns;
	public SkillDetailPanel detailPanel;
	public SkillButton draggingSkillBtn;
	//RectTransform selectMask;
	void Awake()
	{
		skillBtns = GetComponentsInChildren<SkillCombatButton>(false);
	//	selectMask = transform.Find("selectMask").GetComponent<RectTransform>();
		draggingSkillBtn.gameObject.SetActive(false);
	}
	void Start()
	{
		foreach(var btn in skillBtns)
		{
			btn.bindSkill = null;
			btn.Lock();
		}
	}
	public void unlockSkill(int index)
	{
//		print(index);
		skillBtns[index].Unlock();
	}

	int selectedIndex;
	public SkillCombatButton selectedBnt{
		get{
			return skillBtns[selectedIndex];
		}
	}
	//use Skill
	public void skillBtnTouched(int index)
	{
		selectedIndex = index;
		
		/*
		if(selectedSkillIndex == -1)
		{
			selectMask.gameObject.SetActive(true);
			selectMask.GetComponent<Image>().DOFade(1,0);
			//first time
			selectMask.SetParent(skillBtns[index].transform);
			selectMask.anchoredPosition = Vector2.zero;
			selectMask.GetComponent<Image>().DOFade(0,0.5f).SetLoops(-1,LoopType.Yoyo);
		}
		else
		{
			selectMask.SetParent(skillBtns[index].transform);
			selectMask.DOAnchorPos(Vector2.zero,0.2f,true);
		}

		skillBtns[index].selectButton();
		*/
		//selectedSkillIndex = index;
		//print("skill btn touched:"+index);

		//RandomBattleRound.instance.selectSkill(index);
	}

	int skillNum;

	
	public void setCharacter(Character ch)
	{
		isReadyToDoSkill = true;
		skillNum = ch.skillList.Count;
		//print(skillNum);
		for(int i=0;i<skillNum;i++)
		{
			bool btnEnable = skillBtns[i].setSkill(i,ch,ch.skillList[i]);
			if(btnEnable)
			{
				skillBtnTouched(i);
			}
			if(selectedSkillIndex==i&&btnEnable == false)
			{
				selectedSkillIndex = -1;
				//selectMask.gameObject.SetActive(false);
			}
		}
		for(int i=skillNum;i<SkillManager.skill_max_num;i++)
		{
			skillBtns[i].disableButton();
		}
		//ch.chRenderer.selectByUI();
	}

	/*
	public void useSkill()
	{
		print("use Skill");
		if(isReadyToDoSkill)
		{
			print("skill readly");
			if(selectedSkillIndex!=-1 && skillBtns[selectedSkillIndex].isEnable)
			{
				//if(EnergySlotUIManager.instance.occupyTest(skillBtns[selectedSkillIndex].bindSkill.energyCost))
				skillBtns[selectedSkillIndex].useSkill();
				lockAllSkillBtn();
			}
			else{

			}
		}
	}
	*/


	public bool isReadyToDoSkill = false;
	int selectedSkillIndex = -1;
	public void lockAllSkillBtn()
	{
		isReadyToDoSkill = false;
		for(int i=0;i<skillNum;i++)
		{
			skillBtns[i].Lock();
		}
	}
	
	public bool isDraggingSkill = false;

	/*
	public void OnBeginDrag(PointerEventData eventdata, Skill skill,Vector2 ori_pos)
	{
		print("OnBeginDrag");
		isDraggingSkill = true; 
		draggingSkillBtn.GetComponent<RectTransform>().anchoredPosition = ori_pos + Vector2.up*20;
		draggingSkillBtn.bindSkill = skill;
		draggingSkillBtn.gameObject.SetActive(true);
	}
	
	public void OnDrag(PointerEventData eventdata)
	{
		draggingSkillBtn.GetComponent<RectTransform>().anchoredPosition += eventdata.delta/UIManager.instance.canvas.scaleFactor;
	}
	public void OnEndDrag()
	{
		isDraggingSkill = false;
		draggingSkillBtn.gameObject.SetActive(false);
	}*/
	/*
	public bool testDrop()
	{
		print("Target character"+targetCharacter);
		isDraggingSkill = false;
		draggingSkillBtn.gameObject.SetActive(false);
		if(targetCharacter)
			return true;
		return false;
	}*/
	public Character targetCharacter = null;

	/*
	public void OverCharacter(Character ch)
	{
		if(isDraggingSkill)
		{
			targetCharacter = ch;
		}
	}*/

}