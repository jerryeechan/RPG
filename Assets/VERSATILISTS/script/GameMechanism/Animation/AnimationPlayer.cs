using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AnimationPlayer : MonoBehaviour {

	public AnimationUnit[] animationUnits;
	public float[] timeDelays;
	void Start()
	{
		int i=0;
		foreach(var unit in animationUnits)
		{
			this.myInvoke(timeDelays[i],()=>{
				unit.gameObject.SetActive(true);
			});
			i++;
		}
		
	}
}
