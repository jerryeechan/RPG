using UnityEngine;
using System.Collections.Generic;
namespace com.jerrch.rpg
{
public class EquipUIManager : MonoBehaviour,IDisplayable{
	public EquipSlot[] equipSlots;
    Dictionary<EquipType,EquipSlot> equipSlotDict;
	void Awake()
    {
        equipSlotDict = new Dictionary<EquipType,EquipSlot>();
        foreach(var eqslot in equipSlots)
		{
			equipSlotDict.Add(eqslot.equipType,eqslot);
		}
    }
    public void Show()
    {
        inspectCharacter(GameManager.instance.currentCh);
    }
    public void Hide(){

    }
    void inspectCharacter(Character ch)
    {
        foreach(var eqtype in Equip.AllEquipType)
		{
			Equip eq = ch.getEquip(eqtype);
			if(eq != null)
			{
                if(!equipSlotDict.ContainsKey(eqtype))
                {
                    Debug.LogError(eqtype+"equip UI does not exist");
                }
                else
				    equipSlotDict[eqtype].bindItem = eq;	
			}
		}
    }
}
}