using UnityEngine;
using System.Collections;

public class ConsumableItem : Item {

	override protected void Reset()
	{
		type = ItemType.Consume;
	}
	
	protected void Awake () {
		
	}
}
