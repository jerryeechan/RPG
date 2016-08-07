using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class DataManager : Singleton<DataManager> {
	public PlayerData[] playerData;
	public PlayerData curPlayerData;
	public static CharacterData curChData;
	void Awake()
	{
		curPlayerData = playerData[0];
		curChData = curPlayerData.chData[0];
		//Load();
		SaveTemplate();
		//Save();
		//ResetSave();
		//SaveTemplate();
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
