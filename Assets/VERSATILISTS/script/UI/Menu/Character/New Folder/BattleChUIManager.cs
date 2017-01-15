using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.jerrch.rpg;
public class BattleChUIManager :SingletonChUIManager<BattleChUIManager> {
	public void allGetExp(int val)
    {
        foreach(var ch in GameManager.instance.chs)
        {
            ch.getExp(val);
        }
        exp.addValue(val);
    }
}

