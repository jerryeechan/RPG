using UnityEngine;
using System.Collections.Generic;
namespace com.jerrch.rpg{
	public class CharacterManager :Singleton<CharacterManager> {

		public Character chTemplate;
		public Dictionary<string,Character> chDict;
		public Dictionary<string,CharacterRenderer> chUITemplatesDict;
		CharacterRenderer[] chUITemplates;
		public GameObject chRTemplateHolder;
		public Transform[] playerChPositions;
		public Transform[] enemyChPositions;
		
		void Awake()
		{
			chUITemplates = chRTemplateHolder.GetComponentsInChildren<CharacterRenderer>(true);
			chUITemplatesDict = new Dictionary<string,CharacterRenderer>();
			foreach(CharacterRenderer ui in chUITemplates)
			{
				chUITemplatesDict.Add(ui.name,ui);
			}
		}
		public void StartGame()
		{
			//chDict = new Dictionary<string,Character>();
		}

		public List<Character> loadPlayerCharacter(List<CharacterData> chDataList)
		{
			List<Character> chs = new List<Character>();
			int i;
			for(i=0;i<chDataList.Count;i++)
			{
				CharacterData chData = chDataList[i];
				Character newPlayer = chData.genCharacter();
				chs.Add(newPlayer);
				//newPlayer.chUI = PlayerStateUI.instance.chUIs[i];
				newPlayer.chRenderer.transform.position = playerChPositions[i].position;
			}
			/*
			for(;i<3;i++)
			{
				PlayerStateUI.instance.chUIs[i].gameObject.SetActive(false);
			} */
			return chs;
		}
		public List<Character> loadPlayerCharacter()
		{
			//create player characters
			List<Character> chs = new List<Character>();
			List<CharacterData> chDataList = DataManager.instance.playerData[0].chData;
			int i;
			for(i=0;i<chDataList.Count;i++)
			{
				CharacterData chData = chDataList[i];
				Character newPlayer = chData.genCharacter();
				chs.Add(newPlayer);
				//newPlayer.chUI = PlayerStateUI.instance.chUIs[i];
				newPlayer.chRenderer.transform.position = playerChPositions[i].position;
			}
			/*
			for(;i<3;i++)
			{
				PlayerStateUI.instance.chUIs[i].gameObject.SetActive(false);
			} */
			return chs;
			
		}
		public List<Character> loadEnemy(EnemyWave wave)
		{
			
			List<Character> enemies = new List<Character>();
			
			int i=0;
			foreach(var preset in wave.enemyPreset)
			{
				Character newCh = preset.chData.genCharacter();
				newCh.chRenderer.transform.position = enemyChPositions[i].position;	
				enemies.Add(newCh);
				newCh.side = CharacterSide.Enemy;
				i++;
			} 
			return enemies;
		}
		public Character generateCharacter(string name,string UITemplateID)
		{
			Character ch = GameObject.Instantiate(chTemplate);

			CharacterRenderer chRend = null;
			if(UITemplateID!="")
			{
				//generate specific character
				if(!chUITemplatesDict.ContainsKey(UITemplateID))
				{
					Debug.LogError(UITemplateID);
					chRend = Instantiate(chUITemplatesDict["empty"]);
				}
				else
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
			//chDict.Add(name,ch);
			return ch;
		}
		
		

		
	}
}