using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePlayer : MonoBehaviour {

	[SerializeField]
	CompositeText dialogueTextUI;
	[SerializeField]
	CompositeText targetNameTextUI;
	[SerializeField]
	SpriteRenderer npcRenderer;
	AdventureTarget currentTarget;
	public void show()
	{
		npcRenderer.gameObject.SetActive(true);
		iconSpr.gameObject.SetActive(true);
	}
	public void hide()
	{
		npcRenderer.gameObject.SetActive(false);
		iconSpr.gameObject.SetActive(false);
	}
	public bool testPlaying
	{
		get{
			if(dialogueTextUI.isTyping)
			{
				dialogueTextUI.CompleteText();
				return true;
			}
			else
				return false;
			
		}
	}
	public void init()
	{
		iconSpr.enabled = false;
		npcRenderer.sprite = null;
		targetNameTextUI.text = "";
		dialogueTextUI.text = "";
	}
	public void PlayLine(AdventureDialogueLineData line)
	{
		dialogueTextUI.DOText(line.text);
		if(line.target!=null)
		{
			currentTarget = line.target;
		}

		if(currentTarget)
		{
			if(targetNameTextUI)
				targetNameTextUI.text = currentTarget.targetName;
			iconSpr.enabled = true;
			iconSpr.transform.position = currentTarget.transform.position;
			changeNPCSprite(currentTarget.sprite);
		}
		
	}

	
	[SerializeField]
	SpriteRenderer iconSpr;
	public void PlayText(string str)
	{
		dialogueTextUI.DOText(str);
	}
	
	void changeNPCSprite(Sprite sp)
	{
		//TODO FADE
		if(npcRenderer&&sp!=null)
			npcRenderer.sprite = sp;
	}
	public void completeText()
	{
		dialogueTextUI.CompleteText();
	}
	
	/*
	public void PlayNextLine()
	{
		AdventureDialogueLineData line = currentDialogue.nextLine();
		if(dialogueLine.isTyping==true)
		{
			dialogueLine.CompleteText();	
		}
		if(line == null)
		{	

			//next
		}
		else
		{
			dialogueLine.DOText(line.text);				
			if(line.target!=null)
			{
				currentTarget = line.target;
			}
			targetName.text = currentTarget.targetName;
			changeNPCSprite(currentTarget.sprite);
		}
	}
	*/
}
