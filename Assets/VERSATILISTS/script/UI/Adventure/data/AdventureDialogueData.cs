using UnityEngine;
using System.Collections.Generic;

[System.SerializableAttribute]
public class AdventureDialogueData {

    int currentLineID = 0;
    public List<AdventureDialogueLineData> lines;
	
    public AdventureDialogueLineData nextLine()
    {
        if(currentLineID<lines.Count)
        return lines[currentLineID++];
        else
        return null;
    }

    public void restart()
    {
        currentLineID = 0;
    }
}
