using UnityEngine;
public class SkillEffectGemGenerator : MonoBehaviour {

	public GameObject effectPacks;
	public SkillEffect[] allEffects;
	public static SkillEffectGemGenerator instance;
	
	void Awake()
	{
		allEffects = effectPacks.GetComponentsInChildren<SkillEffect>();
		GenerateGem(1,10);
	}
	void GenerateGem(int id,int level)
	{
		//GameObject effectObj = (GameObject)Instantiate(allEffects[id]);
		GameObject g = new GameObject("effectGem",allEffects[id].GetType());
		g.GetComponent<SkillEffect>().setLevel(level);
		
	}
}
