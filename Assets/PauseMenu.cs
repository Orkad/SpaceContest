using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : FadeInOut {
	public static PauseMenu instance{get{return FindObjectOfType<PauseMenu>();}}
	public Button continueButton;
	public Button backToMainMenuButton;
	
	void Start(){
		continueButton.onClick.AddListener(Hide);
		backToMainMenuButton.onClick.AddListener(BackToMainMenu);
	}
	
	void BackToMainMenu(){
		Network.Disconnect();
		Application.LoadLevel("Menu");
	}
}
