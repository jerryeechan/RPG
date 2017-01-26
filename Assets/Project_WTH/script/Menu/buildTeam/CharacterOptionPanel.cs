using System.Collections;
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

    SkillButton[] skillBtns;

    void Awake()
    {
        skillBtns = GetComponentsInChildren<SkillButton>();
    }
	
	public void setCharacter(CharacterData chData)
    {
        helmet.sprite = EquipManager.instance.getEquipFromData(chData.helmet).bindGraphic.chSprite;
        armor.sprite =  EquipManager.instance.getEquipFromData(chData.armor).bindGraphic.chSprite;
        weapon.sprite =  EquipManager.instance.getEquipFromData(chData.weapon).bindGraphic.chSprite;
        shield.sprite =  EquipManager.instance.getEquipFromData(chData.shield).bindGraphic.chSprite;
        
        for(int i=0;i<3;i++)
        {
            skillBtns[i].bindSkill = SkillManager.instance.getSkill(chData.skillDatas[i].id);
        }
        
    }
    public void btnClick(int index)
    {
        RosterBuilder.instance.bindSkill = skillBtns[index].bindSkill;
    }
}