using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using com.jerrch.rpg;
using UnityEngine.UI;
public class DiceActionSlot : MonoBehaviour ,IDropHandler{
	public ActionDiceType diceType;
	public ActionPlayUnit actionUnit;

	[SerializeField]
	Image actionIcon;
	
	void Awake(){
		actionUnit = new ActionPlayUnit();
	}
	public void clear()
	{
		GetComponentInChildren<CanvasGroup>().alpha = 0;
		actionIcon.sprite = SpriteManager.instance.emptySprite;
		actionUnit.maximum = 1;
	}
    public void OnDrop(PointerEventData eventData)
    {
		
		Action dropAction = ActionUIManager.instance.draggingActionBtn.bindAction;
		print(""+dropAction.diceType+diceType);
		if(dropAction.diceType == diceType)
		{
			
			GetComponentInChildren<CanvasGroup>().alpha = 1;
			actionIcon.sprite = dropAction.icon;
			actionUnit.AddAction(TurnBattleManager.instance.selectedPlayer,dropAction);
			TurnBattleManager.instance.testReady();

			SoundEffectManager.instance.playSound(BasicSound.Drop);
		}
    }
	
    // Use this for initialization

}