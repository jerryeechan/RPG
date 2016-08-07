using UnityEngine;
using System.Collections;

  public class PhyAtkValue : SkillEffect{

	//FOR WEAPON

	public override void setLevel (int level)
	{
		baseValue = level*5+1;
		base.setLevel(level);
	}
	public override void ApplyOn (CharacterStat stat)
	{
		stat.phyAtk+=baseValue;
		base.ApplyOn(stat);
	}
	
}
