using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VictoryDisplay : FadeInOut {
	private Text display{get{return GetComponent<Text>();}}

	void Victory(){
		display.text = "Victoire";
		Show();
	}
	
	void Defeat(){
		display.text = "Defaite";
		Show();
	}
}
