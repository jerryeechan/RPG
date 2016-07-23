using UnityEngine;
using System.Collections;

public class CharacterAnimation : MonoBehaviour {

	public void Die()
	{
		Destroy(gameObject);
	}
	
	public void ShowSkill()
	{
		transform.parent.SendMessage("PlaySkillEffect");
	}
}
