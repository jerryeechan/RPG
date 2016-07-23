using System;

[Serializable]
public class PlayerData{
	public string name;
	public CharacterData[] chData;
	
	public int gold;
	
	void Awake(){
		chData[0].knowNewActionData("strike");
	}

	//game progress
}
