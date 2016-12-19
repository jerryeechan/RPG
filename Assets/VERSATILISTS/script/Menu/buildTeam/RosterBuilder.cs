using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.jerrch.rpg;
public class RosterBuilder : Singleton<RosterBuilder>{

	// Use this for initialization
	
	const int optionNum = 4;
	const int fullNum = 3;

	
	[SerializeField]
	RosterButton rosterBtnPrefab;
	List<CharacterData> selectData;
	
	public CompositeText memberNumText;

	List<RosterButton> rosterButtons;

	Transform rosterList;
	void Awake()
	{
		checkBtn = GetComponentInChildren<RosterCheckButton>();
		selectData = new List<CharacterData>();
		rosterButtons = new List<RosterButton>();
		rosterList = transform.Find("rosterList");
		//generate ch option panels;
		int t = optionNum/2;
		for(int i=-t;i<t+1;i++)
		{
			RosterButton btn = Instantiate(rosterBtnPrefab);
			btn.transform.SetParent(rosterList);
			btn.GetComponent<RectTransform>().anchoredPosition = new Vector2(i*32,0);
			btn.transform.localScale = Vector3.one;
			rosterButtons.Add(btn);
			btn.bindChData = ClassesDataManager.instance.generateChData();	
		}
		currentRosterButton = rosterButtons[0];
	}

	[SerializeField]
	CompositeText chNameText;
	[SerializeField]
	CompositeText chClassText;
	[SerializeField]
	CompositeText chSpecialtyText;
	[SerializeField]
	CompositeText actionName;
	[SerializeField]
	ActionButton action_description_panel;

	Action _action;
	public Action bindAction{
		set{
			_action = value;
			actionName.text = _action.name;
			action_description_panel.bindAction = _action;
		}
	}

	CharacterData _chData;
	public CharacterData bindChData{
		set{
			_chData = value;
			chClassText.text = _chData.classID;
			chNameText.text = _chData.name;
		}
	}
	
	RosterCheckButton checkBtn;
	public void checkBtnClicked(bool isCheck)
	{
		checkCharaceter(isCheck);
	}
	public void rosterBtnClicked(RosterButton btn)
	{
		loadRoster(btn);
		checkBtn.isChecked = btn.isChecked;
	}
	void loadRoster(RosterButton btn)
	{
		currentRosterButton = btn;
		bindChData = btn.bindChData;
		bindAction = ActionManager.instance.getActions(btn.bindChData.actionIDs)[0];
	}
	RosterButton currentRosterButton;
	int currentIndex = 0;
	int checkedPlayerNum = 0;
	public bool isTeamFull
	{
		get{
			return checkedPlayerNum>=fullNum;
		}
	}
	public void checkCharaceter(bool isCheck)
	{
		
	
		if(isCheck)
		{
			checkedPlayerNum++;
			selectData.Add(rosterButtons[currentIndex].bindChData);
			memberNumText.text = checkedPlayerNum.ToString();
			currentRosterButton.check();
		}
		else
		{
			checkedPlayerNum--;
			selectData.Remove(rosterButtons[currentIndex].bindChData);
			memberNumText.text = checkedPlayerNum.ToString();
			currentRosterButton.check();
		}
		if(checkedPlayerNum==fullNum)
		{
			Ready();
		}
		
		
	}
	public void Ready()
	{
		GameManager.instance.loadCharacter(selectData);
		print("ready");
	}

	public void Back()
	{
		//TODO:Back to menu
	}
}
