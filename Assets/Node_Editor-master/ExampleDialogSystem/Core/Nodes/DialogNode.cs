using System;
using NodeEditorFramework;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using SmartLocalization;

[Node(false, "Dialog/Dialog Node", new Type[] { typeof(DialogNodeCanvas) })]
public class DialogNode : BaseDialogNode
{
    public static Dictionary<string, LocalizedObject> localTexts;
    private const string Id = "dialogNode";
    public override string GetID { get { return Id; } }
    public override Type GetObjectType { get { return typeof(DialogNode); } }
    
    public override Node Create(Vector2 pos)
    {
        DialogNode node = CreateInstance<DialogNode>();
        
        node.rect = new Rect(pos.x, pos.y, 300, 210);
        node.name = "Dailog Node";

        //Previous Node Connections
        node.CreateInput("Previous Node", "DialogForward", NodeSide.Top, 30);
        node.CreateOutput("Back Node", "DialogBack", NodeSide.Top, 50);

        //Next Node to go to
        node.CreateOutput("Next Node", "DialogForward", NodeSide.Bottom, 30);
        node.CreateInput("Return Node", "DialogBack", NodeSide.Bottom, 50);

        node.SayingCharacterName = "ch name";
        node.WhatTheCharacterSays = "delete";
        node.SayingCharacterPotrait = null;
        node.lines = new List<String>();

        return node;
    }

//display local text of dialog id 
    int lineCount;
    protected internal override void NodeGUI()
    {
        var so = new SerializedObject(this);
        
        GUILayout.BeginHorizontal();

        SayingCharacterName = EditorGUILayout.TextField("Character Name", SayingCharacterName);
        //EditorGUILayout.PropertyField()
        GUILayout.EndHorizontal();
        
        /*
        GUILayout.BeginHorizontal();
        WhatTheCharacterSays = EditorGUILayout.TextField(WhatTheCharacterSays, GUILayout.Height(100));
        GUILayout.EndHorizontal();
        */
        
        GUILayout.BeginHorizontal();
        //EditorGUILayout.IntField(lines.Count,GUILayout.Height(10));
        GUILayout.EndHorizontal();

        var linesProp = so.FindProperty("lines");
        Show(linesProp,EditorListOption.ListSize|EditorListOption.Buttons);
        
        if(lines!=null)
        {
            for(int i=0;i<lines.Count;i++)
            {
                GUILayout.BeginHorizontal();
                LocalizedObject obj;
                if(localTexts.TryGetValue(lines[i], out obj))
                {
                    var line = obj.TextValue;
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.TextArea(line);
                    EditorGUI.EndDisabledGroup();
                }
                
                GUILayout.EndHorizontal();
            }
        }
        
        so.ApplyModifiedProperties();

        GUILayout.BeginHorizontal();

        SayingCharacterPotrait = EditorGUILayout.ObjectField("Character Potrait", SayingCharacterPotrait,
            typeof(Sprite), false) as Sprite;

        GUILayout.EndHorizontal();
    }
    public static void Show (SerializedProperty list, EditorListOption options = EditorListOption.Default) {
		if (!list.isArray) {
			EditorGUILayout.HelpBox(list.name + " is neither an array nor a list!", MessageType.Error);
			return;
		}

		bool
			showListLabel = (options & EditorListOption.ListLabel) != 0,
			showListSize = (options & EditorListOption.ListSize) != 0;

		if (showListLabel) {
			EditorGUILayout.PropertyField(list);
			EditorGUI.indentLevel += 1;
		}
		if (!showListLabel || list.isExpanded) {
			SerializedProperty size = list.FindPropertyRelative("Array.size");
			if (showListSize) {
				EditorGUILayout.PropertyField(size);
			}
			if (size.hasMultipleDifferentValues) {
				EditorGUILayout.HelpBox("Not showing lists with different sizes.", MessageType.Info);
			}
			else {
				ShowElements(list, options);
			}
		}
		if (showListLabel) {
			EditorGUI.indentLevel -= 1;
		}
	}
    private static GUIContent
		moveButtonContent = new GUIContent("\u21b4", "move down"),
		duplicateButtonContent = new GUIContent("+", "duplicate"),
		deleteButtonContent = new GUIContent("-", "delete");
		
	private static void ShowElements (SerializedProperty list, EditorListOption options) {
		bool
			showElementLabels = (options & EditorListOption.ElementLabels) != 0,
			showButtons = (options & EditorListOption.Buttons) != 0;

		for (int i = 0; i < list.arraySize; i++) {
			//if (showButtons) {
				EditorGUILayout.BeginHorizontal();
			//}
			if (showElementLabels) {
				EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
			}
			else {
                var prop = list.GetArrayElementAtIndex(i);
				EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), GUIContent.none);
                if(prop.propertyType == SerializedPropertyType.String)
                {
                    LocalizedObject obj;
                    if(localTexts.TryGetValue(prop.stringValue, out obj))
                    {
                        var line = obj.TextValue;
                        EditorGUILayout.LabelField(line,new GUILayoutOption[]{ GUILayout.MaxWidth(100.0f), GUILayout.MinWidth(10.0f)});
                    }
                }
			}
			if (showButtons) {
				ShowButtons(list, i);
			}
            EditorGUILayout.EndHorizontal();
		}
	}
 	private static GUILayoutOption miniButtonWidth = GUILayout.Width(20f);

	private static void ShowButtons (SerializedProperty list, int index) {
		if (GUILayout.Button(moveButtonContent, EditorStyles.miniButtonLeft, miniButtonWidth)) {
			list.MoveArrayElement(index, index + 1);
		}
		if (GUILayout.Button(duplicateButtonContent, EditorStyles.miniButtonMid, miniButtonWidth)) {
			list.InsertArrayElementAtIndex(index);
		}
		if (GUILayout.Button(deleteButtonContent, EditorStyles.miniButtonRight, miniButtonWidth)) {
			int oldSize = list.arraySize;
			list.DeleteArrayElementAtIndex(index);
			if (list.arraySize == oldSize) {
				list.DeleteArrayElementAtIndex(index);
			}
		}
	}

    public override BaseDialogNode Input(int inputValue)
    {
        switch (inputValue)
        {
            case (int)EDialogInputValue.Next:
                if (Outputs[1].GetNodeAcrossConnection() != default(Node))
                    return Outputs[1].GetNodeAcrossConnection() as BaseDialogNode;
                break;
            case (int)EDialogInputValue.Back:
                if (Outputs[0].GetNodeAcrossConnection() != default(Node))
                    return Outputs[0].GetNodeAcrossConnection() as BaseDialogNode;
                break;
        }
        return null;
    }

    public override bool IsBackAvailable()
    {
        return Outputs[0].GetNodeAcrossConnection() != default(Node);
    }

    public override bool IsNextAvailable()
    {
        return Outputs[1].GetNodeAcrossConnection() != default(Node);
    }
}

[Flags]
public enum EditorListOption {
	None = 0,
	ListSize = 1,
	ListLabel = 2,
	ElementLabels = 4,
	Buttons = 8,
	Default = ListSize | ListLabel | ElementLabels,
	NoElementLabels = ListSize | ListLabel,
	All = Default | Buttons
}