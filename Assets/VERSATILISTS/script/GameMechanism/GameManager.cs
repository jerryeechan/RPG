using UnityEngine;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {
	
	public Character currentCh;
	public List<Character> chs;
	public List<Character> enemies;
	public InfoManager bag;
	public EnemyWave[] enemyWaves;
	Transform saveTransform;

	public bool testMode = true;
	public static float playerAnimationSpeed = 0.5f;
	void Start()
	{
		//testMode = false;
		
		StartGame();
		if(testMode)
		{
			RandomBattleRound.instance.StartBattle(MonsterDataEditor.instance.getMonsterSet("test"));
		}
		else
		{
			DungeonMode();
		}
		Cursor.visible = true;
	}
	public void StartGame()
	{
		if(saveTransform)
			Destroy(saveTransform.gameObject);
		saveTransform = new GameObject("Save").transform;

		CharacterManager.instance.StartGame();
		loadCharacter();
		//
		DungeonManager.instance.newDungeon();
		
		//DungeonMapKeyMode();
		//DungeonMode();
	}
	void loadCharacter()
	{
		//create player characters
		chs = new List<Character>();
		List<CharacterData> chDataList = DataManager.instance.playerData[0].chData;
		for(int i=0;i<chDataList.Count;i++)
		{
			CharacterData chData = chDataList[i];
			Character newPlayer = chData.genCharacter();
			chs.Add(newPlayer);
			newPlayer.chUI = DungeonPlayerStateUI.instance.chUIs[i];
			newPlayer.transform.SetParent(saveTransform); 
			
		} 
		selectCh(chs[0]);
		

		//update stat ui
		//CharacterStatUIManager.instance.viewCharacter(GameManager.instance.currentCh);
		
	}
	
	

	void selectCh(Character ch)
	{
		currentCh = ch;
	}
	public void LockMode()
	{
		keymode = KeyMode.Lock;
	}
	public void DungeonMapKeyMode()
	{
		keymode = KeyMode.Dungeon;
		
	}
	public void DungeonMode()
	{
		UIManager.instance.getPanel("dungeonMap").gameObject.SetActive(true);
		DungeonPlayerStateUI.instance.DungeonMode();
		DungeonMapKeyMode();
		CursorManager.instance.NormalMode();
	}
	public void DungeonOptionKeyMode()
	{
		keymode = KeyMode.DungeonSelect;
	}
	public void CombatKeyMode()
	{
		keymode = KeyMode.Combat;
	}
	public KeyMode keymode = KeyMode.Dungeon;
	//KeyCode[] keylist = {KeyCode.A,KeyCode.D,KeyCode.W,KeyCode.S};
	KeyCode[] keylist = {KeyCode.LeftArrow,KeyCode.RightArrow,KeyCode.UpArrow,KeyCode.DownArrow,KeyCode.Z,KeyCode.X};
	void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
		{
			
			//bag.gameObject.SetActive(isMenuActive);
			if(!bag.isOpen)
			{
				bag.Show();
			}
			else
				bag.Hide();
			
		}

//		Transform chRenTransform = currentCh.chRenderer.transform;
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

public enum KeyMode{Lock,Combat,Dungeon,DungeonSelect};