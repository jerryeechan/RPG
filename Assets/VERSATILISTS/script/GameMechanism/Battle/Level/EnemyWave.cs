using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyWave : MonoBehaviour {

	public List<EnemyPreset> enemyPreset;
	// Use this for initialization

	/*
	public List<Character> getEnemyWave()
	{
		List<Character> enemyGened = new List<Character>();
		for(int i=0;i<enemyTemplates.Count;i++)
		{
			Character newEnemy = (Character)Instantiate(enemyTemplates[i]);
			//newEnemy.
			enemyGened.Add(newEnemy);
			Debug.Log("New enemy gened"+enemyTemplates[i].name);
		}
		return enemyGened;
	}
	*/
}
