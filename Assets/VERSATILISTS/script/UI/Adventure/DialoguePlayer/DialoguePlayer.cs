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
	}
	public void hide()
	{
		npcRenderer.gameObject.SetActive(false);
	}
	public void PlayLine(AdventureDialogueLineData line)
	{
		if(dialogueTextUI.isTyping)
		{
			dialogueTextUI.CompleteText();
		}
		else
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
				iconSpr.transform.position = currentTarget.transform.position;
				changeNPCSprite(currentTarget.sprite);
			}
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
