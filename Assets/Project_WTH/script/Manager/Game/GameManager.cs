using UnityEngine;
using System.Collections.Generic;
using SmartLocalization;
namespace com.jerrch.rpg
{
	public class GameManager : Singleton<GameManager> {
		
		public Character currentCh;
		public int chCount;
		public List<Character> chs;
		public List<Character> enemies;
		public TestMode testMode;
		public AdventureManager adventureManager;
		public StatChUIManager statChUIManager;
		public ShopUIManager shop;
		//public GameObject Canvas;
		public static float playerAnimationSpeed = 0.5f;
		void Awake()
		{

		}
		void Start()
		{
			LocalizationManager.instance.init();
			// DataManager.instance.newPlayerData();
			StartGame();
			//UIManager.instance.getPanel("cover").gameObject.SetActive(true);
			//UIManager.instance.getPanel("cover").hide();
			//testMode = false;
		}
		public void Pause()
		{
			PauseMenuManager.instance.show();
		}

		/*
		public void StartNewDungeon()
		{
			DataManager.instance.newPlayerData();
			StartGame();
			DungeonMode();
			
			PauseMenuManager.instance.Transition((OnCompleteDelegate d)=>{
				UIManager.instance.getPanel("mainmenu").hide();
				UIManager.instance.HideCover();
				d();
			});
		}*/
		public void StartGame()
		{
			//if(stageTransform)
			//	Destroy(stageTransform.gameObject);
			//stageTransform = new GameObject("CombatStageContainer").transform;
			var pData = DataManager.instance.createPlayerData();
			
			pData.chData = DataManager.instance.rosterSelected;
			pData.gold = 90;
			PlayerStateUI.instance.init();

			loadCharacter();
//			InfoManager.instance.init();
			//ShopUIManager.instance.init();//FAKE test shop
			switch(testMode)
			{
				case TestMode.Combat:
					CombatMode();					
					EnemySet testEnemySet = MonsterDataEditor.instance.getMonsterSet("test");
					TurnBattleManager.instance.StartBattle(testEnemySet);					
				break;
				case TestMode.Adventure:
					TurnBattleManager.instance.gameObject.SetActive(false);
					this.myInvoke(1,()=>{
						//InfoManager.instance.switchTab(InfoTabType.Adventure);
					});
					adventureManager.init();
					statChUIManager.init();
					adventureManager.Show();
					
					break;
			}
			shop.init();
		}
		
		//load new characters
		public void loadCharacter(List<CharacterData>chDataList)
		{
			chs = CharacterManager.instance.loadPlayerCharacter(chDataList);
			chCount = chs.Count;
			currentCh = chs[0];
		}


		// load last character
		void loadCharacter()
		{
			//create player characters
			chs = CharacterManager.instance.loadPlayerCharacter();
			chCount = chs.Count;
			currentCh = chs[0];
			
			
			/*
			if(InfoManager.instance)
				InfoManager.instance.init();
			
			if(SkillTree.instance)
				SkillTree.instance.setCharacter(currentCh);
			*/

			//update stat ui
			//CharacterStatUIManager.instance.viewCharacter(GameManager.instance.currentCh);
			
		}
		
		public void AdventureMode()
		{
			print("Adventure Mode");
			UIManager.instance.getPanel("BattleManger").hide();
			//InfoManager.instance.Show();
			//InfoManager.instance.switchTab(InfoTabType.Adventure);
			adventureManager.Show(); 
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
			UIManager.instance.getPanel("BattleManger").show();
			adventureManager.Hide();
			PlayerStateUI.instance.CombatMode();
			//InfoManager.instance.Hide();
			
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
//		KeyCode[] keylist = {KeyCode.LeftArrow,KeyCode.RightArrow,KeyCode.UpArrow,KeyCode.DownArrow,KeyCode.Z,KeyCode.X};
/*
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
			}
			
		}
		*/
	}
	public enum GameMode{Combat,Bag,Adventure};
	public enum TestMode{Release,Combat,Adventure,SkillTree,Stat};
}
