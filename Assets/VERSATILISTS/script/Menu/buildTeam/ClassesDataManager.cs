using System.Collections.Generic;
using UnityEngine;

public class ClassesDataManager : Singleton<ClassesDataManager>{

	[SerializeField]
	CharacterDataEditor rosterTemplates;
	ClassesData[] classesDataList;
	Dictionary<string,ClassesData> classesDataDict;

	void Awake()
	{
		classesDataList = GetComponentsInChildren<ClassesData>();
		classesDataDict = new Dictionary<string,ClassesData>();
		foreach(var classesData in classesDataList)
		{
			classesDataDict.Add(classesData.classID,classesData);
		}
	}

	public CharacterData generateChData()
	{
		int r = 0;
		CharacterData chData = rosterTemplates.characterDataList[r];
		ClassesData classesData = classesDataDict[chData.classID];
		
		//assign classes actions
		chData.actionIDs = classesData.getRandomActionIDs();
		return chData;
	}
}
