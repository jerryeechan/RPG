using UnityEngine;
using System.Collections.Generic;

namespace com.jerrch.rpg
{


	public class GameManager : Singleton<GameManager> {
		
		public Character currentCh;
		public int chCount;
		public List<Character> chs;	
		public List<Character> enemies;
		public InfoManager info;
		public Transform stageTransform;

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
			InfoManager.instance.init();
			switch(testMode)
			{
				case TestMode.Release:
					InfoManager.instance.Hide();
					UIManager.instance.getPanel("mainmenu").show();
				break;
				case TestMode.Combat:
					CombatMode();
					CombatStage stage = CombatStageManager.instance.getTestStage();
					stage.transform.SetParent(stageTransform);
					EnemySet testEnemySet = stage.enemySet;
					RandomBattleRound.instance.StartBattle(testEnemySet);
					
					//DungeonManager.instance.
				break;
				case TestMode.Adventure:
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
			if(stageTransform)
				Destroy(stageTransform.gameObject);
			stageTransform = new GameObject("CombatStageContainer").transform;
			//CharacterManager.instance.StartGame();
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
				ch.transform.SetParent(stageTransform);
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
		
		public void AdventureMode()
		{
			print("Adventure Mode");
			UIManager.instance.getPanel("combat").hide();
			stageTransform.gameObject.SetActive(false);
			InfoManager.instance.Show();
			InfoManager.instance.switchTab(InfoTabType.Adventure);
			PlayerStateUI.instance.AdventureMode();
			CursorManager.instance.NormalMode();
		}
		public void DungeonMode()
		{
		//	UIManager.instance.getPanel("dungeonMap").gameObject.SetActive(true);
			
		}
		public void CombatMode()
		{
			gamemode = GameMode.Combat;
			print("Combat Mode");
			stageTransform.gameObject.SetActive(true);
			UIManager.instance.getPanel("combat").show();
			PlayerStateUI.instance.CombatMode();
			InfoManager.instance.Hide();
			
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
						info.Show();
					}
					else
					{
						info.Hide();
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
			
		}
	}
	public enum GameMode{Combat,Bag,ActionTree,Adventure};
public enum TestMode{Release,Combat,Adventure,ActionTree,Stat};
}
