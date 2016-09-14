using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
public class AnimatableCanvas : MonoBehaviour {
    
    public AnimatableGraphic[] graphics;
    public bool includeInChildren = true;
    public bool isAnimating = false;
    public float duration;
	protected virtual void Awake()
	{
        if(includeInChildren)
		graphics = GetComponentsInChildren<AnimatableGraphic>(true);
        else
        graphics = GetComponents<AnimatableGraphic>();
	}
    
    protected void loadGraphics()
    {
        if(includeInChildren)
		  graphics = GetComponentsInChildren<AnimatableGraphic>(true);
        else
          graphics = GetComponents<AnimatableGraphic>();
    }
	
    public virtual void hide()
    {
        hide(duration);
    }
	public void hide(float duration,OnCompleteDelegate completeEvent=null)
	{
        if(!isAnimating)
        {
            isAnimating = true;
            if(graphics!=null)
            {
                foreach(AnimatableGraphic graphic in graphics)
                {
                    graphic.hide(duration);
                }
                transform.GetComponent<MaskableGraphic>().DOFade(0,duration).OnComplete(
                ()=>{
                    isAnimating = false;
                    if(completeEvent!=null)
                    completeEvent();
                    gameObject.SetActive(false);
                });
            }
        }
        else{
            Debug.LogError(name+" animating");
        }
	}
	protected virtual void hideDone()
	{
        isAnimating = false;
		gameObject.SetActive(false);
	}
    public virtual void show()
    {
        show(duration);
    }
	public void show(float duration,OnCompleteDelegate completeEvent=null)
	{
        if(!isAnimating)
        {

            isAnimating = true;
            activate();
            foreach(AnimatableGraphic graphic in graphics)
            {
                graphic.show(duration);
            }
            transform.GetComponent<MaskableGraphic>().DOFade(1,duration).OnComplete(
                ()=>{
                    isAnimating = false;
                    if(completeEvent!=null)
                    completeEvent();
                    
                });
         
        }
	}
    
	protected virtual void activate()
	{
		gameObject.SetActive(true);
	}
    
   
}
public delegate void OnCompleteDelegate();