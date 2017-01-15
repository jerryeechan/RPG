using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.jerrch.rpg;
using DG.Tweening;
public class ShopUIManager : SingletonCanvas<ShopUIManager> ,IItemSlotManager{

	[SerializeField]
	ItemSlot slotTemplate;
	[SerializeField]
	Image indicator;

	[SerializeField]
	DescriptionUIManager descriptionPanel;
	[SerializeField]
	Image itemImage;

	[SerializeField]
	CompositeText priceText;
	[SerializeField]
	CompositeText appraiseText;
	[SerializeField]
	HandButton buyButton;
//setup
	
	protected override void Awake()
	{
		base.Awake();
		generateSlots();
	}
	

	int currentSlotIndex = 0;
	public void init()
	{
		currentSlotIndex = 0;
		setItems(ItemManager.instance.generateShopItem(0));
		setItems(EquipManager.instance.generateShopEquip(0));
		setNullItem();
		itemSlotTouched(0);
	}

	void setItems(List<Item> items)
	{
		//int stop = currentSlotIndex + items.Count;
		foreach(var item in items)
		{
			setItemToSlot(slots[currentSlotIndex],item);
			currentSlotIndex++;
		}
	}
	void setNullItem()
	{
		print(currentSlotIndex);
		for(;currentSlotIndex<slots.Count;currentSlotIndex++)
		{
			setItemToSlot(slots[currentSlotIndex],null);
		}
	}
	protected void setItemToSlot(ItemSlot slot,Item item)
	{
		//virtual
		slot.bindItem = item;

		//override
		if(item==null)
		{
			//slot.priceText.GetComponentInChildren<Image>().gameObject.SetActive(false);	
		}
		else
		{
			//slot.priceText.GetComponentInChildren<Image>().gameObject.SetActive(true);
		}
	}
	

	void generateSlots()
	{
		int id = 0;
		slots = new List<ItemSlot>();
		for(int j=0;j<4;j++)
			for(int i=0;i<4;i++)
			{
				var newSlot = Instantiate(slotTemplate);
				newSlot.transform.SetParent(transform.Find("shopbkg"));
				newSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(3+26*i,-3-32*j);
				newSlot.index = id;
				newSlot.manager = this;
				id++;
				slots.Add(newSlot);
			}		
		indicator.transform.SetAsLastSibling();
	}

//in game
	
	public void buyBtnTouched()
	{
		ItemManager.instance.playerGetItem(slots[selectedIndex].bindItem);
		PlayerStateUI.instance.getGold(-slots[selectedIndex].bindItem.price);
		setItemToSlot(slots[selectedIndex],null);
	}
	
	List<ItemSlot> slots;

	int selectedIndex;
	public void itemSlotTouched(int index)
	{
		var item = slots[index].bindItem;
		if(item != null)
		{
			descriptionPanel.showItem(item);
			priceText.text = item.price.ToString();	
			itemImage.sprite = item.asset.iconSprite;
			appraiseText.text = item.recommendationText;
			setIndicator(index);
			selectedIndex = index;

			if(PlayerStateUI.instance.gold>=item.price)
			{
				buyButton.interactable = true;
			}
			else
			{
				buyButton.interactable = false;
			}
		}
	}
	void setIndicator(int index)
	{
		indicator.GetComponent<RectTransform>().DOAnchorPos(slots[index].GetComponent<RectTransform>().anchoredPosition,0.2f);
	}
	
	
}
