using UnityEngine;
using System.Collections;

public class SkillEffectGem : MonoBehaviour {

	TextMesh text;
	SkillEffect effect;
	void OnTouchBegan(TouchInfo touchInfo)
	{
		//transform.position = (Vector3)touchInfo.touchPos;
		if(isUnderCrafting==false)
		{
			CraftingManager.instance.CraftingGem(this);
			isUnderCrafting = true;
		}
		else
		{
			CraftingManager.instance.PutGemBack(this);
			isUnderCrafting = false;
		}
		
	}
	bool isUnderCrafting = false;
	bool isMoveToSlot;
	void Awake()
	{
		effect = GetComponent<SkillEffect>();
	}
	/*void OnTriggerEnter2D(Collider2D collider2D)
	{
		if(collider2D.tag=="skillslot")
		{
			isMoveToSlot = true;
		}
	}
	void OnTriggerExit2D(Collider2D collider2D)
	{
		
	}*/
}
