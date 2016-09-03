using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;


public class ItemSlot : MonoBehaviour,IPointerClickHandler,IDragHandler,IDropHandler,IBeginDragHandler,IPointerEnterHandler,IEndDragHandler{
	Image itemImage;
	public int index;
	//Equip equip;
	public CompositeText numText;
	[HideInInspector]
	public Item bindItem;
	
	void Awake()
	{
		itemImage = transform.Find("content").GetComponent<Image>();
		numText = GetComponentInChildren<CompositeText>();
	}
	public void setItem(Item item)
	{
		bindItem = item;
		if(item)
		{
			show();
			itemImage.SetNativeSize();
		}
		else
		{
			hide();
		}
		
	}
	void hide()
	{
		itemImage.sprite = SpriteManager.instance.emptySprite;
		numText.text = "";
	}
	void show()
	{
		itemImage.sprite = bindItem.asset.iconSprite;
		numText.text = bindItem.bindData.num.ToString();
	}
	public void changeNum(int itemNum)
	{
		int num = bindItem.bindData.num+=itemNum;
		numText.text = num.ToString(); 
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemUIManager.instance.itemTouched(index);
    }

	
    public void OnDrop(PointerEventData eventData)
    {
        ItemUIManager.instance.itemOnDrop();
    }
	
    public void OnDrag(PointerEventData eventData)
    {
		if(bindItem)
			ItemUIManager.instance.itemOnDrag(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
		
		if(bindItem)
		{
			ItemUIManager.instance.itemBeginDrag(this);
			hide();
		}
			
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ItemUIManager.instance.OnPointerEnter(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(!ItemUIManager.instance.itemEndDrag())
			show();
    }
}
