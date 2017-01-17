using UnityEngine;
using System.Collections;

public class ItemGraphicAsset : MonoBehaviour {

	void OnValidate()
	{
		id = name;
	}
	public string id;
	public Sprite iconSprite;
}