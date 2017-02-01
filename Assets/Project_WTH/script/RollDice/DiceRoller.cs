using UnityEngine;
public class DiceRoller : MonoBehaviour {
	public float rollForce = 5000;
	void Awake()
	{
		diceNum = dices.Length;
		diceValues = new int[diceNum];
	}
	public GameObject[] dices;
	// Use this for initialization
	int detectNumber(Transform dice)
	{
		if((Approximately(dice.rotation.eulerAngles.z,0)))
		{
			if(Approximately(dice.rotation.eulerAngles.x,0))
			{
				return 5;
			}
			else if(Approximately(dice.rotation.eulerAngles.x,90))
			{
				return 1;
			}
			else if(Approximately(dice.rotation.eulerAngles.x,270))
			{
				return 6;
			}
		}
		else if(Approximately(dice.rotation.eulerAngles.z,90))
		{
			return 4;
		}
		else if(Approximately(dice.rotation.eulerAngles.z,180))
		{
			return 2;
		}
		else if(Approximately(dice.rotation.eulerAngles.z,270))
		{
			return 3;
		}
		return 0;
	}
	bool Approximately(float a,float b){
		float error = 0.3f;
		return Mathf.Abs(a-b)<error;
	}
	int diceNum;
	int [] diceValues;
	
	// Update is called once per frame
	void Update () {
		if(!isDiceReady)
		{
			bool allReady = true;
			for(int i=0;i<diceNum;i++)
			{
				if(dices[i].GetComponent<Rigidbody>().velocity.magnitude<0.01f)
				{
					diceValues[i] = detectNumber(dices[i].transform);
					
				}
				if(diceValues[i]==0)
				{
					allReady = false;
				}
			}
			
			if(allReady)
			{
				//CombatUIManager.instance.DiceRollDone();
				SkillCombatUIManager.instance.lockAllSkillBtn();
				isDiceReady = true;
				for(int i=0;i<diceNum;i++)
				{
					SkillCombatUIManager.instance.unlockSkill(diceValues[i]);
				}
			}
		}
		
	}
	bool isDiceReady;
	public void Roll()
	{
		
		
//		SkillCombatUIManager.instance.lockAllSkillBtn();
		Vector3 force = Random.onUnitSphere*rollForce;
		foreach(GameObject dice in dices)
		{
			Rigidbody diceRB = dice.GetComponent<Rigidbody>();
			diceRB.velocity = Random.onUnitSphere;
			diceRB.AddForce(force);	
			diceRB.angularVelocity = Random.onUnitSphere*200;
		}
		isDiceReady = false;
		for(int i=0;i<diceNum;i++)
		{
			diceValues[i] = 0;
		}
	}
	void Start(){
		Roll();
	}
	//z 270 3
	//z 180 2
	//z 90  4
	//z 0  x 270 6
	//z 0  x 90  1
	//z 0  x 0  5
}
