using UnityEngine;
using System.Collections;

public class CombatStage : MonoBehaviour {
	public string enemyWaveName;
	public EnemySet enemySet{
		get{
			return MonsterDataEditor.instance.getMonsterSet(enemyWaveName);
		}
		
	}
}
