using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using com.jerrch.rpg;
public class AdventureManager : Singleton<AdventureManager> {

//UI
	public CompositeText targetName;
	//public CompositeText dialogueLine;
	
	public AnimatableCanvas optionPanel;
	public AnimatableCanvas optionDisplayerPanel;
	public AnimatableCanvas dialoguePanel;
	public DialoguePlayer dialoguePlayer;
//Renderer
	
	public void show()
	{
		dialoguePlayer.show();
	}
	public void hide()
	{
		dialoguePlayer.hide();
	}
// vars
	public AdventureStage[] stages;
	int currentStageIndex=0;

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
		//TODO the first event???
		currentScene = currentStage.getScene();
		print(currentScene.name);
		encounterEvent(currentScene.getEvent());
		//encounterEvent(testEvent);
	}
	public void PlayDialogue(AdventureDialogueData dialogue)
	{
		currentDialogue = dialogue;
		dialoguePanel.show();
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
		if(currentEvent.options!=null&&currentEvent.options.Length>0)
		{
			ShowOptions(currentEvent.options.Length);
			for(int i = 0;i<currentEvent.options.Length;i++)
			{
				optionBtns[i].text.text = currentEvent.options[i].text;
				optionBtns[i].interactable = true;
			}
		}
		
	}
	void showDetailOption()
	{
		dialoguePanel.hide();
		//ShowOptions(currentEvent.options.Length);
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
		AdventureDialogueLineData line = currentDialogue.nextLine();
		if(currentEventDone)
		{
			NextEvent();
		}
		
		if(line == null)
		{	
			//temp demo solution
			if(!currentEvent.hasOption)
			{
				NextEvent();
			}
			//end of temp demo solution
			
			//end of dialogue
			//display option and wait
			if(!isOptionDisplayed)
			{
				isOptionDisplayed = true;
				switch(currentEvent.type)
				{
					case AdventureEventType.Dialogue:
						showDialogueOption();
					break;
					case AdventureEventType.ChooseReward:
						showDialogueOption();
						showDetailOption();
					break;
					case AdventureEventType.OptionReward:
						showDetailOption();
					break;
				}
			}
		}
		else
		{
			dialoguePlayer.PlayLine(line);
			/*
			if(line.target!=null)
			{
				currentTarget = line.target;
			}
			targetName.text = currentTarget.targetName;
			*/
			
		}
	}
	

	
	public void encounterEvent(AdventureEvent advEvent)
	{
		currentEventDone = false;
		
		if(advEvent.dialogue!=null&&advEvent.dialogue.lines.Count>0)
		{
			switch(advEvent.type)
			{
				case AdventureEventType.Dialogue:
					PlayDialogue(advEvent.dialogue);
				break;
			}
		}
		
		//PlayDialogue(advEvent.dialogue);
		
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
		switch(currentEvent.type)
		{
			case AdventureEventType.ChooseReward:
			EventDone();
			break;
			case AdventureEventType.OptionReward:
			break;
		}
	}

	void playDescription(string des)
	{
		dialoguePanel.show();
		optionDisplayerPanel.hide();
		dialoguePlayer.PlayText(des);
	}
	public void optionSuccess(int index)
	{
		playDescription(currentEvent.options[index].successStr);
	}
	
	public void optionFail(int index){
		playDescription(currentEvent.options[index].failStr);
		optionBtns[index].interactable = false;
	}

	void EventDone()
	{
		currentEventDone = true;
		optionPanel.hide();
		optionDisplayerPanel.hide();
		NextEvent();
	}
	bool currentEventDone = false;

	AdventureStage currentStage{
		get{return stages[currentStageIndex];}
	}
	
	void nextStage()
	{
		currentStageIndex++;
	}
	
	
	public void NextEvent()
	{	
		if(currentEvent.triggerNextEvent)
		{
			print("Next Event");
			encounterEvent(currentEvent.nextEvent);
		}
		else
		{
			print("Get new stage Event");
			/*
			if(currentStage.shouldMoveToNextStage)
			{
				nextStage();
			}
			*/
			UIManager.instance.ShowCover(()=>{
				//TODO:
				currentScene = currentStage.getScene();
				encounterEvent(currentScene.getEvent());
				
				encounterEvent(testEvent);
				UIManager.instance.HideCover();
			});
		}
	}

	AdventureScene _cur_scene;
	AdventureScene currentScene{
		get{
			return _cur_scene;
		}
		set{
			if(_cur_scene)
				_cur_scene.gameObject.SetActive(false);
			
			_cur_scene = value;
			_cur_scene.gameObject.SetActive(true);
		}
	}
	
}