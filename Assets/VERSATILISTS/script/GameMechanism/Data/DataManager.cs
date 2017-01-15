using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace com.jerrch.rpg{
public class DataManager : Singleton<DataManager> {
	public PlayerData[] playerData;
	public PlayerData curPlayerData;
	
	public List<CharacterData> rosterSelected;
	void Awake()
	{
		curPlayerData = playerData[0];
		//Load();
		//SaveTemplate();
		//Save();
		//ResetSave();
		//SaveTemplate();
	}
	public static PlayerData currentData()
	{
		return instance.curPlayerData;
	}

	public PlayerData createPlayerData()
	{
		playerData[0] = new PlayerData();
		playerData[0].gold = 60;
		curPlayerData = playerData[0];
		return playerData[0];
	}
	
	public void Load()
	{
		FileStream readFile = File.OpenRead("Save/player.binary");
		BinaryFormatter formatter = new BinaryFormatter();
		playerData = formatter.Deserialize(readFile) as PlayerData[];
		readFile.Close();
		GameManager.instance.StartGame();
	}

	public void Save()
	{
		FileStream saveFile = File.Create("Save/player.binary");
		BinaryFormatter formatter = new BinaryFormatter();

		curPlayerData.actionDataList = new List<ActionData>();
		
		

		formatter.Serialize(saveFile,playerData);
		saveFile.Close();
	}
	public void SaveTemplate()
	{
		FileStream saveFile = File.Create("Save/template.binary");
		BinaryFormatter formatter = new BinaryFormatter();
		formatter.Serialize(saveFile,playerData);
		saveFile.Close();
	}
	public void Reset()
	{
		FileStream readFile = File.OpenRead("Save/template.binary");
		BinaryFormatter formatter = new BinaryFormatter();
		playerData = formatter.Deserialize(readFile) as PlayerData[];
		readFile.Close();
		GameManager.instance.StartGame();
	}
}
}