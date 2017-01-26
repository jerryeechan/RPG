using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
namespace com.jerrch.rpg
{
public class SkillSlot : SkillButton, IDropHandler
{
	public int index;
    public void OnDrop(PointerEventData eventData)
    {
        SkillTree.instance.OnDropSkillSlot(this);
    }

}

}