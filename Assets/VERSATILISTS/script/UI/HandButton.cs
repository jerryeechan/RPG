using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
public class HandButton : Button,IMoveHandler{
	
	
	Image mask;
	
	override public void OnPointerEnter(PointerEventData eventData)
	{
		
		if(enabled&&interactable)
		{
			base.OnPointerEnter(eventData);
			CursorManager.instance.PointerMode();
		}
	}
	override public void OnPointerExit(PointerEventData eventData)
	{
		if(enabled&&interactable)
		{
			base.OnPointerExit(eventData);
			CursorManager.instance.NormalMode();
		}
			
	}

	public bool maskEnable{
		set{
			if(mask)
			{
				mask.enabled = value;	
			}
		}
		get{
			if(mask)
			{
				return mask.enabled;
			}
			else 
				return false;
		}
	}
	
	public CompositeText text;

	override protected void Awake()
	{
		base.Awake();
		text = GetComponentInChildren<CompositeText>();
		var maskT = transform.Find("mask"); 
		if(maskT)
			mask = maskT.GetComponent<Image>();
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