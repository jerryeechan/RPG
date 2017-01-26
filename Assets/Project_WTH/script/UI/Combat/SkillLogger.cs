using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class SkillLogger : Singleton<SkillLogger> {
	public Text text;
	int count = 0;
	void Awake()
	{
		Clean();
		//Invoke("addTest",1);
		//Invoke("addTest2",2);
	}
	void addTest()
	{
		newLine("囚犯：Fuck you\n");
	}
	void addTest2()
	{
		newLine("勇者：關我屁事\n");
	}
	public void Clean()
	{
		text.text = "";
	}
	List<string> lines = new List<string>();
	public void newLine(string line)
	{
		lines.Add(line);
		count++;
		if(count>10)
		{
			lines.RemoveAt(0);
			count--;
		}
		showLines();
	}
	void showLines()
	{
		Clean();
		foreach (string line in lines)
		{
			text.text += line;
		}
	}
	public static void Log(string line)
	{
		instance.newLine(line+"\n");
		print(line+"\n");
	}
}
