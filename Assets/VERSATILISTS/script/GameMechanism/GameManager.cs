using UnityEngine;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {

	public Character currentCh;
	public List<Character> chs;
	public List<Character> enemies;
	public Transform menu;
	public EnemyWave[] enemyWaves;
	Transform saveTransform;
	void Start()
	{
		
		StartGame();
	}
	public void StartGame()
	{
		if(saveTransform)
			Destroy(saveTransform.gameObject);
		saveTransform = new GameObject("Save").transform;

		CharacterManager.instance.StartGame();
		loadCharacter();
		loadEnemy(enemyWaves[0]);
		loadItem();

		RandomBattleRound.instance.StartGame();
	}
	void loadCharacter()
	{
		//create player characters
		chs = new List<Character>();
		foreach(CharacterData chData in DataManager.instance.playerData[0].chData)
		{
			Character newPlayer = chData.genCharacter();
			newPlayer.transform.SetParent(saveTransform); 
			chs.Add(newPlayer);
		} 
		selectCh(chs[0]);
		

		//update stat ui
		CharacterStatUIManager.instance.viewCharacter(GameManager.instance.currentCh);
	}
	void loadEnemy(EnemyWave wave)
	{
		foreach(var preset in wave.enemyPreset)
		{

			Character newCh = preset.chData.genCharacter();
			newCh.transform.SetParent(saveTransform); 
			enemies.Add(newCh);
			newCh.side = Character.CharacterSide.Enemy;
		} 
	}
	void loadItem()
	{
		int i=0;
		foreach(var itemData in DataManager.instance.curPlayerData.itemDataList)
		{
			Item item = ItemManager.instance.getItem(itemData);
			item.transform.SetParent(saveTransform);
			ItemUIManager.instance.setItem(item,i);
			i++;
		}
	}

	void selectCh(Character ch)
	{
		currentCh = ch;
	}

	public void DungeonMapMode()
	{
		keymode = KeyMode.Dungeon;
	}
	public void DungeonOptionMode()
	{
		keymode = KeyMode.DungeonSelect;
	}
	public void CombatMode()
	{
		keymode = KeyMode.Combat;
	}
	bool isMenuActive = true;
	public KeyMode keymode = KeyMode.Dungeon;
	//KeyCode[] keylist = {KeyCode.A,KeyCode.D,KeyCode.W,KeyCode.S};
	KeyCode[] keylist = {KeyCode.LeftArrow,KeyCode.RightArrow,KeyCode.UpArrow,KeyCode.DownArrow,KeyCode.Z,KeyCode.X};
	void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
		{
			isMenuActive = !isMenuActive;
			menu.gameObject.SetActive(isMenuActive);
		}

		Transform chRenTransform = currentCh.chRenderer.transform;
		/*
        if(Input.GetKey(KeyCode.LeftArrow))
		{
			chRenTransform.transform.Translate(Vector3.left/2);
			chRenTransform.localScale = new Vector3(-1,1,1);
		}
		else if(Input.GetKey(KeyCode.RightArrow))
		{
			chRenTransform.Translate(Vector3.right/2);
			chRenTransform.localScale = new Vector3(1,1,1);
		}
		
		if(Input.GetKey(KeyCode.UpArrow))
		{
			chRenTransform.Translate(Vector3.up/4);
		}        
		else if(Input.GetKey(KeyCode.DownArrow))
		{
			chRenTransform.Translate(Vector3.down/4);
		}*/
		foreach(var key in keylist)
		{
			if(Input.GetKey(key))
			{
				switch(keymode)
				{
					case KeyMode.Dungeon:
						DungeonManager.instance.keyPress(key);
					break;
				}
			}
			if(Input.GetKeyDown(key))
			{
				switch(keymode)
				{
					case KeyMode.Dungeon:
						DungeonManager.instance.keyDown(key);
					break;
					case KeyMode.DungeonSelect:
						DungeonOptionSelector.instance.keydown(key);
					break;
				}
			}
		}
    }
}

public enum KeyMode{Combat,Dungeon,DungeonSelect};