using UnityEngine;
using System.Collections;

public class DamageNumber : MonoBehaviour {

	void SelfDestroy()
	{
		Destroy(transform.root.gameObject);
	}	
}
