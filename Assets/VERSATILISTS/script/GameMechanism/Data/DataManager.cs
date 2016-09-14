using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class DataManager : Singleton<DataManager> {
	public PlayerData[] playerData;
	public PlayerData curPlayerData;
	
	void Awake()
	{
		curPlayerData = playerData[0];
		//Load();
		SaveTemplate();
		//Save();
		//ResetSave();
		//SaveTemplate();
	}
	public void newPlayerData()
	{
		playerData[0] = new PlayerData();
		playerData[0].chData = new List<CharacterData>();
		newCharacter();
		newCharacter();
		curPlayerData = playerData[0];
	}
	public void newCharacter()
	{
		CharacterData newChData = new CharacterData();
		newChData.UITemplateID = "player";

		EquipData helmet = new EquipData();
		EquipData weapon = new EquipData();
		EquipData armor = new EquipData();
		EquipData shield = new EquipData();

		helmet.imageID = helmet.id = "starter_headband";
		weapon.imageID = weapon.id = "paladin_hammer";
		armor.imageID = armor.id = "starter_armor";
		shield.imageID = shield.id = "paladin_shield";

		newChData.helmet = helmet;
		newChData.weapon = weapon;
		newChData.armor = armor;
		newChData.shield = shield;

		newChData.statPoints = 10;

		playerData[0].chData.Add(newChData);
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
