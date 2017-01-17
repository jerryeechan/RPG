using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MenuManager : MonoBehaviour {

	public static MenuManager instance;
	public enum Panel{Battle,Skill,Weapon,Perk};
	//public GameObject StartPanel;
	public GameObject MainPanel;
	
	public GameObject BattleStagePanel;
	public GameObject SkillPanel;
	public GameObject WeaponPanel;
	public GameObject PerkPanel;
	
	GameObject nowPanel;
	Dictionary<Panel,GameObject> panelDic;
	Dictionary<string,Panel> panelNameDic;
	
	public List<MenuButton> menuButtons;
	
	public List<GameObject> panels;
	void Awake()
	{
		if(instance!=null)
		{
			throw new UnityException("double singleton");
		}
		instance = this;
		
		nowPanel = BattleStagePanel;
		menuButtons[0].Select();
		
		buildPanelDic();
	}
	void buildPanelDic()
	{
		panelDic = new Dictionary<Panel, GameObject>();
		//panelDic.Add(Panel.Start,StartPanel);
		//panelDic.Add(Panel.Main,MainPanel);
		panelDic.Add(Panel.Battle,BattleStagePanel);
		panelDic.Add(Panel.Skill,SkillPanel);
		panelDic.Add(Panel.Weapon,WeaponPanel);
		panelDic.Add(Panel.Perk,PerkPanel);
		
		panelNameDic = new Dictionary<string, Panel>();
		//panelNameDic.Add("Start",StartPanel);
		//panelNameDic.Add("Main",MainPanel);
		panelNameDic.Add("Battle",Panel.Battle);
		panelNameDic.Add("Skill",Panel.Skill);
		panelNameDic.Add("Weapon",Panel.Weapon);
		panelNameDic.Add("Perk",Panel.Perk);
		
	}

	public void SwitchTo(Panel panel)
	{
		nowPanel.SetActive(false);
		
		GameObject toPanel;
		panelDic.TryGetValue(panel,out toPanel);
		toPanel.SetActive(true);
		
		nowPanel = toPanel;
		
		for(int i=0;i<menuButtons.Count;i++)
		{
			if(menuButtons[i].panel!=panel)
			menuButtons[i].deSelect();
			else
				menuButtons[i].Select();
		}
	}
	
	public void SwitchTo(string panelName)
	{
		//nowPanel.SetActive(false);
		
		Panel panel;
		panelNameDic.TryGetValue(panelName,out panel);
		SwitchTo(panel);
	}
}
