using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour {
	public Text playerNameText;
	public Image[] playerColorImage;
	
	void Start(){
		playerNameText.text = Player.me.playerName;
		foreach(Image img in playerColorImage){
			img.color = Player.me.color.WithAlpha(0.5f);
		}
	}
}
