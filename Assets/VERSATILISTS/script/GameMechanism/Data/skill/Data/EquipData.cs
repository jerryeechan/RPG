using UnityEngine;
using System.Collections.Generic;

[System.SerializableAttribute]
public class EquipData:ItemData {
    //generate from equip the property may be different

    //TODO: hasEffects randomable 
    int[] hasEffects = new int[1]{0};
    //string specialEffect;
    public Equip genEquip()
    {
        Equip eq = EquipManager.instance.getEquip(id); 
        if(eq)
            eq.setEffect(hasEffects);
        return eq;
    }
}
