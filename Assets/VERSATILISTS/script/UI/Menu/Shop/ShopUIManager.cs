using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.jerrch.rpg;
using DG.Tweening;
public class ShopUIManager : Singleton<ShopUIManager> ,IItemSlotManager{

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
	
	void Awake()
	{
		generateSlots();
	}
	void Start()
	{
		init();
	}

	public void init()
	{
		setItems(ItemManager.instance.generateShopItem(0));
		itemSlotTouched(0);
	}

	void setItems(List<Item> items)
	{
		int i;
		for (i = 0; i < items.Count; i++)
		{
			setItemToSlot(slots[i],items[i]);
		}
		for(;i<slots.Count;i++)
		{
			setItemToSlot(slots[i],null);
		}
	}
	protected void setItemToSlot(ItemSlot slot,Item item)
	{
		//virtual
		slot.bindItem = item;

		//override
		if(item==null)
		{
			slot.priceText.GetComponentInChildren<Image>().gameObject.SetActive(false);	
		}
		else
		{
			slot.priceText.GetComponentInChildren<Image>().gameObject.SetActive(true);
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
				newSlot.transform.SetParent(transform);
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
