using UnityEngine;
using System.Collections.Generic;

public class MonsterDataEditor :Singleton<MonsterDataEditor> {
    DungeonMonsterSet[] sets;
    Dictionary<string,DungeonMonsterSet> monsterSetDict;
    void Awake()
    {
        sets = GetComponentsInChildren<DungeonMonsterSet>();
        monsterSetDict = new Dictionary<string,DungeonMonsterSet>();
        foreach(var monsterSet in sets)
        {
            monsterSetDict.Add(monsterSet.name,monsterSet);
        }
    }
    public DungeonMonsterSet getMonsterSet(string id)
    {
        if(monsterSetDict.ContainsKey(id))
        return monsterSetDict[id];
        else{
            Debug.LogError("No such monster set");
            return null;
        }
    }

    public DungeonMonsterSet getMonsterSet()
    {
        string id = "test";
        if(monsterSetDict.ContainsKey(id))
        return Instantiate(monsterSetDict[id]);
        else{
            Debug.LogError("No such monster set");
            return null;
        }
    }

    
}
