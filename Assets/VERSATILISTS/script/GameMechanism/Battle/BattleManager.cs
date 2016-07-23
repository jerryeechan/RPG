using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BattleManager : Singleton<BattleManager>{

	//Queue<Character>
	
	public List<BattleStage> stages;
	
	public List<Character> playerCharacters;
	List<CharacterButton> chButtons;
	public List<ActionButton[]> playerSkillButtons;
	
	public List<Character> enemyCharacters;
	
	int playerChNum;
	int hasDoneMove;
	int enemyNum;
	
	public Transform canvas;
	public GameObject TurnMark;
	BattleStage stage;
	
	List<Character> cdQueue;
	public void Awake()
	{
		cdQueue = new List<Character>();
	}
	
	void Start()
	{
		//start battle scene load the stage 
		//LoadStage();
	}
	/*
	public void LoadStage()
	{
		stage = (BattleStage)Instantiate(stages[PlayerPrefs.GetInt("stage")]);		
		stage.name = "BattleStage";
		
		TurnMark.SetActive(false);
		
		//get enemy and player,prepare UI then start battleq
		LoadPlayer();
		LoadEnemy();
		PrepareUI();
		BattleStart();
	}
	void LoadPlayer()
	{
		playerCharacters = Character.playerTeam;
		//for testing
		if(playerCharacters==null)
		{
			playerCharacters = Character.GenPlayer();
		}
		
		foreach(Character player in playerCharacters)
		{
			player.transform.position = CharacterPositionSetter.instance.playerPosition;
		}
		cdQueue.AddRange(playerCharacters);
		playerChNum = playerCharacters.Count;
	}
	void LoadEnemy()
	{
		enemyCharacters = stage.getNextWave();
		Vector3[] positions= CharacterPositionSetter.instance.getEnemyPosition(enemyCharacters.Count);
		for(int i=0;i<enemyCharacters.Count;i++)
		{
			enemyCharacters[i].transform.position = positions[i];
			cdQueue.Add(enemyCharacters[i]);
		}
		
		enemyNum = enemyCharacters.Count;
		
	}
	void PrepareUI()
	{
		uiBuilder.SetPlayerCharacterButton(playerCharacters);
	}
	void StartAnimation()
	{
		Invoke("FindNextInQueue",0);
	}
	public void BattleStart()
	{
		
		chQueue = new Queue<Character>();
		chCount = 0;
		for(int i=0;i<playerChNum;i++)
		{
			Debug.Log("Player Character:"+playerCharacters[i].name+"\nhealth:"+playerCharacters[i].battleStat.hp);
			chQueue.Enqueue(playerCharacters[i]);
		}
		
		Debug.Log("..........");
		for(int i=0;i<enemyNum;i++)
		{
			Debug.Log("Enemy Character:"+enemyCharacters[i].name+"\nhealth:"+enemyCharacters[i].battleStat.hp);
			chQueue.Enqueue(enemyCharacters[i]);
		}
		
			
		chCountTotal = playerChNum+enemyNum;
		
		cdQueue.Sort();
		//Debug.Log("in cd queue"+cdQueue.Count);
		StartAnimation();
		
		//FindNextInQueue();
		
		//playerTurn();
	}
	void PlayerTurn(Character ch)
	{
		Debug.Log("Wait Player to do Skill...");
		ch.skillPanel.UnlockPanel();
		
	}
	public void PlayerUseSkill(Character player,int id)
	{
		List<Character> targets = enemyCharacters.GetRange(0,1);
		Debug.Log("Player use "+player.skills[id].name);
		player.useSkill(id,targets);
		//hasDoneMove++;
		
		//wait for skillapplyResult
	}
	
	void EnemyTurn(Character enemy)
	{
		//for(int i=0;i<enemyCharacters.Count;i++)
		//{
		//	Character enemy = enemyCharacters[i];
		//	enemy.useSkill(0,playerCharacters.GetRange(0,1));
		//}
		
		Debug.Log("enemy use skill");
		enemy.useSkill(0,playerCharacters.GetRange(0,1));
		//enemy.EndTurn();
		
		//		}
		//if(!enemy.isDead())
		//	cdQueue.Add(enemy);
		
		//Invoke("FindNextFromQueue",1);
		//FindNextFromQueue();
	}
	
	
	
	int chCount =0 ;
	int chCountTotal;
	Queue<Character> chQueue;
	//SortedList<int,Character> chQueue;
	void FindNextInQueue()
	{
		Debug.Log("FindNext");
		chCount++;
		Character ch = chQueue.Dequeue();
		if(ch.side== Character.CharacterSide.Player)
		{
			PlayerTurn(ch);
		}
		else
		{
			EnemyTurn(ch);
		}
	}
	public void SkillApplyResult(SkillApplyStat stat)
	{
		
		SkillApplyLog(stat);
		CheckGameStatus();
		if(chCount==chCountTotal)
		{
			chCount = 0;
			OneRound();
		}
		FindNextInQueue();
	}
	void OneRound()
	{
		Debug.Log("One round");
		for(int i=0;i<playerCharacters.Count;i++)
		{
			playerCharacters[i].EndRound();
			chQueue.Enqueue(playerCharacters[i]);
		}
		for(int i=0;i<enemyCharacters.Count;i++)
		{
			enemyCharacters[i].EndRound();
			chQueue.Enqueue(enemyCharacters[i]);
		}
		
		
		cdQueue.Sort();
	}
	/*void Update()
	{
		for(int i=0;i<playerCharacters.Count;i++)
		{
			Character player =playerCharacters[i];
			if(player.OneTurn(Time.deltaTime)==0&&!player.isSkillReady)
			{
				player.isSkillReady = true;
				playerTurn(player);
			}
			
		}
		for(int i=0;i<enemyCharacters.Count;i++)
		{
			if(enemyCharacters[i].OneTurn(Time.deltaTime)==0)
			{
				EnemyTurn(enemyCharacters[i]);
			}
			
		}
	}
	
	public void EnemyTurn(Character enemy)
	{
//		for(int i=0;i<enemyCharacters.Count;i++)
//		{
		//PlaceTurnMarkOn(enemy);
		Debug.Log("Enemy Use "+enemy.skills[0].name+" on "+playerCharacters[0].name);
		enemy.useSkill(0,playerCharacters.GetRange(0,1));
		//enemy.EndTurn();
		CheckGameStatus();
//		}
		
		//if(!enemy.isDead())
		//	cdQueue.Add(enemy);
			
		//Invoke("FindNextFromQueue",1);
		//FindNextFromQueue();
	}
	
	public void SkillApplyLog(SkillApplyStat s)
	{
		Debug.Log("skill apply result: hit on "+s.applyTarget.name+" "+s.name+" cause "+s.damageCause+" damage");
		
	}
	
	public GameObject WinBadge;
	public void CheckGameStatus()
	{
		bool isPlayerTeamAllDead = true; 
		bool isEnemyTeamAllDead = true;
		
		for(int i=0;i<playerChNum;i++)
			if(!playerCharacters[i].isDead)
				isPlayerTeamAllDead = false;
		
		for(int i=0;i<enemyNum;i++)
			if(!enemyCharacters[i].isDead)
				isEnemyTeamAllDead = false;
		
		if(isEnemyTeamAllDead)
		{
			Debug.Log("Win");
			Instantiate(WinBadge);
			Invoke("ExitStage",1);
		}
		
		if(isPlayerTeamAllDead)
		{
			Debug.Log("Lose");
			Invoke("ExitStage",1);
		}
		
	}
	public void ExitStage()
	{
		//Destroy(BattleStageMenu.stage.gameObject);
		Application.LoadLevel("menu");
	//Destroy skill panels
	//Clear stage stuff	
	}
	
	/*
	int oneTurnCnt=0;
	void OneTurn()
	{
		string cdInfo = "";
		readyNum = 0;
		for(int i=0;i<cdQueue.Count;i++)
		{
			int cd = cdQueue[i].OneTurn();
			cdInfo+=cdQueue[i].name+cd+", ";
			if(cd==0)
				readyNum++;
		}	
		Debug.Log("One Turn cd info:"+cdInfo);
		if(readyNum>0)
		{
			cdQueue.Sort();
			Debug.Log(cdQueue[0].name+cdQueue[0].cd);
			oneTurnCnt = 0;
			FindNextFromQueue();
		}
		else
		{
			oneTurnCnt++;
			if(oneTurnCnt<10)
			Invoke("OneTurn",0.2f);
			else
			Debug.Log("inf loop");
		}
	}
	
	int readyNum = 0;
	int ct=0;
	void FindNextFromQueue()
	{
		TurnMark.SetActive(false);
		Debug.Log("find next");
		ct++;
		if(ct>5)
		{
			Debug.Log("inf find");
			return;
		}
		
		Debug.Log(cdQueue[0].name+cdQueue[0].cd);
		if(cdQueue[0].cd==0)
		{
			
			Character readyCh = cdQueue[0];
			cdQueue.Remove(cdQueue[0]);
			Debug.Log(readyCh.name+" cd 0");
			if(readyCh.side== Character.CharacterSide.Player)
				playerTurn(readyCh);
			else 
				EnemyTurn(readyCh);
			
			
			ct = 0;
		}
		else
		{
			OneTurn();
		}
	}
	void PlaceTurnMarkOn(Character ch)
	{
		TurnMark.SetActive(true);
		TurnMark.transform.position = ch.markPosition.position+new Vector3(0,10);
	}
	*/
}
