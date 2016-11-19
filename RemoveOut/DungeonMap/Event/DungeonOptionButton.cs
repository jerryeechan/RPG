using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class DungeonOptionButton : MonoBehaviour,IPointerEnterHandler,IPointerClickHandler {

	public CompositeText text;
	public int index;
	void Awake()
	{
		text = GetComponentInChildren<CompositeText>();
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        DungeonOptionSelector.instance.moveTo(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        DungeonOptionSelector.instance.pressButton();
    }
}

