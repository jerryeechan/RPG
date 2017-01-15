using UnityEngine;
using System.Collections.Generic;
namespace com.jerrch.rpg
{
public class EquipUIManager : MonoBehaviour,IinspectPlayerable{
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
    
    public void inspectCharacter(Character ch)
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