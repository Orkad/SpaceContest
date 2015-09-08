using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : Singleton<MenuManager> {
	public Menu firstMenu;
	private Stack<Menu> menuStack = new Stack<Menu>();

	void Start () {
		firstMenu.Show();
		menuStack.Push(firstMenu);
	}
	
	public void StackMenu(Menu menu){
		if(menu == null)
			return;
		menuStack.Peek().Hide();
		menuStack.Push(menu);
		menuStack.Peek().Show();
	}
	
	public void Back(){
		menuStack.Pop().Hide();
		menuStack.Peek().Show();
	}
	
	public void ExitGame(){
		Application.Quit();
	}
}
