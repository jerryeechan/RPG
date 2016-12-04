using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace com.jerrch.rpg
{

public class ActionButton : MonoBehaviour{

	// Use this for initialization
	
	//CharacterButton chButton;
	public CompositeText cdText;
	public CompositeText manaText;
	public Image skillIcon;
	public Image diceTypeImage;
	protected Image disableMask;


	//should not use in public
	[SerializeField]
	Action _action;
	public Action bindAction{
		set{
			print("set bind action");
			_action = value;
			if(_action==null)
			{
				skillIcon.sprite = SpriteManager.instance.emptySprite;
				//disableMask.enabled = true;
				if(diceTypeImage)
					diceTypeImage.color = Color.black;
				manaText.transform.parent.gameObject.SetActive(false);
				cdText.transform.parent.gameObject.SetActive(false);
			}
			else 
			{
				skillIcon.sprite = _action.icon;
				print(_action.icon);
				//disableMask.enabled = false;
				if(diceTypeImage)
					diceTypeImage.color = ActionManager.getDiceTypeColor(_action.diceType);
				if(_action.isPassive)
				{
					showManaCD(false);
				}
				else
				{
					showManaCD(true);
					//manaText.text = ""+_action.minCost+"-"+_action.maxCost; 
					//_action.energyCost.ToString();
					cdText.text = _action.cd.ToString();
				}			
			}
		}
		get{
			return _action;
		}
	}
	void showManaCD(bool b)
	{
		manaText.transform.parent.gameObject.SetActive(b);
		cdText.transform.parent.gameObject.SetActive(b);	
	}
	protected virtual void OnValidate()
	{
		skillIcon = transform.Find("icon").GetComponentInChildren<Image>();
		cdText = transform.Find("cd").GetComponentInChildren<CompositeText>();
		manaText = transform.Find("mp").GetComponentInChildren<CompositeText>();
		disableMask = transform.Find("disableMask").GetComponent<Image>();
	}
	protected virtual void Awake()
	{
		skillIcon = transform.Find("icon").GetComponentInChildren<Image>();
		cdText = transform.Find("cd").GetComponentInChildren<CompositeText>();
		manaText = transform.Find("mp").GetComponentInChildren<CompositeText>();
		disableMask = transform.Find("disableMask").GetComponent<Image>();
	}
}
}