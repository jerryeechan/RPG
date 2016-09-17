using UnityEngine;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {
	
	public Character currentCh;
	public int chCount;
	public List<Character> chs;	
	public List<Character> enemies;
	public InfoManager info;
	Transform saveTransform;

	public TestMode testMode;
	public GameObject Canvas;
	public static float playerAnimationSpeed = 0.5f;
	void Awake()
	{
		Canvas.SetActive(true);
	}
	void Start()
	{
		
		// DataManager.instance.newPlayerData();
		StartGame();
		
		switch(testMode)
		{
			case TestMode.Release:
				InfoManager.instance.Hide();
				UIManager.instance.getPanel("mainmenu").show();
			break;
			case TestMode.Battle:
			
				BattleMode();
				InfoManager.instance.Hide();
				RandomBattleRound.instance.StartBattle(MonsterDataEditor.instance.getMonsterSet());
				//DungeonManager.instance.
			break;
			case TestMode.ActionTree:
			break;
			case TestMode.Stat:
			break;
		}
		//testMode = false;
		/*
		StartGame();
		if(startWithBattle)
		{
			RandomBattleRound.instance.StartBattle(MonsterDataEditor.instance.getMonsterSet("test"));
		}
		else
		{
			DungeonMode();
			DungeonManager.instance.newDungeon();
		}
		Cursor.visible = true;
		*/
	}
	public void StartNewDungeon()
	{
		DataManager.instance.newPlayerData();
		StartGame();
		DungeonMode();
		
		UIManager.instance.ShowCover(()=>{
			UIManager.instance.getPanel("mainmenu").hide();
			UIManager.instance.HideCover();
		});
	}
	public void StartGame()
	{
		if(saveTransform)
			Destroy(saveTransform.gameObject);
		saveTransform = new GameObject("Save").transform;

		CharacterManager.instance.StartGame();
		loadCharacter();
		//DungeonMapKeyMode();
		//DungeonMode();

	}
	void loadCharacter()
	{
		//create player characters
		chs = CharacterManager.instance.loadPlayerCharacter();
		chCount = chs.Count;
		foreach(var ch in chs)
		{
			ch.transform.SetParent(saveTransform);
		}
		currentCh = chs[0];
		
		/*
		if(InfoManager.instance)
			InfoManager.instance.init();
		
		if(ActionTree.instance)
			ActionTree.instance.setCharacter(currentCh);
		*/

		//update stat ui
		//CharacterStatUIManager.instance.viewCharacter(GameManager.instance.currentCh);
		
	}
	
	

	
	public void LockMode()
	{
		gamemode = GameMode.Lock;
	}
	public void DungeonMapKeyMode()
	{
		
		
	}
	public void BagMode(){

		gamemode = GameMode.Bag;
	}
	
	public void DungeonMode()
	{
	//	UIManager.instance.getPanel("dungeonMap").gameObject.SetActive(true);
		DungeonPlayerStateUI.instance.DungeonMode();
		DungeonMapKeyMode();
		CursorManager.instance.NormalMode();
	}
	public void BattleMode()
	{
	
		UIManager.instance.getPanel("combat").gameObject.SetActive(true);
		DungeonPlayerStateUI.instance.CombatMode();
	}
	public void DungeonOptionKeyMode()
	{
		
	}
	public void CombatKeyMode()
	{
		gamemode = GameMode.Combat;
	}
	GameMode lastMode;
	GameMode _gameMode;
	public GameMode gamemode{
		set{
			if(lastMode != _gameMode)
			{lastMode = _gameMode;}
			_gameMode = value;
		}
		get{
			return _gameMode;
		}
	}
	public void SwitchBackGameMode()
	{
		gamemode = lastMode;
	}

	public void characterDied(Character ch)
	{
		chCount --;
		if(chCount == 0)
		{
			GameOver();
		}
	}
	public void GameOver()
	{
		
	}
	//KeyCode[] keylist = {KeyCode.A,KeyCode.D,KeyCode.W,KeyCode.S};
	KeyCode[] keylist = {KeyCode.LeftArrow,KeyCode.RightArrow,KeyCode.UpArrow,KeyCode.DownArrow,KeyCode.Z,KeyCode.X};
	void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
		{
			
			if(gamemode == GameMode.Combat)
			{

			}
			else{
				
				//bag.gameObject.SetActive(isMenuActive);
				if(!info.isOpen)
				{
					BagMode();
					info.Show();
				}
				else
				{
					info.Hide();
					DungeonMapKeyMode();
				}
			}
			
				
			
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
				switch(gamemode)
				{
					
				}
			}
			if(Input.GetKeyDown(key))
			{
				switch(gamemode)
				{
					
				}
			}
		}
    }
}

public enum GameMode{Lock,Combat,Bag,ActionTree};
public enum TestMode{Release,Battle,Dungeon,ActionTree,Stat};