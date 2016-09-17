using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;


public class ItemSlot : MonoBehaviour,IPointerClickHandler,IDragHandler,IDropHandler,IBeginDragHandler,IPointerEnterHandler,IEndDragHandler{
	protected Image itemImage;
	public int index;
	//Equip equip;
	public CompositeText numText;
	//[HideInInspector]
 	Item _bindItem;
	HandButton btn;
	public Item bindItem{
		set{
			setItem(value);
		}
		get{return _bindItem;}
	}
	protected virtual void setItem(Item item)
	{
		_bindItem = item;
		if(_bindItem)
		{
			print(itemImage.sprite);
			print(item.asset.iconSprite);
			itemImage.sprite = item.asset.iconSprite;
			if(item.stackable)
			{
				if(numText)
					numText.text = item.bindData.num.ToString();
				
			}
			else
			{
				if(numText)
					numText.text = "";
			}
			

			itemImage.SetNativeSize();
			if(btn)
				btn.enabled = true;
		}
		else
		{
			itemImage.sprite = SpriteManager.instance.emptySprite;
			if(numText)
				numText.text = "";
			if(btn)
				btn.enabled = false;
		}
	}

	public bool isEmpty{
		get{
			return bindItem == null;
		}
	}
	public bool isBindEquip{
		get{
			if(bindItem)
				return bindItem.itemType == ItemType.Equip;
			else
				return false;
		}
	}
	public ItemSlotType slotType;
	
	protected virtual void Awake()
	{
		slotType = ItemSlotType.Item;
		itemImage = transform.Find("content").GetComponent<Image>();
		numText = GetComponentInChildren<CompositeText>();
		btn = GetComponent<HandButton>();
	}
	
	
	public void changeNum(int itemNum)
	{
		int num = bindItem.bindData.num+=itemNum;
		numText.text = num.ToString();
		if(num==0)
		{
			deleteItem();
		}
		
		
	}
	public void use()
	{
		bindItem.use();
		remove();
	}
	public void remove(bool all=false)
	{
		if(all==true)
		{
			deleteItem();
		}
		else
		{
			changeNum(-1);
		}
	}
	public void deleteItem()
	{
		bindItem.bindData.deleteData();
		bindItem = null;
	}
    public virtual void OnPointerClick(PointerEventData eventData)
    {
		
		if(eventData.clickCount == 2)
		{
			ItemUIManager.instance.useItem();
		}
		else if(eventData.clickCount==1)
		{
			ItemUIManager.instance.itemTouched(this);
			ItemUIManager.instance.showItem(true);
		}
        
    }
	Item hideItem;
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
		if(bindItem != null)
		{
			ItemUIManager.instance.itemBeginDrag(this);
			hideItem = bindItem;
			bindItem = null;
		}
			
    }
	 public virtual void OnDrag(PointerEventData eventData)
    {
		ItemUIManager.instance.itemOnDrag(eventData);
    }
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        ItemUIManager.instance.OnPointerEnter(this);
    }
	public virtual void OnDrop(PointerEventData eventData)
    {	
        ItemUIManager.instance.itemOnDrop(this);
    }
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        if(ItemUIManager.instance.itemEndDrag())
		{
		
		}
		else
		{
			bindItem = hideItem;
		}
    }
}

public enum ItemSlotType{
	Item,Equip
}