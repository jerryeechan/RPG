using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public Character character;
	public void loadPlayer()
	{
		string playerJSON = PlayerPrefs.GetString("playerData");
		character = JsonUtility.FromJson<Character>(playerJSON);
	}
	public void savePlayer()
	{
		
		
		//JsonUtility.ToJson();
		//character.skills
	}
}
