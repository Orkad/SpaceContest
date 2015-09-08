using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PlayerStatRow : MonoBehaviour {
	public Player refPlayer;
	
	public Image background{get{return GetComponent<Image>();}}
	public Text playerNameText;
	public Text planetCountText;
	
	void Start(){
		playerNameText.text = refPlayer.playerName;
		background.color = refPlayer.color;
	}
}
