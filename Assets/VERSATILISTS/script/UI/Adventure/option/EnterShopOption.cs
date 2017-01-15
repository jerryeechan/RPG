using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterShopOption : AdventureOption{
	public override void choose()
	{
		this.myInvoke(1,
			()=>{
				PauseMenuManager.instance.Transition(()=>{
					chooseEvent();
				});
			});	
	}
	
	
	public void chooseEvent()
	{
		ShopUIManager.instance.init();
		ShopUIManager.instance.show();
	}
	
}
