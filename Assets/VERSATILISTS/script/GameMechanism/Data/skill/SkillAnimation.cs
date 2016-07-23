using UnityEngine;
using System.Collections;

public class SkillAnimation : MonoBehaviour {

	public void SetSkill(Skill skill)
	{
		_skill = skill;
	}
	Skill _skill;
	void HitTarget()
	{
 		_skill.SkillDoEffect();
	}
	void PlayDone()
	{
		Destroy(transform.parent.gameObject);
	}
}
