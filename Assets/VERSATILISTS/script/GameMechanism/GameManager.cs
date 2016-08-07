using UnityEngine;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {

	public Character currentCh;
	public List<Character> chs;
	public List<Character> enemies;
	public Transform menu;
	public EnemyWave[] enemyWaves;
	Transform saveTransform;
	void Start()
	{
		
		StartGame();
	}
	public void StartGame()
	{
		if(saveTransform)
			Destroy(saveTransform.gameObject);
		saveTransform = new GameObject("Save").transform;

		CharacterManager.instance.StartGame();
		loadCharacter();
		loadEnemy(enemyWaves[0]);
		loadItem();

		RandomBattleRound.instance.StartGame();
	}
	void loadCharacter()
	{
		//create player characters
		chs = new List<Character>();
		foreach(CharacterData chData in DataManager.instance.playerData[0].chData)
		{
			Character newPlayer = chData.genCharacter();
			newPlayer.transform.SetParent(saveTransform); 
			chs.Add(newPlayer);
		} 
		selectCh(chs[0]);
		

		//update stat ui
		CharacterStatUIManager.instance.viewCharacter(GameManager.instance.currentCh);
	}
	void loadEnemy(EnemyWave wave)
	{
		foreach(CharacterData chData in wave.enemyData)
		{
			Character newCh = chData.genCharacter();
			newCh.transform.SetParent(saveTransform); 
			enemies.Add(newCh);
			newCh.side = Character.CharacterSide.Enemy;
		} 
	}
	void loadItem()
	{
		int i=0;
		foreach(var itemData in DataManager.instance.curPlayerData.itemDataList)
		{
			Item item = ItemManager.instance.getItem(itemData);
			item.transform.SetParent(saveTransform);
			ItemUIManager.instance.setItem(item,i);
			i++;
		}
	}

	void selectCh(Character ch)
	{
		currentCh = ch;
	}

	bool isMenuActive = true;
	void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
		{
			isMenuActive = !isMenuActive;
			menu.gameObject.SetActive(isMenuActive);
		}
            
        
    }
}
