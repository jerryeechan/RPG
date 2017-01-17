using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.jerrch.rpg;
using UnityEngine.UI;
public class ActionDescription : ActionButton{
	[SerializeField]
	CompositeText description;
	public override Action bindAction
	{
		set{
			base.bindAction = value;
			description.text = value.description;
		}
	}
}
