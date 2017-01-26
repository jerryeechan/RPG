using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.jerrch.rpg;
using UnityEngine.UI;

//DELETE
public class SkillDescription : SkillButton{
	[SerializeField]
	CompositeText description;
	public override Skill bindSkill
	{
		set{
			base.bindSkill = value;
			description.text = value.description;
		}
	}
}
