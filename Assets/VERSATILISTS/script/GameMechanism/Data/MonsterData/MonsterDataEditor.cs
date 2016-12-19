using UnityEngine;
using System.Collections.Generic;

public class MonsterDataEditor :Singleton<MonsterDataEditor> {
    EnemySet[] sets;
    Dictionary<string,EnemySet> monsterSetDict;
    void Awake()
    {
        sets = GetComponentsInChildren<EnemySet>();
        monsterSetDict = new Dictionary<string,EnemySet>();
        foreach(var monsterSet in sets)
        {
            monsterSetDict.Add(monsterSet.name,monsterSet);
        }
    }
    public EnemySet getMonsterSet(string id)
    {
        if(monsterSetDict.ContainsKey(id))
        return Instantiate(monsterSetDict[id]);
        else{
            Debug.LogError("No such monster set, use default");
           
            return getMonsterSet();
        }
    }

    public EnemySet getMonsterSet()
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
