using UnityEngine;
using System.Collections.Generic;

[System.SerializableAttribute]
public class EquipData:ItemData {
    //generate from equip the property may be different

    ///TODO need effect seed!!!!
    public EquipData():base("","")
    {
        //hasEffects = new int[1]{0};
        
    }
    int seed;
    
    public EquipData(string id,int statSeed):base(id,id)
    {
       // hasEffects = new int[1]{0};
        seed = statSeed;
    }
    public EquipData(string id,string graphicID,int statSeed):base(id,graphicID)
    {
        //hasEffects = new int[1]{0};
        seed = statSeed;
    }
    //TODO: hasEffects randomable 
    
    //int[] hasEffects = new int[1]{0};
    //string specialEffect;
    public new Equip getItem()
    {
        Equip eq = EquipManager.instance.getEquip(id,imageID);
        if(eq!=null)
            eq.seed = seed; 
        return eq;
    }
}
