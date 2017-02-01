using UnityEngine;
using System.Collections;



public class EquipGraphicAsset : ItemGraphicAsset{


	//idle, attack,
	public AnimationClip[] equipAnimations;
	public Sprite chSprite;
	
	[SerializeField] 
	Sprite[] sprites;

	public EquipType type;
	public ClassesType classesType;
	public string suitName;
	public string equipTypeName;
	public void loadSpirtes(Sprite[] sps)
	{
		sprites = sps;
	}
	
	//public Sprite equipSprite;
}

public enum EquipType{Helmet,Armor,Shield,Weapon}