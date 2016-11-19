using UnityEngine;
using System.Collections;
namespace com.jerrch.rpg
{
		public class PhyAtkValue : SkillEffect{

		//FOR WEAPON
		
		public override void setLevel (int level)
		{
			base.setLevel(level);
		}
		public override void ApplyOn (CharacterStat stat)
		{
			stat.phyAtk+=initValue;
			base.ApplyOn(stat);
		}
		
	}
}