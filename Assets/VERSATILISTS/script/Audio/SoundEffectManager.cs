using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : Singleton<SoundEffectManager> {

	AudioSource source;
	void Awake()
	{
		source = GetComponent<AudioSource>();
	}
	// Use this for initialization
	public void playSound(AudioClip clip)
	{
		source.PlayOneShot(clip,1);
	}

	[SerializeField]
	AudioClip UISound;
	[SerializeField]
	AudioClip DropSound;
	[SerializeField]
	AudioClip PressSound;
	public void playSound(BasicSound basicSound)
	{
		switch(basicSound)
		{
			case BasicSound.UI:
				playSound(UISound);
			break;
			case BasicSound.Drop:
				playSound(DropSound);
			break;
			case BasicSound.Press:
				playSound(PressSound);
			break;
		}
	}
}

public enum BasicSound{UI,Drop,Press};