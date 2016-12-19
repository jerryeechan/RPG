using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RosterButton : MonoBehaviour {
	[SerializeField]
	Image checkImage;
	public CharacterData bindChData;
	public int index;
	
	void Awake()
	{
		checkImage.enabled = false;
	}

	public void init(int index, CharacterData bindChData)
	{
		this.index = index;
		this.bindChData = bindChData;
	}

	bool _isChecked = false;
	public bool isChecked{
		set{
			_isChecked = value;
			checkImage.enabled = _isChecked;
		}
		get{
			return _isChecked;
		}
	}
	public void check()
	{
		isChecked = !isChecked;
	}

	public void click()
	{
		RosterBuilder.instance.rosterBtnClicked(this);
	}
}