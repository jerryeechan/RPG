using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class InfoTabButton : MonoBehaviour,IPointerClickHandler{

	public InfoTabType type;

    public void OnPointerClick(PointerEventData eventData)
    {
        InfoManager.instance.switchTab(type);
    }
}
