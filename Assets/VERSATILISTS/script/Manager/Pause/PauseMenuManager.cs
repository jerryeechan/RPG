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
		public void Transition(OnTransitionDelegate transitionDelegate)
		{
			hide();
			cover.show(()=>
			{
				print("Transition ok");
				transitionDelegate(cover.hide);
			});
		}
}

public delegate void OnTransitionDelegate(OnCompleteDelegate d);