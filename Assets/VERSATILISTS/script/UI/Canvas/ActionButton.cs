using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ActionButton : MonoBehaviour {

	// Use this for initialization
	int skillIndex;
	public	Character ch;
	//CharacterButton chButton;
	NumberImageText cdText;
	NumberImageText manaText;

	public Text skillNameText;
	bool isInited =false;
	Image disableMask;
	Image skillBtnIcon;
	void Awake()
	{
		skillNameText = GetComponent<Text>();
		/*
		cdText = transform.Find("cd").GetComponent<NumberImageText>();
		manaText = transform.Find("manacost").GetComponent<NumberImageText>();
		
		disableMask = transform.Find("DisableMask").GetComponent<Image>();
		skillBtnIcon = transform.Find("icon").GetComponent<Image>();
		*/
		btn =GetComponent<Button>();
	}
	public Action bindAction;
	public void setSkill(int id,Character ch, Action bindAciton)
	{
	
		if(!isInited)
		{
			cdText.init();
			manaText.init();
			isInited = true	;
		}
		this.ch = ch;
		skillIndex = id;
		this.bindAction = bindAction;
		//Text text = GetComponentInChildren<Text>(); 	
		//skillBtnIcon.sprite = bindAction.iconx;
		//skillBtnIcon.SetNativeSize();
		//cdText.setSprite(NumberGenerator.instance.getCDSprite(bindSkill.cd));
		//manaText.setSprite(NumberGenerator.instance.getManaSprite(bindSkill.mpCost()));
//		text.text = skill.name;
	}
	Color32 white = new Color32(255,255,255,0);
	Color32 black = new Color32(0,0,0,150);
	Color32 red = new Color32(255,0,0,150);
	public void Unlock()
	{
		ratio++;
		btn.interactable = true;
		
		highlightRaito();
		
		//disableMask.color = white;
	}
	void highlightRaito()
	{
		 
		if(ratio==1)
		{
			//skillNameText.color = Color.white;
		}
		else if(ratio == 2)
		{
			//skillNameText.color = Color.yellow;
		}
	}
	
	int ratio = 0;
	Button btn;
	public void Lock()
	{
		btn.interactable = false;
		ratio =0;
		highlightRaito();
		//disableMask.color = black;
	}
	public void selectButton()
	{
		skillNameText.color = Color.yellow;
	}

	public void deSelectButton()
	{
		skillNameText.color = Color.white;
	}
}
