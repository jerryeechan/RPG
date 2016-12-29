using UnityEngine;
using System.Collections.Generic;

[System.SerializableAttribute]
public class EquipData:ItemData {
    //generate from equip the property may be different
    public EquipData():base("","")
    {
        hasEffects = new int[1]{0};
        
    }
    public EquipData(string id):base(id,id)
    {
        hasEffects = new int[1]{0};
    }
    public EquipData(string id,string graphicID):base(id,graphicID)
    {
        hasEffects = new int[1]{0};
    }
    //TODO: hasEffects randomable 
    int[] hasEffects = new int[1]{0};
    //string specialEffect;
    public Equip genEquip()
    {
        Equip eq = EquipManager.instance.getEquip(id,imageID); 
        if(eq)
            eq.setEffect(hasEffects);
        return eq;
    }
}
