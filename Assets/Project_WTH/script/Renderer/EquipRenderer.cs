using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class EquipRenderer : ReSkinRenderer {

	Animator anim;
	AnimatorOverrideController overrideController;
	public EquipType type;
	// Use this for initialization
	public EquipGraphicAsset bindEquipGraphic;
	void Awake()
	{
		spr = GetComponent<SpriteRenderer>();
		if(bindEquipGraphic)
		{
			wearEquip(bindEquipGraphic);
		}
		//anim = GetComponent<Animator>();
		//overrideController = new AnimatorOverrideController();
		//overrideController.runtimeAnimatorController = anim.runtimeAnimatorController;
		
		//changeEquip(clips);
	}
	void OnValidate()
	{
		if(bindEquipGraphic)
		{
			wearEquip(bindEquipGraphic);
		}
	}
	public void wearEquip(EquipGraphicAsset equipGraphic)
	{
		//bindEquip = equip;
		bindEquipGraphic = equipGraphic;
		getSprites(equipGraphic.classesType.ToString(),equipGraphic.suitName,equipGraphic.equipTypeName);
	}
	public void wearEquip(AnimationClip[] clips)
	{
		if(clips==null)
			return;
		
		/*
		for(int i=0;i<overrideController.clips.Length;i++)
		{
			switch(overrideController.clips[i].originalClip.name)
			{
				case "idle":
					
					overrideController[overrideController.clips[i].originalClip.name] = clips[0];
				break;
				case "melee":
					
					overrideController[overrideController.clips[i].originalClip.name] = clips[1];
				break;
				case "upmelee":
					
					if(clips.Length>2)
						overrideController[overrideController.clips[i].originalClip.name] = clips[2];
				break;
				case "holdmelee":
					
					if(clips.Length>3)
						overrideController[overrideController.clips[i].originalClip.name] = clips[3];
				break;
			}
		}
		anim.runtimeAnimatorController = overrideController;
		*/
	}

	
}
