using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using com.jerrch.rpg;
public class CharacterRenderer : MonoBehaviour {

	public Character bindCh;
	HealthBar bar;
	
	static float playerAnimationSpeed = 0.5f;
	//public Animator[] equipAnims;
	public Animator anim;
	public EquipRenderer[] equipRs;
	public SpriteRenderer indicater;
	public Dictionary<EquipType,EquipRenderer> equipUIDicts;
	Transform hitSpot;
	public Vector2 hitPos
	{
		get{
			return hitSpot.position;
		}
	}
	void Awake()
	{
		hitSpot = transform.Find("hitSpot");
		anim = GetComponentInChildren<Animator>();
		bar = GetComponentInChildren<HealthBar>();
		Transform equipTransform =transform.Find("equip");
		if(equipTransform)
		{
			//equipAnims = equipTransform.GetComponentsInChildren<Animator>();
			//foreach (var eqanim in equipAnims)
			//{
			//	eqanim.speed = playerAnimationSpeed;
			//}
			anim.speed = playerAnimationSpeed;
			equipUIDicts = new Dictionary<EquipType,EquipRenderer>();
			equipRs = equipTransform.GetComponentsInChildren<EquipRenderer>();
			foreach(var eqR in equipRs)
			{
				equipUIDicts.Add(eqR.type,eqR);	
			}
		}

		
		indicater.DOFade(1,0);
		indicater.DOFade(0,0.4f).SetLoops(-1,LoopType.Yoyo);
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
				eqr.wearEquip(equip.bindGraphic);
				//eqr.wearEquip(equip);
				//eqr.wearEquip(equipGraphic.equipAnimations);
			}
			
		}
		else
		{
			Debug.LogError("no equip render graphic");
		}
		
		
	}
	public void syncAnimation()
	{
		if(equipUIDicts==null)
			return;
	}

	public void init(CharacterStat stat)
	{
		if(bar)
			bar.SetFullValue(stat.hp.finalValue);
	}
	public void updateRenderer(CharacterStat stat)
	{
		bar.SetNowValue(stat.hp.currentValue);
	}
	public void PlayCharacterAnimation(CharacterAnimation chAnimation)
	{
		if(anim)
		{
			anim.Play(chAnimation.ToString(),-1,0);
			anim.speed = AnimationManager.getChAnimSpeed(chAnimation);
		}
		
		
		/*
		foreach(var equipAnim in equipAnims)
		{
			equipAnim.speed = AnimationManager.getChAnimSpeed(chAnimation);
			equipAnim.Play(chAnimation.ToString(),-1,0);
		}*/
	
		
		//will call playSkill with animation event;
	}
	public SpriteRenderer dieBody;
	
	//select the characeter
	void OnMouseEnter()
	{
		if(!isCombatMode)
			return;
		if(!SkillCombatUIManager.instance.isDraggingSkill)
		{
			CursorManager.instance.PointerMode();
		}
		else
		{
			if(bindCh.side == CharacterSide.Enemy)
			{
				CursorManager.instance.AttackMode();
			}
			else
			{
				CursorManager.instance.PointerMode();
			}
		}
		

	}
	
	void OnMouseExit()
	{
		//if(!SkillCombatUIManager.instance.isDraggingSkill)
		//	return;
		CursorManager.instance.NormalMode();
		/*
		if(isCombatMode)
			SkillCombatUIManager.instance.OverCharacter(null);
			*/
	}

	//only call in SkillUIManger.instance.setCharacter
	
	static CharacterRenderer lastSelected;
	public void deselect()
	{
		//indicater.DOKill();
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
		/*
		if(isCombatMode&&SkillCombatUIManager.instance.isDraggingSkill)
		{
			SkillCombatUIManager.instance.OverCharacter(bindCh);
			
			if(bindCh.side == CharacterSide.Player)
				RandomBattleRound.instance.selectedAllies = bindCh;
			else
				RandomBattleRound.instance.selectedEnemy = bindCh;
			
		}*/
	}
	bool isCombatMode{
		get {return GameManager.instance.gamemode == GameMode.Combat;} 
	}
	float pressCount;
	bool isLongPress;
	void OnMouseDown()
	{
		isMouseDown = true;
		//clean
		isLongPress = false;
		//NumberGenerator.instance.GetDamageNum(transform.position+Vector3.up*20,Random.Range(0,20));
		
	}
	void OnMouseUp()
	{
		if(!isCombatMode)
			return;
		print("mosue up");
		pressCount = 0;
		isMouseDown = false;
		if(SkillCombatUIManager.instance.isDraggingSkill)
		{
			print("drop skill on this ch:"+bindCh.name);
		}
		
	}

	public void selected()
	{
		SkillCombatUIManager.instance.setCharacter(bindCh);
		BattleChUIManager.instance.setCharacter(bindCh);
		
		TurnBattleManager.instance.selectedPlayer = bindCh;
		
		indicater.gameObject.SetActive(true);
		if(lastSelected)
			lastSelected.deselect();
		lastSelected = this;
	}
	
	void OnMouseUpAsButton()
	{
		
		if(!isCombatMode)
			return;
		if(!bindCh)
		{

		}
		else if(isLongPress)
		{

		}
		else
		{
			//TODO: click the character, select character 
			if(bindCh.side == CharacterSide.Player)
			{
				selected();
				SoundEffectManager.instance.playSound(BasicSound.UI);
			}
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
		print("hit Animation");
		
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
	public void BattleEnd()
	{
		indicater.gameObject.SetActive(false);
	}
}

public enum CharacterAnimation{idle,melee,holdmelee,upmelee,die,hurt,defense}
public delegate void OnHitAnimationDone(Character ch);