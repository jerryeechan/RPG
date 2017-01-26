using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    private Dictionary<string, DialogNodeCanvas> _dialogIdTracker;

    [SerializeField]
    private GameObject _messageBoxPrefab;

    [SerializeField]
    private RectTransform _canvasObject;
    
    public void Awake()
    {        
        _dialogIdTracker = new Dictionary<string, DialogNodeCanvas>();
        _dialogIdTracker.Clear();
        foreach (DialogNodeCanvas nodeCanvas in Resources.LoadAll<DialogNodeCanvas>("Saves/"))
        {
            foreach (string id in nodeCanvas.GetAllDialogId())
            {
                _dialogIdTracker.Add(id, nodeCanvas);
            }
        }
    }

    public void ShowDialogWithId(string dialogIdToLoad, bool goBackToBeginning)
    {

        DialogNodeCanvas nodeCanvas;
        if (_dialogIdTracker.TryGetValue(dialogIdToLoad, out nodeCanvas))
        {
            nodeCanvas.ActivateDialog(dialogIdToLoad, goBackToBeginning);
        }
        else
            Debug.LogError("Not found Dialog with ID : " + dialogIdToLoad);

        MessageBoxHud messageBox = GameObject.Instantiate(_messageBoxPrefab).GetComponent<MessageBoxHud>();        
        // messageBox.Construct(dialogIdToLoad, this);
        // messageBox.transform.SetParent(_canvasObject, false);
        // messageBox.SetData(GetNodeForId(dialogIdToLoad));
        // _messageBoxes.Add(dialogIdToLoad, messageBox);
    }

    private BaseDialogNode GetNodeForId(string dialogIdToLoad)
    {
        DialogNodeCanvas nodeCanvas;
        if (_dialogIdTracker.TryGetValue(dialogIdToLoad, out nodeCanvas))
        {
            return nodeCanvas.GetDialog(dialogIdToLoad);
        }
        else
            Debug.LogError("Not found Dialog with ID : " + dialogIdToLoad);
        return null;
    }

    private void GiveInputToDialog(string dialogIdToLoad, int inputValue)
    {
        DialogNodeCanvas nodeCanvas;
        if (_dialogIdTracker.TryGetValue(dialogIdToLoad, out nodeCanvas))
        {
            nodeCanvas.InputToDialog(dialogIdToLoad, inputValue);
        }
        else
            Debug.LogError("Not found Dialog with ID : " + dialogIdToLoad);
    }

    /*
    public void OkayPressed(string dialogId)
    {
        GiveInputToDialog(dialogId, (int)EDialogInputValue.Next);
        _messageBoxes[dialogId].SetData(GetNodeForId(dialogId));
    }

    public void BackPressed(int dialogId)
    {
        GiveInputToDialog(dialogId, (int)EDialogInputValue.Back);
        _messageBoxes[dialogId].SetData(GetNodeForId(dialogId));
    }

    public void RemoveMessageBox(int dialogId)
    {
        _messageBoxes.Remove(dialogId);
    }

    public void OptionSelected(int dialogId, int optionSelected)
    {
        GiveInputToDialog(dialogId, optionSelected);
        _messageBoxes[dialogId].SetData(GetNodeForId(dialogId));
    }*/
}

public enum EDialogInputValue
{
    Next = -2,
    Back = -1,
}