using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class RosterCheckButton : MonoBehaviour{

	[SerializeField]
	Sprite uncheckedSprite;
	[SerializeField]
	Sprite checkedSprite;
	
	bool _isChecked = false;

	Image btnImage;
	void Awake()
	{
		btnImage = GetComponent<Image>();
	}
	public bool isChecked
	{
		set{
			_isChecked = value;
			setSprite();
		}
		get{
			return _isChecked;
		}
	}
	public void click(){
		isChecked = !isChecked;
		RosterBuilder.instance.checkBtnClicked(isChecked);
	}
	void setSprite()
	{
		if(isChecked)
			btnImage.sprite = checkedSprite;
		else
			btnImage.sprite = uncheckedSprite;
	}
}
