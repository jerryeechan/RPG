using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillTreePanel : Singleton<SkillTreePanel>, IDragHandler,IBeginDragHandler
{
	void Awake()
	{
		scrollRect = GetComponent<ScrollRect>();
	}
	ScrollRect scrollRect;
    public void OnDrag(PointerEventData eventData)
    {
        scrollRect.OnDrag(eventData);
    }

	public void OnBeginDrag(PointerEventData eventData)
    {
        scrollRect.OnBeginDrag(eventData);
    }
 
    public void OnEndDrag(PointerEventData eventData)
    {
        scrollRect.OnEndDrag(eventData);
    }
 
 
    public void OnScroll(PointerEventData data)
    {
        scrollRect.OnScroll(data);
    }
}
