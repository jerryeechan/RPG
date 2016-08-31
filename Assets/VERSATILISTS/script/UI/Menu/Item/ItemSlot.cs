using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public enum ItemType
{
	Quest,Equip,Consume
}
public class ItemSlot : MonoBehaviour,IPointerClickHandler{
	Image itemImage;
	public int index;
	//Equip equip;
	public Item bindItem;
	public CompositeText numText;
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
	public void changeNum(int itemNum)
	{
		int num = bindItem.bindData.num+=itemNum;
		numText.text = num.ToString(); 
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemUIManager.instance.itemTouched(index);
    }
}
