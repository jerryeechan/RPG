using UnityEngine;
using System.Collections;

  public class PhyAtkValue : SkillEffect{

	//FOR WEAPON
	public int atkPower = 1;
	void Awake()
	{
		setLevel(level);
	}
	public override void ApplyOn (CharacterStat stat)
	{
		stat.phyAtk+=atkPower;
		base.ApplyOn (stat);
	}
	public override void setLevel (int level)
	{
		atkPower = level*5;
		base.setLevel(level);
	}
}
