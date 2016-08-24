using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
public class DungeonOptionSelector : Singleton<DungeonOptionSelector>{
	public DungeonOptionButton left;
	public DungeonOptionButton right;
	RectTransform panel;
	void Awake(){
		image = GetComponent<Image>();
		panel = transform.parent.GetComponent<RectTransform>();
		left.index = 0;
		right.index = 1;
		selectedButton = left;
	}
	public void keydown(KeyCode key)
	{
		switch(key)
		{
			case KeyCode.Z:
				//confirm
				pressButton();
			break;
			case KeyCode.LeftArrow:
				moveTo(left);
			break;
			case KeyCode.RightArrow:
				moveTo(right);
			break;
		}
	}
	public void moveTo(DungeonOptionButton target)
	{
		selectedButton = target;
		RectTransform rt =  GetComponent<RectTransform>();
		//rt.anchorMin = new Vector2(0,0.5f);
		rt.SetParent(target.GetComponent<RectTransform>());
		
		rt.offsetMax = new Vector2(0,0);
		rt.offsetMin = new Vector2(0,0);
	}
	
	
	Image image;
	bool isPressing= false;
	public void pressButton()
	{
		if(!isPressing)
		{
			isPressing = true;
			image.DOFade(0,0.05f).SetLoops(4,LoopType.Yoyo).OnComplete(()=>{isPressing = false;});
			if(selectedButton.index ==0 )
				de.confirm();
			else
				de.cancel();
		}
			
	}
	DungeonEvent de;
	DungeonOptionButton selectedButton;
	public void showPanel(DungeonEvent de)
	{
		moveTo(selectedButton);
		this.de = de;
		left.text.text = de.confirmText;
		right.text.text = de.cancelText;

		panel.DOAnchorPos(Vector2.zero,0.5f,true);
	}
	public void hidePanel()
	{
		panel.DOAnchorPos(new Vector2(0,-panel.sizeDelta.y),0.5f,true);
	}
}