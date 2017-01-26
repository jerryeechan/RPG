using UnityEngine;
using System.Collections;

public class EquipRenderer : ReSkinRenderer {

	Animator anim;
	AnimatorOverrideController overrideController;
	public EquipType type;
	// Use this for initialization
	void Awake()
	{
		spr = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		overrideController = new AnimatorOverrideController();
		overrideController.runtimeAnimatorController = anim.runtimeAnimatorController;
		
		//changeEquip(clips);
	}
	public void restart()
	{
		//anim.Stop();
		anim.StartPlayback();
		print("restart");
	}
	public void wearEquip(Equip equip)
	{
		getSprites(equip.bindClasses.ToString()+"/"+equip.id);
	}
	public void wearEquip(AnimationClip[] clips)
	{
		if(clips==null)
			return;
		
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
//			overrideController[overrideController.clips[i].originalClip.name] = clips[i];
		}
		anim.runtimeAnimatorController = overrideController;
		
		//overrideController["attack"] = clips[1];
	}

	
}
