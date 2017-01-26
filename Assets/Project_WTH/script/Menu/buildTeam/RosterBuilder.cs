using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.jerrch.rpg;
using UnityEngine.UI;
using DG.Tweening;
public class RosterBuilder : Singleton<RosterBuilder>{

	// Use this for initialization
	
	const int optionNum = 4;
	const int fullNum = 3;

	
	[SerializeField]
	RosterButton rosterBtnPrefab;
	[SerializeField]
	CharacterOptionPanel chPanel;
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

		
	}
	
	void Start()
	{
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
		rosterBtnClicked(currentRosterButton);
		
	}

	[SerializeField]
	CompositeText chNameText;
	[SerializeField]
	CompositeText chClassText;
	[SerializeField]
	CompositeText classSpecialtyText;
	[SerializeField]
	CompositeText classDescriptionText;

	[SerializeField]
	CompositeText skillName;
	[SerializeField]
	SkillDescription skill_description_panel;

	Skill _skill;
	public Skill bindSkill{
		set{
			_skill = value;
			skillName.text = _skill.name;
			skill_description_panel.bindSkill = _skill;
			
		}
	}

	CharacterData _chData;
	public CharacterData bindChData{
		set{
			_chData = value;
			chClassText.text = _chData.classID;
			chNameText.text = _chData.nickName;
			var classData = ClassesDataManager.instance.getClassData(_chData.classID);
			classSpecialtyText.text = classData.specialty;
			classDescriptionText.text = classData.description; 
			chPanel.setCharacter(_chData);
		}
	}
	
	RosterCheckButton checkBtn;
	public void checkBtnClicked(bool isCheck)
	{
		checkCharaceter(isCheck);
	}
	[SerializeField]
	Image indicator;
	public void rosterBtnClicked(RosterButton btn)
	{
		indicator.rectTransform.anchoredPosition = btn.GetComponent<RectTransform>().anchoredPosition;
		loadRoster(btn);
		checkBtn.isChecked = btn.isChecked;
	}
	void loadRoster(RosterButton btn)
	{
		currentRosterButton = btn;
		bindChData = btn.bindChData;
		bindSkill = SkillManager.instance.getSkills(btn.bindChData.skillDatas)[0];
	}
	RosterButton currentRosterButton;
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
			
			selectData.Add(currentRosterButton.bindChData);
			memberNumText.text = checkedPlayerNum.ToString();
			currentRosterButton.check();
		}
		else
		{
			checkedPlayerNum--;
			selectData.Remove(currentRosterButton.bindChData);
			memberNumText.text = checkedPlayerNum.ToString();
			currentRosterButton.check();
		}
		if(checkedPlayerNum==fullNum)
		{
			Ready();
		}
	}
	[SerializeField]
	RectTransform startPanel;
	public void Ready()
	{
		DataManager.instance.rosterSelected = selectData;
		//GameManager.instance.loadCharacter(selectData);
		startPanel.DOAnchorPos(Vector2.zero,1).SetEase(Ease.OutBack);
		WTHSceneManager.instance.adventureStart();
	}

	public void Back()
	{
		//TODO:Back to menu
	}
}
