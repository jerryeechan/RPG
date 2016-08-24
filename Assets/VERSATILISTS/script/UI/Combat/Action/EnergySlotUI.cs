using UnityEngine;
using System.Collections;

public class EnergySlotUI : MonoBehaviour {
	GameObject perk;
	void Awake()
	{
		perk = transform.Find("perk").gameObject;
	}
	public void occupy()
	{
		perk.SetActive(false);
	}
	public void recover()
	{
		perk.SetActive(true);
	}
}