using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class DataManager : Singleton<DataManager> {
	public PlayerData[] playerData;
	void Awake()
	{
		//Load();
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
	public void ResetSave()
	{
		FileStream readFile = File.OpenRead("Save/template.binary");
		BinaryFormatter formatter = new BinaryFormatter();
		playerData = formatter.Deserialize(readFile) as PlayerData[];
		readFile.Close();
	}
}
