using UnityEngine;
using System.Collections;

public class ConsumableItem : Item {

	override protected void Reset()
	{
		itemType = ItemType.Consume;
	}
	
	protected void Awake () {
		
	}
}
