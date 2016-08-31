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
		
		//changeEquip(clips);
	}
	public void restart()
	{
		anim.Play("idle");
	}
	public void wearEquip(Sprite sp)
	{
		if(sp==null)
		return;
		spr.sprite = sp;
	}
	public void wearEquip(AnimationClip[] clips)
	{
		if(clips==null)
			return;
			
		for(int i=0;i<clips.Length;i++)
		{
			print(clips[i].name);
			overrideController[overrideController.clips[i].originalClip.name] = clips[i];
		}
		anim.runtimeAnimatorController = overrideController;
		
		//overrideController["attack"] = clips[1];
	}

	
}
