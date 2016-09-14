using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CharacterAbilityUI : MonoBehaviour ,IPointerClickHandler{

    public AbilityType type;
    CompositeText text;
    void Awake()
    {   
        text = GetComponentInChildren<CompositeText>();
    }
    public void updateUI(int val)
    {
        if(!text)
            text = GetComponentInChildren<CompositeText>();
        //string result = labelString+":"+val;
        text.text = val.ToString();
    }
    void IPointerClickHandler.OnPointerClick(PointerEventData data)
    {
        print("Ability btn click");
        CharacterAbilityUIManager.instance.statUIClicked(type);
    }
}

public enum AbilityType{STR,CON,INT,DEX}