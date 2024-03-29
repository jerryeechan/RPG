﻿
namespace SmartLocalization.Editor{
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(LocalizedText))]
public class LocalizedTextInspector : Editor 
{
	
	private string selectedKey = null;
	
	void Awake()
	{
		LocalizedText textObject = ((LocalizedText)target);
		if(textObject != null)
		{
			selectedKey = textObject.localizedKey;
		}
		var languageValues = LanguageHandlerEditor.LoadParsedLanguageFile("zh-TW", false);
		if(languageValues.ContainsKey(selectedKey))
		{
			var localText = languageValues[selectedKey].TextValue;
			GUILayout.TextField(localText);
		}
	}
	public static string getLocalText()
	{
		return "aewr";
	}
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		//LanguageManager.Instance.GetSupportedLanguages();
		 
		var languageValues = LanguageHandlerEditor.LoadParsedLanguageFile("zh-TW", false);
		if(languageValues.ContainsKey(selectedKey))
		{
			var localText = languageValues[selectedKey].TextValue;
			GUILayout.TextField(localText);
		}
		
		
		
		selectedKey = LocalizedKeySelector.SelectKeyGUI(selectedKey, true, LocalizedObjectType.STRING);
		
		if(!Application.isPlaying && GUILayout.Button("Use Key", GUILayout.Width(70)))
		{
			LocalizedText textObject = ((LocalizedText)target);
			textObject.localizedKey = selectedKey;
		}
	}
	
}
}