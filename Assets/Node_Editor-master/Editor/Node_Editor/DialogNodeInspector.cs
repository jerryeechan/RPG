using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using SmartLocalization.Editor;
using NodeEditorFramework;



	[CustomEditor (typeof(DialogNode))]
	public class DialogNodeInspector : Editor
	{
		public DialogNode node;
		
		public void OnEnable () 
		{
			node = (DialogNode)target;
		}

		public override void OnInspectorGUI () 
		{
			var languageValues = LanguageHandlerEditor.LoadParsedLanguageFile("zh-TW", false);
			for(int i=0;i<node.localLines.Count;i++)
			{
				node.localLines[i] = languageValues[node.lines[i]].TextValue;
			}
		}
	}
