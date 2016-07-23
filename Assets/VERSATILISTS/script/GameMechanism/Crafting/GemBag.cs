using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GemBag : MonoBehaviour {

	public static GemBag instance;
	List<SkillEffectGem> gems;
	void Awake()
	{
		if(instance!=null)
			throw new UnityException("double singleton");
		instance = this;
	}
	void Start()
	{
		gems = new List<SkillEffectGem>();
		SkillEffectGem[] gemArray = GetComponentsInChildren<SkillEffectGem>();
		for(int i=0;i<gemArray.Length;i++)
		gems.Add(gemArray[i]);
	}
	public void PutGemIntoBag(SkillEffectGem gem)
	{
		gems.Add(gem);
		gem.transform.parent = transform;
		PlaceInEmptySlot(gem.transform);
	}
	public void RemoveFromBag(SkillEffectGem gem)
	{
		gems.Remove(gem);
	}
	public void PlaceInEmptySlot(Transform gemTransform)
	{
		
	}
}
