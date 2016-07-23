using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SkillCraftingSlots : MonoBehaviour {

	public static SkillCraftingSlots instance;
	// Use this for initialization
	List<SkillEffectGem> gems;
	Skill skillBase;
	void Awake()
	{
		if(instance!=null)
			throw new UnityException("double singleton");
		instance = this;
		
	}
	void Start () {
		gems = new List<SkillEffectGem>();
	}
	public void PutGemIntoCrafting(SkillEffectGem gem)
	{
		gems.Add(gem);
		gem.transform.parent = transform;
		PlaceInEmptySlot(gem.transform);
	}
	public void RemoveFromCrafting(SkillEffectGem gem)
	{
		gems.Remove(gem);
		
	}
	public void PlaceInEmptySlot(Transform gemTransform)
	{
		
	}
	
	public List<SkillEffectGem> gemsUnderCrafting()
	{
		return gems;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
