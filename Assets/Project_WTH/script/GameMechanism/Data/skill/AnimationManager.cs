using UnityEngine;
using System.Collections.Generic;

public class AnimationManager : Singleton<AnimationManager> {

	Dictionary<string,AnimationUnit> skillAnimationDict;
	Dictionary<string,AnimationUnit> skillHitAnimationDict;
	void Awake()
	{
		AnimationUnit[] skillAnims = transform.Find("skillAnimation").GetComponentsInChildren<AnimationUnit>(true);
		AnimationUnit[] skillHitAnims = transform.Find("skillHitAnimation").GetComponentsInChildren<AnimationUnit>(true);
		skillAnimationDict = new Dictionary<string,AnimationUnit>();
		skillHitAnimationDict = new Dictionary<string,AnimationUnit>();
		foreach(var anim in skillAnims)
		{	
			skillAnimationDict.Add(anim.name,anim);
		}

		foreach(var anim in skillAnims)
		{
//acti			print(anim.name);
			skillHitAnimationDict.Add(anim.name,anim);
		}

		chAnimationSpeed = new Dictionary<CharacterAnimation,float>();
		chAnimationSpeed.Add(CharacterAnimation.idle,0.5f);
		chAnimationSpeed.Add(CharacterAnimation.melee,1f);
		chAnimationSpeed.Add(CharacterAnimation.upmelee,1f);
		chAnimationSpeed.Add(CharacterAnimation.holdmelee,1f);
		chAnimationSpeed.Add(CharacterAnimation.die,0.5f);
	}
	public AnimationUnit getSkillHitEffect(string id)
	{
		if(skillHitAnimationDict.ContainsKey(id))
		return Instantiate(skillHitAnimationDict[id]);
		else
		{
//			Debug.LogError("no skill effect:"+id);
			return null;
		}
	}
	public AnimationUnit getSkillEffect(string id){
		if(skillAnimationDict.ContainsKey(id))
		{
			return Instantiate(skillAnimationDict[id]);	
		}
		else
		{
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
