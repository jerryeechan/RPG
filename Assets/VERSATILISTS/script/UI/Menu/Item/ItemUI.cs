using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public enum ItemType
{
	Quest,Equip,Consume
}
public class ItemUI : MonoBehaviour,IPointerClickHandler{
	Image itemImage;
	public int index;
	//Equip equip;
	public Item bindItem;
	void Awake()
	{
		itemImage = transform.Find("content").GetComponent<Image>();
	}
	public void setItem(Item item)
	{
		bindItem = item;
		itemImage.sprite = item.asset.iconSprite;
		itemImage.SetNativeSize();
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemUIManager.instance.itemTouched(index);
    }
}
