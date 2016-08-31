using UnityEngine;
using System.Collections;

public class AnimationUnit : MonoBehaviour {

	public void play()
	{
		
	}
	AnimationUnit OnComplete(System.Action action)
	{
		action();
		return this;
	}
}
