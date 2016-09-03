using UnityEngine;
using System.Collections;

public class DescriptionUIManager : Singleton<DescriptionUIManager> {

	public CompositeText nameText;
	public CompositeText descriptionText;
	public void showItem(Item item)
	{
		if(item)
		{
			ItemData itemData = item.bindData;
			nameText.text = item.itemName;
			descriptionText.text = item.description;
		}
		
	}
}
