using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
public class HoverUI : MonoBehaviour,IDescribable ,IPointerEnterHandler,IPointerExitHandler{
	
	public virtual string description()
	{
		//return bindAction.description;
		return "";
	}

	public virtual string title()
	{
		//return bindAction.actionData.id;
		return "";
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		if(DescriptionUIManager.instance)
		{
			DescriptionUIManager.instance.show(title(),description());
		}
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		//DescriptionUIManager.instance.hide();
	}
}
