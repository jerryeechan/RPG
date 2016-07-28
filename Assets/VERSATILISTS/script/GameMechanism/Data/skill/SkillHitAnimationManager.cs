using UnityEngine;
using System.Collections.Generic;

public class SkillHitAnimationManager : Singleton<SkillHitAnimationManager> {

	Dictionary<string,GameObject> skillAnimationDict;
	public GameObject genSkillAnimation(string id)
	{
		return JPoolManager.instance.GetObject(id+"HitEffect");
	}
}
