using UnityEngine;
using System.Collections.Generic;
using SmartLocalization;
public class LocalizationManager : MonoBehaviour {
	public  List<SmartCultureInfo> availableLanguages;
	// Use this for initialization
	void Awake () {
	
	
	}
	public int current;

	void OnValidate()
	{
		if(availableLanguages.Count>current)
			LanguageManager.Instance.ChangeLanguage(availableLanguages[current]);
	}
		
	void Start()
	{
//		SmartCultureInfo systemLanguage = LanguageManager.Instance.GetDeviceCultureIfSupported();		
		availableLanguages = LanguageManager.Instance.GetSupportedLanguages();
		
		LanguageManager.Instance.ChangeLanguage(availableLanguages[1]);
	}
	// Update is called once per frame
	void Update () {
	
	}
}