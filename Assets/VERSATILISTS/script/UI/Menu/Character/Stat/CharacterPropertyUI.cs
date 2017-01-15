using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class CharacterPropertyUI :MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IDescribable{

	public string title;
	public string description;
	
	public AttributeType type;
	public bool isPercentage;
	// propertyText;
	CompositeText valueText;
	void Awake()
	{
		valueText = transform.GetComponentInChildren<CompositeText>();
	}
	
	public void setValue(float v)
	{
		string vText = ((int)v).ToString();
		if(isPercentage)
			vText = ""+(int)(v*100)+"%";
		
		valueText.text = vText; 
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		DescriptionUIManager.instance.show(title,description);
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		DescriptionUIManager.instance.hide();
	}

    string IDescribable.description()
    {
		return description;
    }

    string IDescribable.title()
    {
        return title;
    }
}
