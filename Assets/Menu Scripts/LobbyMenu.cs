using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class LobbyMenu : Menu{
	public static LobbyMenu instance{get{return FindObjectOfType<LobbyMenu>();}}
	public Button readyButton;
	public Button launchButton;
	public Transform playerRowContainer;
	public Text gameNameText;
	
	
	public override void Start(){
		base.Start();
		readyButton.onClick.AddListener(() => SetLobbyPlayerReady());
	}
	
	public void SetLobbyPlayerReady(){
		LobbyPlayer.me.SendReadyToBeginMessage();
		readyButton.interactable = false;
	}
	
	protected override void OnBack(){
		NetworkManager.singleton.StopHost();
	}
	
	protected override void OnStack (){
		gameNameText.text = NetworkManager.singleton.matchHost;
	}
}