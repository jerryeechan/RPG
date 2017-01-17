using UnityEngine;
using System.Collections;

public class FontManager : Singleton<FontManager>{
	public Font[] chineseFont;
	public Font GetChineseFont()
	{
		return chineseFont[0];
	}
}
