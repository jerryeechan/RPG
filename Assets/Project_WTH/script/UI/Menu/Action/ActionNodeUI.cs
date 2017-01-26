using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class SkillNodeUI : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler,IScrollHandler {
    public void OnDrag(PointerEventData eventData)
    {
        SkillTreePanel.instance.OnDrag(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        SkillTreePanel.instance.OnBeginDrag(eventData);
    }
 

    public void OnEndDrag(PointerEventData eventData)
    {
        SkillTreePanel.instance.OnEndDrag(eventData);
    }
 
 
    public void OnScroll(PointerEventData data)
    {
        SkillTreePanel.instance.OnScroll(data);
    }
}
