using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
public class AnimatableGraphic : MonoBehaviour {
    protected MaskableGraphic graphic;
    public float showAlpha = 1;
    public float duration = 0.5f;
    public float delay = 0;
	// Use this for initialization
    
	void Awake()
    {
        graphic = GetComponent<MaskableGraphic>();
        //setAlpha(0);
    }
    public void setAlpha(float alpha)
    {
        if(graphic==null)
        {
            graphic = GetComponent<MaskableGraphic>();
        }
        //graphic.canvasRenderer.SetAlpha(alpha);
        
        //graphic.DOFade(alpha,0);
    }
    public void showDefault()
    {
        Invoke("show",delay);
    }
    public void show(float duration)
    { 
        graphic = GetComponent<MaskableGraphic>();
        this.duration = duration;
        show();
        
    }
    public void show(float duration,float delay)
    {
        this.duration = duration;
        Invoke("show",delay);
    }
    void show()
    {
        graphic = GetComponent<MaskableGraphic>();
        if(graphic)
        {
            gameObject.SetActive(true);
            setAlpha(0);
            //print(duration);
            graphic.DOFade(showAlpha,duration);
            //graphic.CrossFadeAlpha(,duration,false);
        }
    }
    public void hide(float duration)
    {
        if(graphic)
        graphic.CrossFadeAlpha(0,duration,false);
    }
    public void hide()
    {
        if(graphic)
        graphic.CrossFadeAlpha(0,duration,false);
    }    
}
