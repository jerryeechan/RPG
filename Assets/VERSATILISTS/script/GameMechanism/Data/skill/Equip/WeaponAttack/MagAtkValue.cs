using UnityEngine;
using System.Collections;

public class MagAtkValue : SkillEffect{

	//FOR WEAPON
	public int atkPower = 1;
	void Awake()
	{
		setLevel(level);
		mpCost = 0;
	}
	public override void ApplyOn (CharacterStat stat)
	{
		stat.magAtk+=atkPower;
		base.ApplyOn (stat);
	}
	public override void setLevel (int level)
	{
		atkPower = level*5;
		base.setLevel(level);
	}
}
