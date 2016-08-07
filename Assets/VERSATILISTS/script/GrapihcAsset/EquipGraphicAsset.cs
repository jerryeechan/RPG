using UnityEngine;
using System.Collections;

public enum EquipType{Helmet,Armor,Shield,Weapon}

public class EquipGraphicAsset : ItemGraphicAsset{

	public EquipType type;

	//idle, attack,
	public AnimationClip[] equipAnimations;
	public Sprite equipSprite;
}
