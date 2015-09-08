using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class Popup : FadeInOut {
	public static Popup instance;
	public Text titleText;
	public Text descriptionText;
	public Button okButton{get{return GetComponentInChildren<Button>();}}
	
	void Awake(){
		if(instance != null){
			Debug.Log("Une instance de Popup est déja présente sur la scène");
			return;
		}
		instance = this;
		okButton.onClick.AddListener(Hide);
	}
	
	public void SetTitleText(string title){
		titleText.text = title;
	}
	
	public void SetDescriptionText(string description){
		descriptionText.text = description;
	}

	public void ShowWith(string title,string description,params UnityAction[] actionsOnClick){
		SetTitleText(title);
		SetDescriptionText(description);
		foreach(UnityAction ua in actionsOnClick)
			okButton.onClick.AddListener(ua);
		Show ();
	}
}
