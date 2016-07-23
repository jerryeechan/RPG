using UnityEngine;
using System.Collections.Generic;

[System.SerializableAttribute]
public class EquipData {
	string name;

    
    public void WearEquip(Character ch)
    {
        EquipManager.instance.getEquip(name);
        
    }
    //generate from equip the property may be different
    int[] hasEffects;
    List<SkillEffect> extraEffects;
//    public static EquipData 
}
