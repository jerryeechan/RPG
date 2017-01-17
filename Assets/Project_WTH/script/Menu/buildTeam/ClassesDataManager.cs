using System.Collections.Generic;
using UnityEngine;

public class ClassesDataManager : Singleton<ClassesDataManager>{

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
	int r = 0;
	public Sprite getClassIcon(string classID)
	{
		if(classesDataDict.ContainsKey(classID))
		return classesDataDict[classID].iconSprite;
		else{
			Debug.LogError("no icon sprite");
			return null;
		}
	}
	public ClassesData getClassData(string classID)
	{
		return classesDataDict[classID];
	}
	public CharacterData generateChData()
	{
		if(r==classesDataList.Length)
			r=0;
		//classesDataList[r].getChData();
		
		
		//assign classes actions
		
		return classesDataList[r++].generateChData();
	}
}
