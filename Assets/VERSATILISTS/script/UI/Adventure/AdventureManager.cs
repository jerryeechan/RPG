using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using com.jerrch.rpg;
public class AdventureManager : Singleton<AdventureManager> {

	public CompositeText targetName;
	public CompositeText dialogueLine;
	public Image targetImage;
	public Image bgImage;
	public AnimatableCanvas optionPanel;
	public AnimatableCanvas optionDisplayerPanel;
	public AnimatableCanvas dialoguePanel;

	AdventureOptionBtn[] optionBtns;
	AdventureOptionDisplayer[] optionDisplayers;

	AdventureDialogueData currentDialogue;
	AdventureEvent currentEvent;

	void Awake()
	{
		optionBtns = optionPanel.GetComponentsInChildren<AdventureOptionBtn>();
		optionDisplayers = optionDisplayerPanel.GetComponentsInChildren<AdventureOptionDisplayer>();
		optionPanel.hide();
		optionDisplayerPanel.hide();
	}
	public AdventureEvent testEvent;
	void Start()
	{
		encounterEvent(testEvent);
	}
	public void PlayDialogue(AdventureDialogueData dialogue)
	{
		currentDialogue = dialogue;
		dialogue.restart();
		PlayNextLine();
	}
	void ShowOptions(int num)
	{
		optionPanel.show();
		if(num == 2)
		{
			optionBtns[0].GetComponent<RectTransform>().SetPositionX(-45);
			optionBtns[1].GetComponent<RectTransform>().SetPositionX(45);
			optionBtns[2].gameObject.SetActive(false);
		}
		else
		{
			optionBtns[0].GetComponent<RectTransform>().SetPositionX(-85);
			optionBtns[1].GetComponent<RectTransform>().SetPositionX(0);
			optionBtns[2].gameObject.SetActive(true);
		}
		
		
	}
	void ShowOptionDisplayer(int num)
	{
		optionDisplayerPanel.show();
		if(num == 1)
		{
			optionDisplayers[0].GetComponent<RectTransform>().SetPositionX(0);
			optionDisplayers[1].gameObject.SetActive(false);
			optionDisplayers[2].gameObject.SetActive(false);
		}
		else if(num == 2)
		{
			optionDisplayers[0].GetComponent<RectTransform>().SetPositionX(-45);
			optionDisplayers[1].GetComponent<RectTransform>().SetPositionX(45);
			optionDisplayers[1].gameObject.SetActive(true);
			optionDisplayers[2].gameObject.SetActive(false);
		}
		else
		{
			optionDisplayers[0].GetComponent<RectTransform>().SetPositionX(-85);
			optionDisplayers[1].GetComponent<RectTransform>().SetPositionX(0);
			optionDisplayers[1].gameObject.SetActive(true);
			optionDisplayers[2].gameObject.SetActive(true);
		}
	}
	AdventureTarget currentTarget;
	bool isOptionDisplayed = false;

	void showDialogueOption()
	{
		ShowOptions(currentEvent.options.Length);
		for(int i = 0;i<currentEvent.options.Length;i++)
		{
			optionBtns[i].text.text = currentEvent.options[i].text;
			optionBtns[i].interactable = true;
		}
	}
	void showDetailOption()
	{
		dialoguePanel.hide();
		ShowOptions(currentEvent.options.Length);
		ShowOptionDisplayer(currentEvent.detailOptions.Length);
		for(int i=0;i<currentEvent.options.Length;i++)
		{
			optionBtns[i].text.text = currentEvent.options[i].text;
			optionBtns[i].interactable = true;
		}
		for(int i = 0;i<currentEvent.detailOptions.Length;i++)
		{
			optionDisplayers[i].setOption((currentEvent.detailOptions[i] as TakeItemOption).getItemInfo());
			//optionDisplayers[i].interactable = true;
		}
	}
	
	//dialogues
	public void PlayNextLine()
	{
		if(currentEventDone)
		{
			NextEvent();
		}
		AdventureDialogueLineData line = currentDialogue.nextLine();
		if(dialogueLine.isTyping==true)
		{
			dialogueLine.CompleteText();
		}
		if(line == null)
		{
			//end of dialogue
			//display option
			if(!isOptionDisplayed)
			{
				isOptionDisplayed = true;
				switch(currentEvent.type)
				{
					case AdventureEventType.Dialogue:
						showDialogueOption();
					break;
					case AdventureEventType.Reward:
						showDetailOption();
					break;
				}
			}
		}
		else
		{
			dialogueLine.DOText(line.text);
			
				
			if(line.target!=null)
			{
				currentTarget = line.target;
			}
			targetName.text = currentTarget.targetName;
			targetImage.sprite = currentTarget.sprite;
			targetImage.SetNativeSize();
		}
		
	}
	
	public void genAdventureEvents()
	{

	}

	
	public void encounterEvent(AdventureEvent advEvent)
	{
		currentEventDone = false;
		switch(advEvent.type)
		{
			case AdventureEventType.Dialogue:
				PlayDialogue(advEvent.dialogue);
			break;
		}
		isOptionDisplayed = false;
		optionPanel.hide();
		currentEvent = advEvent;
		
	}
	
	public void selectOption(int index)
	{
		currentEvent.options[index].choose();
		
	}
	public void selectDetail(int index)
	{
		currentEvent.detailOptions[index].choose();
	}

	void playDescription(string des)
	{
		dialoguePanel.show();
		optionDisplayerPanel.hide();
		dialogueLine.DOText(des);
	}
	public void optionSuccess(int index)
	{
		playDescription(currentEvent.options[index].successStr);
		EventDone();
	}
	
	public void optionFail(int index){
		playDescription(currentEvent.options[index].failStr);
		optionBtns[index].interactable = false;
	}

	void EventDone()
	{
		currentEventDone = true;
		optionPanel.hide();
	}
	bool currentEventDone = false;

	public AdventureEvent getEvent()
	{
		//TODO:
		//generate random events
		return testEvent;
	}
	
	public void NextEvent()
	{	
		if(currentEvent.triggerNextEvent)
		{
			encounterEvent(currentEvent.nextEvent);
		}
		else
		{
			UIManager.instance.ShowCover(()=>{
				encounterEvent(getEvent());
				UIManager.instance.HideCover();
			});
		}
		
	}
	
}