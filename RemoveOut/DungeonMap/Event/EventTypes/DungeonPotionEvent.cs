using UnityEngine;
using System.Collections;

public class DungeonPotionEvent : DungeonEvent {
    public int healAmount;
    override public void confirm(){
		base.confirm();
		
		
	}
	override public void cancel()
	{
		base.cancel();
		DiceRoller2D.instance.Roll(drinkIt);
	}

  
	public void pickUp()
    {
        //put in to bag
    }

    public void drinkIt(int num)
    {
        //bonus 
    }
}