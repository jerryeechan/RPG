using UnityEngine;
using System.Collections.Generic;

public class AnimationManager : Singleton<AnimationManager> {

	Dictionary<string,AnimationUnit> skillAnimationDict;
	Dictionary<string,AnimationUnit> actionAnimationDict;
	void Awake()
	{
		AnimationUnit[] actionAnims = transform.Find("actionAnimation").GetComponentsInChildren<AnimationUnit>(true);
		AnimationUnit[] skillAnims = transform.Find("skillHitAnimation").GetComponentsInChildren<AnimationUnit>(true);
		skillAnimationDict = new Dictionary<string,AnimationUnit>();
		actionAnimationDict = new Dictionary<string,AnimationUnit>();
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
		chAnimationSpeed.Add(CharacterAnimation.upmelee,1f);
		chAnimationSpeed.Add(CharacterAnimation.holdmelee,1f);
		chAnimationSpeed.Add(CharacterAnimation.die,0.5f);
	}
	public AnimationUnit getSkillHitEffect(string id)
	{
		if(skillAnimationDict.ContainsKey(id))
		return Instantiate(skillAnimationDict[id]);
		else
		{
//			Debug.LogError("no skill effect:"+id);
			return null;
		}
	}
	public AnimationUnit getActionEffect(string id){
		if(actionAnimationDict.ContainsKey(id))
		{
			return Instantiate(actionAnimationDict[id]);	
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
