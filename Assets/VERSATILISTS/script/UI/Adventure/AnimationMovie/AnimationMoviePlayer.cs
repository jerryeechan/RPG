using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMoviePlayer : MonoBehaviour {

	// Use this for initialization
	[SerializeField]
	DialoguePlayer dialoguePlayer;

	void Start()
	{
		PlayLine();
	}
	public void PlayMovie(MovieClip movieUnit)
	{
		 Instantiate(movieUnit);
	}

	public MovieClip currentUnit;

	public void PlayLine()
	{
		AdventureDialogueLineData lineData = currentUnit.getNext();
		if(lineData == null)
		{
			MovieDone();
		}
		else
			dialoguePlayer.PlayLine(lineData);
	}
	public void animationDoneCallBack()
	{
		dialoguePlayer.completeText();
	}
	bool isDone = false;
	public void MovieDone()
	{
		if(!isDone)
		{
			isDone = true;
			WTHSceneManager.instance.buildTeam();
		}
		
	}
}
