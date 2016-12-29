using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.jerrch.rpg;
using DG.Tweening;
public class TurnBattleManager : Singleton<TurnBattleManager> {

	//List<DiceActionSlot> playerSlot;

	//List<DiceActionSlot> slots; //change every time

	public List<Character> monsters;
	public List<Character> players;
	public Character selectedPlayer;

	Character randomTarget(List<Character> chs)
	{
		int r = Random.Range(0,chs.Count);
		return chs[r];
	}

	public Character filterCharacter(List<Character> chs,SkillSpecificFilter filter)
	{
			
		switch(filter)
		{
			case SkillSpecificFilter.HighestHP:
				return FindHighestHp(chs);
			case SkillSpecificFilter.Random:
				return randomTarget(chs);
			default:
				return randomTarget(chs);
		}
		
	}
	
	public Character filterEnemy(CharacterSide side,SkillSpecificFilter filter)
	{
		if(side == CharacterSide.Enemy)
		{
			return filterCharacter(players,filter);
		}
		else
		{
			return filterCharacter(monsters,filter);
		}
	}
	public Character filterAllies(CharacterSide side,SkillSpecificFilter filter)
	{
		if(side == CharacterSide.Player)
		{
			return filterCharacter(players,filter);
		}
		else
		{
			return filterCharacter(monsters,filter);
		}
	}
	Character FindHighestHp(List<Character> chs)
	{
		Character target = chs[0];
		int maxHp = 0;
		foreach(var ch in chs)
		{
			if(ch.battleStat.hp>maxHp)
			{
				maxHp = ch.battleStat.hp;
				target = ch;
			}
		}
		return target;
	}

	
	public Character currentPlayer;
	//public List<Character> enemyDiedThisTurn;


	//slot's order should be rearranged
	
	//action holder
	
	//action Queue player
	Queue<DiceSlot> diceQueue;
	[SerializeField]
	Image indicator;
	
	void Awake()
	{
		
	}
	
	void Start()
	{
		
	}

	Transform stageTransform;
	public void StartBattle(EnemySet enemySet)
	{
		GameManager.instance.CombatMode();
		stageTransform = new GameObject("CombatStage").GetComponent<Transform>();
		players = new List<Character>();
		monsters = new List<Character>();
		
		
		foreach(Character ch in GameManager.instance.chs)
		{
			players.Add(ch);
			print(ch.name+"參戰");
			ch.BattleStart();
		}
		
		monsters = CharacterManager.instance.loadEnemy(enemySet.getNextWave());
		foreach(Character enemy in monsters)
		{
			print(enemy.name+"參戰");
			enemy.BattleStart();
			enemy.transform.SetParent(stageTransform);
		}
		currentPlayer = players[0];
		StartRound();
	}
	public void StartRound()
	{
		reset();
		DiceRollerAll.instance.Roll(rollDone);
	}
	void rollDone(ActionDiceType[] results)
	{
		 //useless...?
	}

	DiceSlot[] dices;

	public void reset()
	{
		dices = DiceRollerAll.instance.getDices;
		indicator.enabled = false;
		indicator.rectTransform.anchoredPosition = dices[0].GetComponent<RectTransform>().anchoredPosition;
		currentActionQueue = null;


		foreach(var dice in dices)
		{
			dice.clear();
		}
	}

	[SerializeField]
	bool isTesting = true;
	[SerializeField]
	int testCount = 2;
	//Ready btn???
	public void testReady()
	{
		int count = 0;
		foreach(var dice in dices)
		{
			if(dice.actionSlot.actionUnit.actionPairs.Count > 0)
			{
				count++;
			}
		}
		if(isTesting)
		{
			if(count==testCount)
			{
				ActionReady();
			}
		}
		else
		{
			if(count==3)
			{
				ActionReady();
			}
		}
	}
	//complete actions assignment,
	public void ActionReady()
	{
		//Enemies' actions 
		indicator.enabled = true;
		diceQueue = new Queue<DiceSlot>();
		foreach(var dice in dices)
		{
			if(!dice.isPlayerDice){
				foreach(var enemy in monsters)
					dice.actionSlot.actionUnit.AddAction(enemy,enemy.actionList[0]);
			}
			diceQueue.Enqueue(dice);
		}
		this.myInvoke(0.5f,NextAction);
	}
	
	Queue<ActionPair> currentActionQueue;
	
	// Update is called once per frame
	void NextAction()
	{
		Debug.Log("NextAction");
		//四郎探母
		if(currentActionQueue==null||currentActionQueue.Count==0)
		{
			if(diceQueue.Count==0)
			{
				this.myInvoke(1,StartRound);
				return;	
			}
			var dice = diceQueue.Dequeue();
			currentActionQueue = dice.actionSlot.actionUnit.actionPairs;
			indicator.rectTransform.DOMove(dice.transform.position,0.2f);
		}
		
		if(currentActionQueue.Count>0)
		{
			ActionPair actionPair =	currentActionQueue.Dequeue();
			actionPair.PlayAction(ActionDone);
		}
		else
		{
			NextAction();
		}
		
	}

	
	public void ActionDone()
	{
		
		
		for(int i=0;i<monsters.Count;i++)
		{
			var enemy = monsters[i];
			if(enemy.isDead)
			{
				monsters.RemoveAt(i);
				i--;
			}
		}
		if(monsters.Count==0)
		{
			this.myInvoke(2,EndofBattle);
			return;
		}

		for(int i=0;i<players.Count;i++)
		{
			var player = players[i];
			if(player.isDead)
			{
				players.RemoveAt(i);
				i--;
			}
		}

		Debug.Log("action done");
		this.myInvoke(0.5f,NextAction);
		
		
	}

	void EndofBattle()
	{
		print("End of Battle");
		foreach(var ch in players)
		{
			ch.BattleEnd();
		}
		UIManager.instance.ShowCover(()=>{
			GameManager.instance.AdventureMode();
			AdventureManager.instance.NextEvent();
			Destroy(stageTransform.gameObject);
			UIManager.instance.HideCover();
		});
	}
}
