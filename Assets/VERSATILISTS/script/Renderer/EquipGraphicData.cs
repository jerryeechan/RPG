using UnityEngine;
using System.Collections;

public enum EquipType{Helmet,Armor,Shield,Weapon}

public class EquipGraphicData : MonoBehaviour {

	public EquipType type;
	public AnimationClip[] equipAnimations;
	public Sprite equipSprite;
	public Sprite iconSprite;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
