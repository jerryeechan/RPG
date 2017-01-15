using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using com.jerrch.rpg;
public class BattleChUIManager :SingletonChUIManager<BattleChUIManager> {

    CompositeText nameText;
	public void allGetExp(int val,OnCompleteDelegate d)
    {
        foreach(var ch in GameManager.instance.chs)
        {
            ch.getExp(val);
        }
        exp.addValue(val,()=>
        {
            d();
            
        });
    }
    /*
    public override void setCharacter(Character ch)
    {
        base.setCharacter(ch);
        
    }
    */
}

