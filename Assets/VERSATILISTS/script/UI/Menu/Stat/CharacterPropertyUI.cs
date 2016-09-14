using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class CharacterPropertyUI : CompositeText,IPointerEnterHandler,IPointerExitHandler{

	public string title;
	public string description;
	
	public bool isPercentage;
	

	
	public void setValue(float v)
	{
		string vText = v.ToString();
		if(isPercentage)
			vText = ""+v*100+"%";
		
		text = vText;
		
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		DescriptionUIManager.instance.show(title,description);
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		DescriptionUIManager.instance.hide();
	}
}
