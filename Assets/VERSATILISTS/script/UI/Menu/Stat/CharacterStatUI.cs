using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CharacterStatUI : MonoBehaviour {

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

}
