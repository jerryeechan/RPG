using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class ActionSlot : ActionButton, IDropHandler
{
	public int index;
    public void OnDrop(PointerEventData eventData)
    {
        ActionTree.instance.OnDropActionSlot(this);
    }

}
