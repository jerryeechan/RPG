using UnityEngine;
using System.Collections;
using DG.Tweening;
public class AnimationUnit : MonoBehaviour {

	Animator[] animators;
	
	public System.Action OnCompleteEvent;
	
	public float speed
	{
		set{
			foreach(var anim in animators)
				anim.speed = value;
		}
	}
	void Awake()
	{
		animators = GetComponentsInChildren<Animator>(true);
		if(animators == null)
		{
			Debug.LogError("no animator");
		}
	}
	public void shakeCamera()
	{
		Camera.main.DOShakePosition(0.2f,5,30);
		playDone();
	}
	bool isDone = false;
	public void playDone()
	{
		if(!isDone)
		{
			if(OnCompleteEvent != null)
			OnCompleteEvent();
			isDone = true;
		}
		//Debug.LogError("Play done");
		
	}
}
