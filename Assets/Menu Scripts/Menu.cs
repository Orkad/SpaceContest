using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu : FadeInOut{
	public static Menu currentMenu;
	protected Menu previousMenu;
	public Button backButton;
	public bool startActive = false;
	public bool center = true;
	
	public virtual void Start(){
		StartHidden();
		if(startActive)
			Show();
		if(center)
			rectTransform.position = new Vector2(Screen.width / 2,Screen.height /2);
		if(backButton != null)
			backButton.onClick.AddListener(() => Back());
	}
		
	public void Stack(Menu nextMenu){
		if(nextMenu == null)
			return;
		currentMenu = nextMenu;
		Hide();
		nextMenu.previousMenu = this;
		nextMenu.Show();
		nextMenu.OnStack();
	}
	
	public static void StaticStack(Menu nextMenu){
		currentMenu.Stack(nextMenu);
	}
	
	public void Back(){
		if(previousMenu == null){
			Application.Quit();
			return;
		}
		currentMenu = previousMenu;
		OnBack();
		Hide();
		previousMenu.Show();
		
	}
	
	protected virtual void OnBack(){}
	protected virtual void OnStack(){}
}
