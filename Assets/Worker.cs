using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Worker : FadeInOut {
	public static Worker singleton;
	[Header("Object References")]
	public Text informationText;
	public Image image;
	[Header("Prefabs References")]
	public Sprite workingIcon;
	public Sprite failureIcon;
	[Header("Definitions")]
	public Color normalColor;
	public Color failureColor;

	void Awake () {
		singleton = this;
		StartHidden();
	}
	
	public void TimedHide(float sec){
		StopAllCoroutines();
		StartCoroutine(TimedHideCoroutine(sec));
	}
	
	public IEnumerator TimedHideCoroutine(float sec){
		yield return new WaitForSeconds(sec);
		Hide ();
	}
	
	public void Work(string workingText){
		Menu.currentMenu.Hide();
		image.sprite = workingIcon;
		image.color = informationText.color = normalColor;
		informationText.text = workingText;
		Show();
	}
	
	public void EndWork(){
		singleton.Hide ();
		Menu.currentMenu.Show();
	}
	
	public void FailWork(string failureText){
		Debug.Log("Fail");
		TimedHide(4f);
		image.sprite = failureIcon;
		image.color = informationText.color = failureColor;
		informationText.text = failureText;
		Menu.currentMenu.Back();
	}
}
