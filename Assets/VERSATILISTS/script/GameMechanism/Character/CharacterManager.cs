using UnityEngine;
using System.Collections.Generic;

public class CharacterManager :Singleton<CharacterManager> {

	public Character chTemplate;
	public Dictionary<string,Character> chDict;
	public Dictionary<string,CharacterRenderer> chUITemplatesDict;
	public List<CharacterRenderer> chUITemplates;
	void Awake()
	{
		chUITemplatesDict = new Dictionary<string,CharacterRenderer>();
		foreach(CharacterRenderer ui in chUITemplates)
		{
//			print(ui.name);
			chUITemplatesDict.Add(ui.name,ui);
		}
	}
	public void StartGame()
	{
		chDict = new Dictionary<string,Character>();
	}
	public Character generateCharacter(string name,string UITemplateID)
	{
		Character ch = GameObject.Instantiate(CharacterManager.instance.chTemplate);

		CharacterRenderer chRend;
		if(UITemplateID!="")
		{
			//print(UITemplateID);
			chRend = Instantiate(chUITemplatesDict[UITemplateID]);
			
		}
		else
		{
			chRend = Instantiate(chUITemplatesDict["empty"]);
		}
		chRend.transform.SetParent(ch.transform);
			chRend.bindCh = ch;
			ch.chRenderer = chRend;
		
		ch.name = name;
		chDict.Add(name,ch);
		return ch;
	}
	
	

	
}