using UnityEngine;
using System.Collections;
using com.jerrch.rpg;
public class DescriptionUIManager : Singleton<DescriptionUIManager> {

	public CompositeText nameText;
	public CompositeText descriptionText;
	RectTransform rect;
	void Awake()
	{
		rect = GetComponent<RectTransform>();
		//testBound();
	}
	public void showItem(Item item)
	{
		if(item)
		{
			//ItemData itemData = item.bindData;
			nameText.text = item.itemName;
			descriptionText.text = item.description;
		}
		
	}
	public void show(string title,string description)
	{
		nameText.text = title;
		descriptionText.text = description;

	}
	public void hide()
	{
		nameText.text = "";
		descriptionText.text = "";
	}
/*
	void testBound()
	{
		rect.anchorMax = new Vector2(0.5f,1);
		rect.anchorMin = new Vector2(0.5f,1);
		rect.pivot = new Vector2(0.5f,1);
		rect.anchoredPosition = Vector2.zero;
		top = rect.localPosition;

		rect.anchorMax = new Vector2(0.5f,0);
		rect.anchorMin = new Vector2(0.5f,0);
		rect.pivot = new Vector2(0.5f,0);
		bottom = rect.localPosition;

		rect.anchorMax = new Vector2(0.5f,0.5f);
		rect.anchorMin = new Vector2(0.5f,0.5f);
		
	}
	Vector3 top;
	Vector3 bottom;
	*/
}
