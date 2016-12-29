﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.jerrch.rpg;
public class CharacterOptionPanel : MonoBehaviour {
    [SerializeField]
    Image helmet;
    [SerializeField]
    Image armor;
    [SerializeField]
    Image shield;
    [SerializeField]
    Image weapon;
	
	public void setCharacter(CharacterData chData)
    {
        helmet.sprite = EquipManager.instance.getEquipFromData(chData.helmet).bindGraphic.chSprite;
        armor.sprite =  EquipManager.instance.getEquipFromData(chData.armor).bindGraphic.chSprite;
        weapon.sprite =  EquipManager.instance.getEquipFromData(chData.weapon).bindGraphic.chSprite;
        print(chData.shield.id);
        shield.sprite =  EquipManager.instance.getEquipFromData(chData.shield).bindGraphic.chSprite;
    }
}