using UnityEngine;
using System.Collections;
using DG.Tweening;
public class DamageNumber : MonoBehaviour {
	public int index;
	public void Show()
	{
		SpriteRenderer spr = GetComponent<SpriteRenderer>();
		if(index == 0)
		{
			spr.DOFade(0,2).OnComplete(()=>{
				Destroy(transform.parent.gameObject);
			});
		}
		else{
			spr.DOFade(0,2);
		}
		
	}
}
