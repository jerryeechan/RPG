using UnityEngine;
using System.Collections.Generic;

//using 
public class RandomBattleRound : Singleton<RandomBattleRound> {
	public List<Character> enemies;
	public List<Character> players;
	public Queue<Character> chQueue;
	public Queue<Character> enemyQueue;
	public List<Character> enemyDiedThisTurn;
	public int energyPoints;
	public void StartBattle(DungeonMonsterSet monsterSet)
	{
		GameManager.instance.CombatKeyMode();
		//loadEnemy(enemyWaves[0]);
		//prepare characters from character data
		//List<CharacterData> enemyData =  GetComponent<MonsterDataEditor>().characterDataList;
		players = new List<Character>();
		enemies = new List<Character>();
		chQueue = new Queue<Character>();
		enemyQueue = new Queue<Character>();
		enemyDiedThisTurn = new List<Character>();
		foreach(Character ch in GameManager.instance.chs)
		{
			players.Add(ch);
			chQueue.Enqueue(ch);
			print(ch.name+"參戰");
			ch.BattleStart();
		}
		
		loadEnemy(monsterSet.getNextWave());
		foreach(Character enemy in enemies)
		{
			print(enemy.name+"參戰");
			enemy.BattleStart();
		}


		
		if(enemies.Count>0)
			selectedEnemy = enemies[0];
		currentPlayer = players[0];

		//UI
		DungeonPlayerStateUI.instance.CombatMode();

		Round();	
	}
	
	void loadEnemy(EnemyWave wave)
	{
		Transform saveTransform = GameObject.Find("Save").transform;
		foreach(var preset in wave.enemyPreset)
		{
			Character newCh = preset.chData.genCharacter();
			newCh.transform.SetParent(saveTransform);
			enemies.Add(newCh);
			newCh.side = CharacterSide.Enemy;
		} 
	}
	
	public void ActionDone()
	{
		print("action Done");
		if(enemyDiedThisTurn.Count>0)
		{
			foreach(var enemy in enemyDiedThisTurn)
			{
				//sum exp...
			}
			enemyDiedThisTurn.Clear();
			//play get exp animation
			this.myInvoke(1,NextAction);
		}
		checkGameOver();
		
	}
	 void checkGameOver()
	{
		foreach(var enemy in enemies)
		{
			if(!enemy.isDead)
			{
				NextAction();
				return;
			}
				
		}
		
		print("Battle Win");
		DungeonPlayerStateUI.instance.popUpText("Victory");
		
		this.myInvoke(1,EndofBattle);
			//TODO: back to dungeon mode
			//drop tressure
	}
	void EndofBattle()
	{
		UIManager.instance.ShowCover(()=>{
			GameManager.instance.DungeonMode();
			UIManager.instance.HideCover();
		});
	}
	public void NextAction()
	{	
		if(playerTurn)
		{
			NextPlayerAction();
		}
		else
		{
			NextEnemyAction();
		}
	}


	void NextPlayerAction()
	{
		ActionUIManager.instance.setCharacter(currentPlayer);
	}
	
	bool playerTurn = true;
	public void Round()
	{
		if(playerTurn)
		{
			DungeonPlayerStateUI.instance.popUpText("Your turn");
			foreach(var ch in players)
			{
				foreach(var actionData in ch.actionDataList)
				{
					if(actionData.cd_count!=0)
						actionData.cd_count--;
				}
			}
			DiceRoller2D.instance.Roll(OnDiceRollDone);
			print("player turn");
		}
		else
		{
			DungeonPlayerStateUI.instance.popUpText("Enemy's turn");
			EnergySlotUIManager.instance.removeAll();
			foreach(var enemy in enemies)
			{
				if(!enemy.isDead)
					enemyQueue.Enqueue(enemy);
			}
			NextEnemyAction();
		}
	}

	
	Character enemy;
	void NextEnemyAction()
	{
		if(enemyQueue.Count==0)
		{
			this.myInvoke(2,RoundDone);
		}
		else 
		{
			if(!enemyQueue.Dequeue().useAction(0))
			{
				NextEnemyAction();
			}
		}
	}
	
	/*
	public void Round(){
		print("Round");
		if(chQueue.Count<=1)
		{
			print("game over");
			//return;
		}	
		Character ch = chQueue.Dequeue();
		
		print(ch.name+"'s turn");
		if(ch.isDead)
		{
			print(ch.name+" is Dead!!");
			//Round();
		}
		else
		{
			
			
			if(ch.side == Character.CharacterSide.Player)
			{
				//prepare to roll dice
				//select Player first??
				DiceRoller2D.instance.Roll(OnDiceRollDone);
				currentPlayer = ch;
				print("player turn");
				
			}
			else
			{
				ch.useAction(0);
			}	
		}
	}
	*/
	
	
	public void PlayerRoundDone()
	{
		RoundDone();
	}
	void RoundDone()
	{
		playerTurn = !playerTurn;
		Round();
	}

	public void OnDiceRollDone(int sum)
	{
		
		//actionPanel.SetActive(true);
		EnergySlotUIManager.instance.removeAll();
		EnergySlotUIManager.instance.generateEnergy(sum);
		ActionUIManager.instance.setCharacter(currentPlayer);
		//actionDetailPanel.SetActive(true);
	}
	public void EnemyDie(Character ch)
	{
		enemyDiedThisTurn.Add(ch);
	}

	public void selectAction(int index)
	{
		//currentPlayer.actionData[index].energyCost;
	}	
	public void deSelectAction(int index)
	{
		
	}				
	CharacterData currentPlayerCharacterData;
	public Character currentPlayer;
	public Character selectedEnemy;	
}