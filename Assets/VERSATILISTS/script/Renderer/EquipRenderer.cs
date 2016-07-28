using UnityEngine;
using System.Collections;

public class EquipRenderer : MonoBehaviour {
	SpriteRenderer spr;
	Animator anim;
	AnimatorOverrideController overrideController;
	public AnimationClip[] clips;
	// Use this for initialization
	void Awake()
	{
		spr = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		overrideController = new AnimatorOverrideController();
		overrideController.runtimeAnimatorController = anim.runtimeAnimatorController;
		changeEquip(clips);
	}
	public void changeEquip(Sprite sp)
	{
		if(sp==null)
		return;
		spr.sprite = sp;
	}
	public void changeEquip(AnimationClip[] clips)
	{
		if(clips==null)
			return;
		for(int i=0;i<clips.Length;i++)
		{
			overrideController[overrideController.clips[0].originalClip.name] = clips[i];
		}
		anim.runtimeAnimatorController = overrideController;
		
		//overrideController["attack"] = clips[1];
	}

	
}
