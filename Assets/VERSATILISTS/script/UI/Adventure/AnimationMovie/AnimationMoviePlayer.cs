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

	AdventureDialogueLineData currentLine;
	public void PlayLine()
	{
		if(currentLine == null)
		{
			currentLine = currentUnit.getNext();
			if(currentLine == null)
			{
				MovieDone();
			}
		}
		if(!dialoguePlayer.testPlaying)
		{
			dialoguePlayer.PlayLine(currentLine);
			currentLine = null;
		}
		
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
