using UnityEngine;
using System.Collections.Generic;

public class AnimationManager : Singleton<AnimationManager> {

	Dictionary<string,Animator> skillAnimationDict;
	Dictionary<string,Animator> actionAnimationDict;
	void Awake()
	{
		Animator[] actionAnims = transform.Find("actionAnimation").GetComponentsInChildren<Animator>();
		Animator[] skillAnims = transform.Find("skillHitAnimation").GetComponentsInChildren<Animator>();
		skillAnimationDict = new Dictionary<string,Animator>();
		actionAnimationDict = new Dictionary<string,Animator>();
		foreach(var anim in actionAnims)
		{	
			actionAnimationDict.Add(anim.name,anim);
			
		}

		foreach(var anim in skillAnims)
		{
//acti			print(anim.name);
			skillAnimationDict.Add(anim.name,anim);
		}

		chAnimationSpeed = new Dictionary<CharacterAnimation,float>();
		chAnimationSpeed.Add(CharacterAnimation.idle,0.5f);
		chAnimationSpeed.Add(CharacterAnimation.melee,1f);
	}
	public Animator getSkillHitEffect(string id)
	{
		if(skillAnimationDict.ContainsKey(id))
		return Instantiate(skillAnimationDict[id]);
		else
		{
			Debug.LogError("no skill effect:"+id);
			return null;
		}
	}
	public Animator getActionEffect(string id){
		if(actionAnimationDict.ContainsKey(id))
		{
			return Instantiate(actionAnimationDict[id]);	
		}
		else
		{
			Debug.LogError("no action effect:"+id);
			return null;
		}
	}

	Dictionary<CharacterAnimation,float> chAnimationSpeed;
	public static float getChAnimSpeed(CharacterAnimation chAnim)
	{
		return instance.chAnimationSpeed[chAnim];
	}
	/*
	public GameObject genSkillAnimation(string id)
	{
		return JPoolManager.instance.GetObject(id+"HitEffect");
	}
	*/
}
