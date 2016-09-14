using System;
using System.Collections.Generic;
[Serializable]
public class PlayerData{
	public string name;
	public List<CharacterData> chData;
	
	public CharacterData curChData;
	public List<ItemData> itemDataList;
	public List<ActionData> actionDataList;
	public int gold;
	
	//when reach 100, bad things happens
	public int doom;
	
	void Awake(){
//		chData[0].knowNewActionData("strike");
		curChData = chData[0];
	}

	

	//game progress
}
