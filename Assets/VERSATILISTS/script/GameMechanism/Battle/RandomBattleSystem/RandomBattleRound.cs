using UnityEngine;
using System.Collections.Generic;

//using 
public class RandomBattleRound : Singleton<RandomBattleRound> {
	public List<Character> enemies;
	public List<Character> players;
	Queue<Character> chQueue;

	void Awake()
	{
		//prepare characters from character data
		List<CharacterData> enemyData =  GetComponent<MonsterDataEditor>().characterDataList;
		players = new List<Character>();
		chQueue = new Queue<Character>();


		foreach(CharacterData chData in DataManager.instance.playerData[0].chData)
		{
			Character newPlayer = chData.genCharacter(); 
			players.Add(newPlayer);
			chQueue.Enqueue(newPlayer);
			ActionLogger.Log(newPlayer.name+"參戰\n");
		} 
		foreach(CharacterData chData in enemyData)
		{
			Character enemy = chData.genCharacter(); 
			enemies.Add(enemy);
			chQueue.Enqueue(enemy);
			ActionLogger.Log(enemy.name+"參戰\n");
			enemy.side = Character.CharacterSide.Enemy;
		}
		
		selectedEnemy = enemies[0];
		
	}
	void Start()
	{
		Round();
	}
	public void Round(){
		Character ch = chQueue.Dequeue();
		if(ch.isDead)
		{
			ActionLogger.Log(ch.name+" is Dead\n");
			Round();
		}
		else
		{
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
			chQueue.Enqueue(ch);
		}
	}
									
	CharacterData currentPlayerCharacterData;
	public Character currentPlayer;
	public Character selectedEnemy;	
}