using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseMenuManager : SingletonCanvas<PauseMenuManager>{
		public override void show(OnCompleteDelegate completeEvent=null)
		{
			base.show(completeEvent);
			PausePanel.instance.show();
		}
		public AnimatableCanvas cover;
		public void Cover(OnTransitionDelegate transitionDelegate)
		{
			hide();
			cover.show(()=>
			{
				print("Transition ok");
				transitionDelegate(null);
			});
		}
		public void Transition(OnTransitionDelegate transitionDelegate)
		{
			hide();
			cover.show(()=>
			{
				print("Transition ok");
				transitionDelegate(cover.hide);
			});
		}
		public void Transition(OnCompleteDelegate delegateFunc)
		{
			hide();
			cover.show(()=>
			{
				print("Transition ok");
				delegateFunc();
				cover.hide(1,null);
			});
		}
}

public delegate void OnTransitionDelegate(OnCompleteDelegate d);