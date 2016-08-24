using UnityEngine;
using System.Collections.Generic;

//using 
public class RandomBattleRound : Singleton<RandomBattleRound> {
	public List<Character> enemies;
	public List<Character> players;
	public Queue<Character> chQueue;

	public int energyPoints;
	public void StartGame()
	{
		//prepare characters from character data
		List<CharacterData> enemyData =  GetComponent<MonsterDataEditor>().characterDataList;
		players = new List<Character>();
		enemies = new List<Character>();
		chQueue = new Queue<Character>();

		foreach(Character ch in GameManager.instance.chs)
		{
			players.Add(ch);
			chQueue.Enqueue(ch);
			ActionLogger.Log(ch.name+"參戰");
			ch.BattleStart();
		}
		
		foreach(Character enemy in GameManager.instance.enemies)
		{
			enemies.Add(enemy);
			chQueue.Enqueue(enemy);
			ActionLogger.Log(enemy.name+"參戰");
			enemy.BattleStart();
		}
		
		if(enemies.Count>0)
			selectedEnemy = enemies[0];
		currentPlayer = players[0];
		Round();	
	}
	
	
	
	public void NextRound()
	{
		Invoke("Round",1);
	}
	public void Round(){
		print("Round");
		if(chQueue.Count<=1)
			return;
		Character ch = chQueue.Dequeue();
		
		ActionLogger.Log(ch.name+"'s turn");
		if(ch.isDead)
		{
			ActionLogger.Log(ch.name+" is Dead!!");
			//Round();
		}
		else
		{
			chQueue.Enqueue(ch);
			if(ch.side == Character.CharacterSide.Player)
			{
				//prepare to roll dice
				//select Player first??
				currentPlayer = ch;
				ActionUIManager.instance.setCharacter(ch);
			}
			else
			{
				ch.useAction((int)Random.value*6);
			}	
		}
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