using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class ActionNodeUI : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler,IScrollHandler {
    public void OnDrag(PointerEventData eventData)
    {
        ActionTreePanel.instance.OnDrag(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ActionTreePanel.instance.OnBeginDrag(eventData);
    }
 

    public void OnEndDrag(PointerEventData eventData)
    {
        ActionTreePanel.instance.OnEndDrag(eventData);
    }
 
 
    public void OnScroll(PointerEventData data)
    {
        ActionTreePanel.instance.OnScroll(data);
    }
}
