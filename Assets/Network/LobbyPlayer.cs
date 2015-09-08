using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class LobbyPlayer : NetworkLobbyPlayer {
	public static LobbyPlayer me;
	public static Color notReadyColor = Color.yellow;
	public static Color readyColor = Color.green;
	public Text playerNameText;
	public Image image;
	public Image checkBoxReady;
	
	[SyncVar]public string playerName;

	public override void OnStartLocalPlayer ()
	{
		me = this;
		CmdSendIdentity(Data.playerName);
	}
	
	public void Start(){
		image.color = notReadyColor;
	}
	
	public void Update(){
		playerNameText.text = playerName;
		if(readyToBegin)
			image.color = readyColor;
		else
			image.color = notReadyColor;
	}
	
	[Command]
	void CmdSendIdentity(string name){
		playerName = name;
	}
}
