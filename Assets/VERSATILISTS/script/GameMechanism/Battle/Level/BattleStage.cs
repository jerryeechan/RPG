using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BattleStage : MonoBehaviour {

	public List<EnemyWave> enemyWaves;
	int waveNow = 0;
	public bool isAllDead()
	{
		return waveNow>enemyWaves.Count?true:false;
	}
	public List<Character> getNextWave()
	{
		
		return enemyWaves[waveNow++].getEnemyWave();
	}
}
