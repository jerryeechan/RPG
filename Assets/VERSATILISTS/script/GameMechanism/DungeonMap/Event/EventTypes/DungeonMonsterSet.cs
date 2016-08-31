using UnityEngine;
using System.Collections.Generic;

public class DungeonMonsterSet : MonoBehaviour {

	EnemyWave[] waves;
	Queue<EnemyWave> waveQueue;
	// Use this for initialization
	void Awake () {
		waves = GetComponentsInChildren<EnemyWave>();
		waveQueue = new Queue<EnemyWave>();
		foreach(var wave in waves)
		{	
			waveQueue.Enqueue(wave);
		}
	}

	public EnemyWave getNextWave()
	{
		return waveQueue.Dequeue();
	}
}
