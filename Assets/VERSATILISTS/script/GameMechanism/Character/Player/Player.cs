using UnityEngine;
using System.Collections;

namespace com.jerrch.rpg{
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

}