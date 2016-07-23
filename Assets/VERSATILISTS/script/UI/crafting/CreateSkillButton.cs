using UnityEngine;
using System.Collections;

public class CreateSkillButton : MonoBehaviour {

	void OnTouchBegan(TouchInfo touchInfo)
	{
		Debug.Log("new skill created");
		CraftingManager.instance.CraftNewSkill();
	}
	
}
