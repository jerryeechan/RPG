using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.SerializableAttribute]
public class DependentAttribute : Attribute {
    
	 protected List<Attribute> _otherAttributes;
    // protected Dictionary<string,Attribute> otherAttributes;
    public DependentAttribute(int startingValue):base(startingValue)
    {
        _otherAttributes = new List<Attribute>();
    }
        
    public void addAttribute(Attribute attr) 
    {
        _otherAttributes.Add(attr);
    }
        
    public void removeAttribute(Attribute attr)
    {
        if (_otherAttributes.IndexOf(attr) >= 0)
        {
            _otherAttributes.Remove(attr);
        }
    }
        
    public override int calculateValue()
    {
        // Specific attribute code goes somewhere in here
            
        _finalValue = baseValue;
            
        applyRawBonuses();
            
        applyFinalBonuses();
            
        return _finalValue;
    }
}
