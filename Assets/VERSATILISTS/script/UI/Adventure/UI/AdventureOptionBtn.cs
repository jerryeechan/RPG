using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class AdventureOptionBtn : MonoBehaviour, IPointerClickHandler
{
    public CompositeText text;
	public int index;
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if(interactable)
        {
            AdventureManager.instance.selectOption(index);
            CursorManager.instance.NormalMode();
            interactable = false;
        }
    }
    void Awake()
    {
        text = GetComponentInChildren<CompositeText>();
    }

    public bool interactable{
        set{
            GetComponent<HandButton>().interactable = value;
        }
        get{
            return GetComponent<HandButton>().interactable;
        }
    }
    
}
