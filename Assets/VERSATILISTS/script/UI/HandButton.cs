using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class HandButton : Button{
	override public void OnPointerEnter(PointerEventData eventData)
	{
		base.OnPointerEnter(eventData);
		CursorManager.instance.PointerMode();
	}
	override public void OnPointerExit(PointerEventData eventData)
	{
		base.OnPointerExit(eventData);
		CursorManager.instance.NormalMode();
	}
	
	
	

}
