using UnityEngine;
using System.Collections.Generic;
namespace com.jerrch.rpg
{
//using 
	public class RandomBattleRound : Singleton<RandomBattleRound> {
		public List<Character> enemies;
		public List<Character> players;
		public Queue<Character> chQueue;
		public Queue<Character> enemyQueue;
		public List<Character> enemyDiedThisTurn;
		public int energyPoints;
		Transform stageTransform;
		public void StartBattle(EnemySet enemySet)
		{
			GameManager.instance.CombatMode();
			stageTransform = new GameObject("CombatStage").GetComponent<Transform>();
			//stageTransform.gameObject.SetActive(true);
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
			
			enemies = CharacterManager.instance.loadEnemy(enemySet.getNextWave());
			foreach(Character enemy in enemies)
			{
				print(enemy.name+"參戰");
				enemy.BattleStart();
				enemy.transform.SetParent(stageTransform);
			}

			if(enemies.Count>0)
				selectedEnemy = enemies[0];
			currentPlayer = players[0];

			//UI
			Round();	
		}
		
		
		
		public void ActionDone()
		{
			print("action Done");
			if(enemyDiedThisTurn.Count>0)
			{
				int exp = 0;
				foreach(var enemy in enemyDiedThisTurn)
				{
					//sum exp...
					exp+=20;
				}

				//exp
				//foreach(var ch in GameManager.instance.chs)
				//	ch.getExp(exp);

				enemyDiedThisTurn.Clear();
				//play get exp animation
				this.myInvoke(1,NextAction);
			}
			checkBattleOver();
			
		}
		void checkBattleOver()
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
			PlayerStateUI.instance.popUpText("Victory");
			
			this.myInvoke(1,EndofBattle);
				//TODO: back to dungeon mode
				//drop tressure
		}
		void EndofBattle()
		{
			print("End of Battle");
			foreach(var ch in players)
			{
				ch.BattleEnd();
			}
			/*
			UIManager.instance.ShowCover(()=>{
				GameManager.instance.AdventureMode();
				AdventureManager.instance.NextEvent();
				Destroy(stageTransform.gameObject);
				UIManager.instance.HideCover();
			});
			*/
		}
		public void NextAction()
		{	
			if(isPlayerTurn)
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
			if(!DiceRollerSingle.instance.isRoundDone)
				{//DiceRollerSingle.instance.Roll(OnDiceRollDone);
				}
			else
				RoundDone();
			//ActionUIManager.instance.setCharacter(currentPlayer);
		}
		
		bool isPlayerTurn = true;
		public void Round()
		{
			if(isPlayerTurn)
			{
				PlayerStateUI.instance.popUpText("Your turn");
				//renew action available and count CD  
				foreach(var ch in players)
				{
					ch.doneActionThisRound = false;
					foreach(var action in ch.actionList)
					{
						if(action==null||action.isPassive)
							continue;
						else if(action.cd_count!=0)
							action.cd_count--;
					}
				}
				//DiceRoller2D.instance.Roll(OnDiceRollDone);
				DiceRollerSingle.instance.init();
				DiceRollerSingle.instance.Roll(OnDiceRollDone);
				print("player turn");
			}
			else
			{
				PlayerStateUI.instance.popUpText("Enemy's turn");
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
				//TODO
				Character enemy = enemyQueue.Dequeue();

				//enemy.useAction(0);
				int r = Random.Range(0,enemy.actionList.Count);
				//enemy.useAction(r);
				/* 
				if(!.useAction(0))
				{
					NextEnemyAction();
				}*/
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
			if(isPlayerTurn)
				RoundDone();
		}
		void RoundDone()
		{
			isPlayerTurn = !isPlayerTurn;
			Round();
		}

		int[] diceValues;
		public void OnDiceRollDone(int[] values)
		{
			diceValues = values;
			ActionUIManager.instance.setCharacter(currentPlayer);
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
		public Character selectedAllies;

	}
}

//6 1 2   b3 b3b3 212

//SONG
//2 5 2 5 1. 2/ 3b 2.. 
//        1. 7 6 5