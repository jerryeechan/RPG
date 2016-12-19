using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEventTransmitter : MonoBehaviour {

	public void hitTarget()
	{
		GetComponentInParent<AnimationUnit>().shakeCamera();
	}
}
