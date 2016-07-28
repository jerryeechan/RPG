using UnityEngine;
using System.Collections.Generic;

public class CharacterManager :Singleton<CharacterManager> {

	public Character chTemplate;
	public Dictionary<string,Character> chDict = new Dictionary<string,Character>();
	public Dictionary<string,CharacterRenderer> chUITemplatesDict;
	public List<CharacterRenderer> chUITemplates;
	void Awake()
	{
		chUITemplatesDict = new Dictionary<string,CharacterRenderer>();
		foreach(CharacterRenderer ui in chUITemplates)
		{
			print(ui.name);
			chUITemplatesDict.Add(ui.name,ui);
		}
	}
	public Character generateCharacter(string name,string UITemplateID)
	{
		Character ch = GameObject.Instantiate(CharacterManager.instance.chTemplate);

		if(UITemplateID!="")
		{
			print(UITemplateID);
			CharacterRenderer chRend = GameObject.Instantiate(chUITemplatesDict[UITemplateID]);
			chRend.bindCh = ch;
			ch.chRenderer = chRend;
		}
		
		ch.name = name;
		chDict.Add(name,ch);
		return ch;
	}
	
	

	
}