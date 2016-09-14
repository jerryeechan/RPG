using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
public class HandButton : Button{
	
	override public void OnPointerEnter(PointerEventData eventData)
	{
		if(enabled)
		{
			base.OnPointerEnter(eventData);
			CursorManager.instance.PointerMode();
		}
	}
	override public void OnPointerExit(PointerEventData eventData)
	{
		if(enabled)
		{
			base.OnPointerExit(eventData);
			CursorManager.instance.NormalMode();
		}
	}
	
	public CompositeText text;

	override protected void Awake()
	{
		base.Awake();
		text = GetComponentInChildren<CompositeText>();
	}
	
	public Vector2 hideVec;
	bool isHidden = false;
	public void Show()
	{
		if(isHidden)
		{
			GetComponent<RectTransform>().DOAnchorPos(-hideVec,0.5f);
			isHidden = false;
		}
	}
	public void Hide()
	{
		if(!isHidden)
		{
			GetComponent<RectTransform>().DOAnchorPos(hideVec,0.5f);
			isHidden = true;
		}
	}
}
public enum HandButtonType{Normal,Hand,Grab,Lock};