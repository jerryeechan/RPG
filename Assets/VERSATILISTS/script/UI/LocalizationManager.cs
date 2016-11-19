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
		SmartCultureInfo systemLanguage = LanguageManager.Instance.GetDeviceCultureIfSupported();
		print(systemLanguage);
		
		availableLanguages = LanguageManager.Instance.GetSupportedLanguages();
		
		
	}
	// Update is called once per frame
	void Update () {
	
	}
}