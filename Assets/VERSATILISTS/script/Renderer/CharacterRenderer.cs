using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
public class CharacterRenderer : MonoBehaviour {

	public Character bindCh;
	HealthBar bar;
	
	
	public Animator[] equipAnims;
	public Animator anim;
	public EquipRenderer[] equipRs;
	public SpriteRenderer indicater;
	public Dictionary<EquipType,EquipRenderer> equipUIDicts;
	void Awake()
	{
		anim = GetComponentInChildren<Animator>();
		bar = GetComponentInChildren<HealthBar>();
		Transform equipTransform =transform.Find("equip");
		if(equipTransform)
		{
			equipAnims = equipTransform.GetComponentsInChildren<Animator>();
			foreach (var eqanim in equipAnims)
			{

				eqanim.speed = GameManager.playerAnimationSpeed;
			}
			equipUIDicts = new Dictionary<EquipType,EquipRenderer>();
			equipRs = equipTransform.GetComponentsInChildren<EquipRenderer>();
			foreach(var eqR in equipRs)
			{
				print(eqR);
				equipUIDicts.Add(eqR.type,eqR);	
			}
/*
			equipUIDicts.Add(EquipType.Armor,armorRenderer);
			equipUIDicts.Add(EquipType.Helmet,helmetRenderer);
			equipUIDicts.Add(EquipType.Weapon,weaponRenderer);
			equipUIDicts.Add(EquipType.Shield,shieldRenderer);
			*/
		}
	}
	public void wearEquip(Equip equip)
	{
		EquipGraphicAsset equipGraphic = equip.bindGraphic;
		if(equipGraphic)
		{
			EquipType type = equip.equipType;
			EquipRenderer eqr = equipUIDicts[type]; 
			if(!eqr)
			{
				Debug.LogError("no eqr");
			}
			else
			{
				eqr.wearEquip(equipGraphic.equipAnimations);
				eqr.wearEquip(equipGraphic.equipSprite);
			}
			
		}
		else
		{
			Debug.LogError("no equip render graphic");
		}
		
		
	}
	public void syncAnimation()
	{
		print("sync");
		
		if(equipUIDicts==null)
			return;

		/*
		foreach(var eqRenderer in equipUIDicts)
		{
			print(eqRenderer);
			eqRenderer.Value.restart();
		}
		*/
	}

	public void init(CharacterStat stat)
	{
		if(bar)
			bar.SetFullValue(stat.hp);
		
	}
	public void updateRenderer(CharacterStat stat)
	{
		bar.SetNowValue(stat.hp);
	}
	public void PlayCharacterAnimation(CharacterAnimation chAnimation)
	{
		if(anim)
			anim.Play(chAnimation.ToString());
		foreach(var equipAnim in equipAnims)
		{
			equipAnim.speed = AnimationManager.getChAnimSpeed(chAnimation);
			equipAnim.Play(chAnimation.ToString());
		}
		//will call playSkill with animation event;
	}
	//select the characeter
	void OnMouseEnter()
	{
		CursorManager.instance.AttackMode();
		//Cursor.SetCursor()
	}
	void OnMouseExit()
	{
		CursorManager.instance.NormalMode();
	}
	public void selectByUI()
	{
		indicater.gameObject.SetActive(true);
	}
	public void deselect()
	{
		indicater.gameObject.SetActive(false);
	}
	bool isMouseDown;
	void OnMouseOver()
	{
		if(isMouseDown)
		{
			pressCount+=Time.deltaTime;
			if(pressCount>1)
			{
				print("Mouse press");
				pressCount = 0;	
				isLongPress = true;
			}
		}
	}
	float pressCount;
	bool isLongPress;
	void OnMouseDown()
	{
		isMouseDown = true;
		//clean
		isLongPress = false;
	}
	void OnMouseUp()
	{
		pressCount = 0;
		isMouseDown = false;
	}
	void OnMouseUpAsButton()
	{
		if(!bindCh)
		{

		}
		else if(isLongPress)
		{

		}
		else
		{
			RandomBattleRound.instance.selectedEnemy = bindCh;	
			ActionUIManager.instance.useAction();
		}
		
		//print("click ch Render");
	}
	public void HitAnimation()
	{
		print(GetComponentInChildren<SpriteRenderer>().gameObject);
		LeanTween.alpha(gameObject,0,0.05f).setDelay(0.1f).setLoopPingPong().setLoopCount(6);
		//GetComponentInChildren<SpriteRenderer>().DOColor(Color.red,0);//.SetDelay(0.1f).SetLoops(6,LoopType.Yoyo);
		//iTween.ShakePosition(gameObject,Vector3.one,0.2f);
		transform.DOShakePosition(0.2f,4,100);
		//d.loopCount = 4;
		
	}
	SpriteRenderer spr;
	public void setAlpha( float val )
    {
        spr.color = new Color(1f,1f,1f,val);
    }

	public void DieComplete()
	{
		print("die");
		Destroy(bindCh.gameObject);
		Destroy(gameObject);
	}
	
	public void ShowSkill()
	{
		transform.parent.SendMessage("PlaySkillEffect");
	}
}

public enum CharacterAnimation{idle,spell,melee,die,hurt,defense}
public delegate void OnHitAnimationDone(Character ch);