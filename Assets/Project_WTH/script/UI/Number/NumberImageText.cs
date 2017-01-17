using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class NumberImageText : MonoBehaviour {
	void Awake()
	{
		init();
	}
	public void init()
	{
		img = GetComponent<Image>();
	}
	public void setSprite(Sprite sp)
	{
		img.sprite = sp;
		img.SetNativeSize();
	}
	Image img;
	
}
