using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.jerrch.rpg;
using DG.Tweening;
public class TurnBattleManager : Singleton<TurnBattleManager> {

	[SerializeField]
	AnimatableCanvas victoryPanel;
	[SerializeField]
	AnimatableCanvas gameOverPanel;
	//List<DiceSkillSlot> playerSlot;

	//List<DiceSkillSlot> slots; //change every time

	public List<Character> monsters;
	public List<Character> players;

	public List<Character> diedEnemies;
	public List<Character> diedPlayers;

	public Character selectedPlayer;
	
	DiceSelectionDisplayer diceDisplayer;
	void Awake()
	{
		diceDisplayer = GetComponentInChildren<DiceSelectionDisplayer>();
	}
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
			if(ch.battleStat.hp.currentValue>maxHp)
			{
				maxHp = ch.battleStat.hp.currentValue;
				target = ch;
			}
		}
		return target;
	}

	
	public Character currentPlayer;
	//public List<Character> enemyDiedThisTurn;


	//slot's order should be rearranged
	
	//skill holder
	
	//skill Queue player
	
	
	
	
	
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
			//ch.BattleStart();
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
		currentPlayer.chRenderer.selected();
	}
	[SerializeField]
	HandButton readyBtn;
	public void StartRound()
	{
		diceDisplayer.reset();
		DiceRollerAll.instance.Roll(rollDone);
	}
	
	void rollDone(SkillDiceType[] results,int enemyIndex)
	{	
		readyBtn.interactable = true;
		//prepare monster skill;
		var enemySlot = diceDisplayer.getSkillSlot(enemyIndex);
		foreach(var enemy in monsters)
		enemySlot.skillUnit.AddSkill(enemy,enemy.skillList[0]);
		diceDisplayer.rollDone();
		print("Rolldone");
		//TODO: extra bonus of rolling result
	
	}	
	//Ready btn???

	/*
	public void testReady()
	{
		int count = 0;
		foreach(var dice in dices)
		{
			if(dice.skillSlot.skillUnit.skillPairs.Count > 0)
			{
				count++;
			}
		}
		if(isTesting)
		{
			if(count==testCount)
			{
				SkillReady();
			}
		}
		else
		{
			if(count==2)//Fake 1
			{
				SkillReady();
			}
		}
	}*/
	public void selectSkill(Skill skill)
	{
		diceDisplayer.selectedDice.setSkill(skill);
		diceDisplayer.selectNextDice();
	}
	public void selectSlot(int index)
	{
		diceDisplayer.selectDice(index);
	}

	public void SkillReady()
	{
		//Enemies' skills 
		diceDisplayer.PrepareToPlay();
		readyBtn.interactable = false;
		this.myInvoke(0.5f,NextSkill);
	}
	
	Queue<SkillPair> currentSkillQueue;
	
	// Update is called once per frame
	void NextSkill()
	{
		Debug.Log("NextSkill");
		
		if(currentSkillQueue==null||currentSkillQueue.Count==0)
		{
			var dice = diceDisplayer.playNextDice();
			if(dice==null)
			{
				this.myInvoke(1,StartRound);
				return;	
			}
			currentSkillQueue = dice.skillSlot.skillUnit.skillPairs;
			
		}
		
		if(currentSkillQueue.Count>0)
		{
			SkillPair skillPair =	currentSkillQueue.Dequeue();
			skillPair.PlaySkill(SkillDone);
		}
		else
		{
			NextSkill();
		}
		
	}

	
	void SkillDone()
	{
		for(int i=0;i<monsters.Count;i++)
		{
			var enemy = monsters[i];
			if(enemy.isDead)
			{
				diedEnemies.Add(enemy);
				monsters.Remove(enemy);
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
				diedPlayers.Add(player);
				players.RemoveAt(i);
				i--;
			}
		}
		if(players.Count==0)
		{
			this.myInvoke(2,GameOver);
			return;
		}

		Debug.Log("skill done");
		this.myInvoke(0.5f,NextSkill);
		
		
	}
	void GameOver()
	{
		gameOverPanel.show();
		this.myInvoke(5,()=>{
			WTHSceneManager.instance.BackToMainMenu();
		});
	}
	void EndofBattle()
	{
		print("End of Battle");
		addExp();
		victoryPanel.show();
	}

	void addExp()
	{
		BattleChUIManager.instance.allGetExp(60,Exit);
	}

	void Exit()
	{
		foreach(var ch in players)
		{
			ch.BattleEnd();
		}
		GameManager.instance.AdventureMode();
		AdventureManager.instance.NextEvent();
		Destroy(stageTransform.gameObject);
		diceDisplayer.end();
		victoryPanel.hide();
	}
}
