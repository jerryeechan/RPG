using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AdventureOptionDisplayer : AdventureOptionBtn {

	Image icon;
	CompositeText titleText;
	CompositeText detailText;
	CompositeText descriptionText;
	CompositeText requirementText;
	public Item item;
	// Use this for initialization
	void Awake()
	{
		icon = transform.Find("icon").GetComponent<Image>();
		titleText = transform.Find("title").GetComponent<CompositeText>();
		detailText = transform.Find("detail").GetComponent<CompositeText>();
		descriptionText = transform.Find("description").GetComponent<CompositeText>();
		requirementText = transform.Find("requirement").GetComponent<CompositeText>();
	}
	public override void OnPointerClick(PointerEventData eventData)
    {
        if(interactable)
        {
            AdventureManager.instance.selectDetail(index);
            CursorManager.instance.NormalMode();
            interactable = false;
        }
    }
	public void setOption(Item item)
	{
		this.item = item;
		titleText.text = item.itemName;
		icon.sprite = item.asset.iconSprite;
		switch(item.itemType)
		{
			case ItemType.Equip:
				var equip = item as Equip;
				detailText.text = "Lv:";
				descriptionText.text = equip.description+equip.getEffectString();
				requirementText.text = equip.getRequirementString();
			break;
			case ItemType.Consume:
				
				detailText.text = "x"+item.bindData.num;
				descriptionText.text = item.description;
			break;
			case ItemType.Place:

			break;
		}
	}
}
 