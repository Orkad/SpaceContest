using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup),typeof(RectTransform))]
public class FadeInOut : MonoBehaviour{
	protected RectTransform rectTransform{get{return GetComponent<RectTransform>();}}
	protected CanvasGroup canvasGroup{get{return GetComponent<CanvasGroup>();}}
	public bool show = false;
	
	public float transitionDuration = 0.2f;
	public UnityEvent OnShow;
	public UnityEvent OnHide;
	
	protected virtual void UpdateShow(){}
	private void Update(){
		if(show){
			UpdateShow();
			canvasGroup.alpha +=  Time.deltaTime / transitionDuration;
		}
		else
			canvasGroup.alpha -=  Time.deltaTime / transitionDuration;
		canvasGroup.blocksRaycasts = canvasGroup.interactable =  IsShown();
		canvasGroup.alpha = Mathf.Clamp01(canvasGroup.alpha);
	}
	
	public void StartHidden(){
		canvasGroup.alpha = 0f;
		show = false;
	}
	
	public void StartShown(){
		canvasGroup.alpha = 1f;
		show = true;
	}
	
	public void Show(){
		BeforeShow();
		show = true;
		OnShow.Invoke();
	}
	
	protected virtual void BeforeShow(){}
	
	public void Hide(){
		BeforeHide();
		show = false;
		OnHide.Invoke();
	}
	
	protected virtual void BeforeHide(){}
	
	public void Toogle(){
		if(IsShown())
			Hide();
		else
			Show();
	}
	
	public bool IsHidden(){
		return canvasGroup.alpha <= 0f;
	}
	
	public bool IsShown(){
		return canvasGroup.alpha >= 1f;
	}
}