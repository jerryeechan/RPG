using UnityEngine;
using System.Collections.Generic;
using SmartLocalization;
public class LocalizationManager : Singleton<LocalizationManager> {
	public  List<SmartCultureInfo> availableLanguages;
	// Use this for initialization
	public int current;

	void OnValidate()
	{
		if(availableLanguages.Count>current)
			LanguageManager.Instance.ChangeLanguage(availableLanguages[current]);
	}
	
	public void init()
	{
		availableLanguages = LanguageManager.Instance.GetSupportedLanguages();
		LanguageManager.Instance.ChangeLanguage(availableLanguages[1]);
	}
	void Start()
	{
//		SmartCultureInfo systemLanguage = LanguageManager.Instance.GetDeviceCultureIfSupported();		
		init();
	}
}