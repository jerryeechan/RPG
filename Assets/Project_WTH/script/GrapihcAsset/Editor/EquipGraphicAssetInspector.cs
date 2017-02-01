using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EquipGraphicAsset))]
public class EquipGraphicAssetInspector : Editor{

	public EquipGraphicAsset eqGraphic;
		
	public void OnEnable () 
	{
		eqGraphic = (EquipGraphicAsset)target;
	}
	public Sprite[] sprites;
	public override void OnInspectorGUI () 
	{	
		sprites = Resources.LoadAll<Sprite>("Characters/"+eqGraphic.classesType.ToString()+"/"+eqGraphic.suitName+"/"+eqGraphic.equipTypeName);
		if(sprites.Length==0)
			EditorGUILayout.HelpBox("no corresponding sprites",MessageType.Error);
		else
		{
			eqGraphic.loadSpirtes(sprites);
		}
		base.OnInspectorGUI();
	}
}
