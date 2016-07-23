using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CraftingManager : MonoBehaviour {

	public static CraftingManager instance;
	public SkillCraftingSlots skillCraft;
	public GemBag bag;
	void Awake()
	{
		if(instance!=null)
			throw new UnityException("double singleton");
		instance = this;
	}
	public void CraftingGem(SkillEffectGem gem){
		bag.RemoveFromBag(gem);
		skillCraft.PutGemIntoCrafting(gem);
	}
	public void PutGemBack(SkillEffectGem gem)
	{
		skillCraft.RemoveFromCrafting(gem);
		bag.PutGemIntoBag(gem);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	public void CraftNewSkill()
	{
		GameObject newSkill = new GameObject("newSkill");
		
		List<SkillEffectGem> gems =skillCraft.gemsUnderCrafting();
		for(int i=0;i<gems.Count;i++)
		{
			SkillEffect gemEffect = gems[i].GetComponent<SkillEffect>();
			System.Type skillClass = gemEffect.GetType();
			SkillEffect effect = newSkill.AddComponent(skillClass) as SkillEffect;
			effect.setLevel(gemEffect.level);
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
