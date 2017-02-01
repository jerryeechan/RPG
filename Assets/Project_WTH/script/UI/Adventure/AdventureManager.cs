using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using com.jerrch.rpg;
public class AdventureManager : Singleton<AdventureManager>,IDisplayable {

	//UI
	public CompositeText targetName;
	//public CompositeText dialogueLine;
	
	public AnimatableCanvas optionPanel;
	public AnimatableCanvas optionDisplayerPanel;
	public AnimatableCanvas dialoguePanel;
	public DialoguePlayer dialoguePlayer;
//Renderer
	
	public void Show()
	{
		dialoguePlayer.show();
		GetComponent<AnimatableCanvas>().show();
	}
	public void Hide()
	{
		dialoguePlayer.hide();
		GetComponent<AnimatableCanvas>().hide();
	}
// vars
	

	AdventureOptionBtn[] optionBtns;
	AdventureOptionDisplayer[] optionDisplayers;

	public AdventureDialogueData currentDialogue;
	AdventureEvent currentEvent;

	void Awake()
	{
		optionBtns = optionPanel.GetComponentsInChildren<AdventureOptionBtn>();
		optionDisplayers = optionDisplayerPanel.GetComponentsInChildren<AdventureOptionDisplayer>();
		optionPanel.hide(0);
		optionDisplayerPanel.hide(0);
	}
	//public AdventureEvent testEvent;
	public void init()
	{
		currentScene = AdventureStageManager.instance.currentStage.getScene();
		print(currentScene.name);
		encounterEvent(currentScene.getEvent());
	}
	public void PlayDialogue(AdventureDialogueData dialogue)
	{
//		Debug.LogError("play dialogue");
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
			optionBtns[2].GetComponent<RectTransform>().SetPositionX(85);
			optionBtns[2].gameObject.SetActive(true);
		}
	}
	void ShowOptionDisplayer(int num)
	{
		optionDisplayerPanel.show();
		optionDisplayers[0].interactable = true;
		optionDisplayers[1].interactable = true;
		optionDisplayers[2].interactable = true;
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
	
	AdventureDialogueLineData currentLine;
	//dialogues
	public void PlayNextLine()
	{
		if(currentEventDone)
		{
			NextEvent();
		}

		if(currentLine == null)
		{
			currentLine = currentDialogue.nextLine();
		}
		if(currentLine == null)
		{	
			//temp demo solution
			if(currentEvent.hasOption == false)
			{
				currentEventDone = true;
			}
			else if(!isOptionDisplayed)
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
			//end of temp demo solution
			
			//end of dialogue
			//display option and wait
			
		}
		else
		{
			
			if(!dialoguePlayer.testPlaying)
			{
				dialoguePlayer.PlayLine(currentLine);
				currentLine = null;
			}
			/*
			if(line.target!=null)
			{
				currentTarget = line.target;
			}
			targetName.text = currentTarget.targetName;
			*/
			
		}
	}
	
	public void encounterEvent(BaseEvent baseEvent)
	{
		AdventureEvent adv = baseEvent as AdventureEvent;
		if(adv!=null)
		{
			encounterEvent(adv);
		}
		else
		{
			EffectEvent ev = baseEvent as EffectEvent;
			if(ev!=null)
			{
				ev.doEffect();
				encounterEvent(ev.nextEvent);
			}
		}
	}
	
	void encounterEvent(AdventureEvent advEvent)
	{
		currentEventDone = false;
		if(advEvent == null)
		{
			Debug.LogError("null event");
		}
		if(advEvent.dialogue!=null&&advEvent.dialogue.lines.Count>0)
		{
//			Debug.LogError("has event");
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
		//EventDone();
	}
	
	public void optionFail(int index){
		playDescription(currentEvent.options[index].failStr);
		optionBtns[index].interactable = false;
	}

	public void EventDone()
	{
		print("hide option panels all");
		currentEventDone = true;
		optionPanel.hide();
		optionDisplayerPanel.hide();
	}
	
	bool currentEventDone = false;

	
	
	
	
	public void NextEvent()
	{	
		if(currentEvent.triggerNextEvent)
		{
			Debug.LogError("Next Event");
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
			PauseMenuManager.instance.Transition(()=>{
				//TODO:
				currentScene = AdventureStageManager.instance.currentStage.getScene();

				dialoguePlayer.init();
				encounterEvent(currentScene.getEvent());
				//encounterEvent(testEvent);
			});
		}
	}

	[SerializeField]
	AdventureScene _cur_scene;
	AdventureScene currentScene{
		get{
			return _cur_scene;
		}
		set{
			if(_cur_scene != null)
				_cur_scene.gameObject.SetActive(false);
			
			_cur_scene = value;
			_cur_scene.gameObject.SetActive(true);
		}
	}
	
}