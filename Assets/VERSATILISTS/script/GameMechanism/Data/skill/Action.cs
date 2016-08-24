using UnityEngine;
using System.Collections.Generic;
namespace com.jerry.rpg
{
	public class Action : MonoBehaviour {
		Skill[] skills;
		public string description;
		public CharacterAnimation chAnimation;
		[HideInInspector]
		public ActionData actionData;
		public Character caster;
		
		void Awake()
		{
			//foreach (Skill skill in  GetComponentsInChildren<Skill>())
			skills = GetComponentsInChildren<Skill>();
			foreach (var skill in skills)
			{
				skill.parentAction = this;
			}
		}
		public void move()
		{
			PlayActionAnimation();
		}
		void PlayActionAnimation()
		{
			//TODO:

			//temp
			OnActionAnimationDone();
		}
		void OnActionAnimationDone()
		{
			skills[0].init(caster);
			skills[0].DoEffect();
		}
	}
}