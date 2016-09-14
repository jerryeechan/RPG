using UnityEngine;
using System.Collections;



public class EquipGraphicAsset : ItemGraphicAsset{


	//idle, attack,
	public AnimationClip[] equipAnimations;
	public Sprite equipSprite;
}

public enum EquipType{Helmet,Armor,Shield,Weapon,Body}