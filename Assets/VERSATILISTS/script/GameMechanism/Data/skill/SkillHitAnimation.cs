using UnityEngine;
using System.Collections;
using com.jerrch.rpg;
public class SkillHitAnimation : MonoBehaviour {

	public void SetSkill(Skill skill,Character target)
	{
		//TODO? 還不知道有用嗎
		_skill = skill;
	}
	Skill _skill;
	void HitTarget()
	{
 		//_skill.SkillDoEffect();
	}
	void PlayDone()
	{
		Destroy(transform.parent.gameObject);
	}
}
