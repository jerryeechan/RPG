using System;

[Serializable]
public class PlayerData{
	public string name;
	public CharacterData[] chData;
	
	public CharacterData curChData;
	public ItemData[] itemDataList;
	public int gold;
	
	void Awake(){
		chData[0].knowNewActionData("strike");
		curChData = chData[0];
	}

	

	//game progress
}
