using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {
	Canvas canvas;
	void Awake()
	{
		canvas = GetComponent<Canvas>();
		canvas.pixelPerfect = true;
		if(Screen.width>=960)
		canvas.scaleFactor = 2;
	}
}
