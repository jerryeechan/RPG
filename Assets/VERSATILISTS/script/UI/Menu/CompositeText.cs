using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using SmartLocalization;
using com.jerrch.rpg;
public class CompositeText : MonoBehaviour {

	public string prefix;
	public string postfix;
	Font ori_font;
	Text[] texts;

	void init(){
		if(!isInited)
		{
			texts = GetComponentsInChildren<Text>();
			foreach(Text t in texts)
			{
				t.raycastTarget = false;
			}
			isInited = true;
		}
		text = _textValue;
		
	}
	bool isInited = false;
	void OnValidate()
	{
		init();
	}
	
	void Awake()
	{
		init();
		ori_font = texts[0].font;
	}
	void Start()
	{
		LanguageManager.Instance.OnChangeLanguage += OnLanguegeChanged;
	}
	public bool romanChOnly = true;

	void OnLanguegeChanged(LanguageManager languageManager)
	{
		text = _textValue;
		Font font = ori_font;
		if(!romanChOnly&&languageManager.CurrentlyLoadedCulture.languageCode=="zh-TW")
		{
			font = FontManager.instance.GetChineseFont();
		}
		
		foreach(Text t in texts)
		{
			t.font = font;
			//t.font = 
		}
	}

	public string _textValue;
	public string text{
		set{
			_textValue = value;
			foreach(Text t in texts)
			{
				string newString = "";
				if(prefix!="")
					newString = GetLocalText(prefix);
				newString+= GetLocalText(value)+GetLocalText(postfix);
				t.text = newString;
			}
		}
		get{
			return _textValue;
		}
	}
	public static string GetLocalText(string key)
	{
		if(LanguageManager.Instance)
		{
			string text = LanguageManager.Instance.GetTextValue(key);
			if(text!=null)
				return text;
		}
		return key;
	}
	/*
	string GetLocalText(string key)
	{
		if(LanguageManager.Instance)
		{
			string text = LanguageManager.Instance.GetTextValue(key);
			if(text!=null)
				return text;
		}
		return key;
	}*/

	public Color color{
		set{
			foreach(Text t in texts)
			{
				t.color = value;
			}
		}
	}

	public bool isTyping = false;
	public void DOText(string str)
	{
		_textValue = GetLocalText(str);
		if(isTyping == false)
		{
			isTyping = true;
			float duration = _textValue.Length*0.1f;
			foreach(Text t in texts)
			{
				t.text = "";
				
				t.DOText(_textValue,duration,true).OnComplete(()=>
				{
					isTyping = false;
				});
			}
		}
		else
		{
			CompleteText();
		}
		
	}
	public void CompleteText()
	{
		foreach(Text t in texts)
		{
			t.text = "";
			t.DOKill();
			t.text = _textValue;
		}
		isTyping = false;
	}
	public void PopText(string str)
	{
		
		foreach(Text t in texts)
		{
			//print("pop text");
			t.text = LanguageManager.Instance.GetTextValue(str);
			t.DOFade(0,0);
			pop(t);
		}
	}
	void pop(Text t)
	{	
		t.DOFade(1,0.5f).OnComplete(()=>{
			t.DOFade(0,0.5f).SetDelay(1);	
		});
	}
	public void DOValue(int startValue,int diff)
	{
		print("dovalue"+startValue+diff);
		StopCoroutine(number(startValue,diff));
		StartCoroutine(number(startValue,diff));
	}
	IEnumerator number(int startValue,int diff)
	{
		float inc = Mathf.Abs(diff)/diff;
		
		if(Mathf.Abs(diff)>1000)
		{
			inc*=7;
		}
		if(Mathf.Abs(diff)>30)
		{
			inc*=2;
		}
		
		print(inc);
		
		float finalValue = startValue+diff;

		float currentValue = startValue;
		float incAbs = Mathf.Abs(inc);
		//for(float i=startValue;i<=startValue+diff;i+=inc)
		while(Mathf.Abs(finalValue-currentValue)>incAbs)
		{
			
			currentValue+=inc;
			foreach(Text t in texts)
			{
				string newString = "";
					if(prefix!="")
						newString = LanguageManager.Instance.GetTextValue(prefix);
					newString += (int)currentValue + LanguageManager.Instance.GetTextValue(postfix);
					t.text = newString;
			}
			yield return new WaitForSeconds(0.05f);
		}
		foreach(Text t in texts)
		{
			string newString = "";
				if(prefix!="")
					newString = LanguageManager.Instance.GetTextValue(prefix);
				newString += startValue+diff + LanguageManager.Instance.GetTextValue(postfix);
				t.text = newString;
		}
	}
}
