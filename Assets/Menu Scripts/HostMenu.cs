using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections;
using UnityEngine.UI;

public class HostMenu : Menu {
	public InputField gameNameInput;
	public DropDown maxPlayerInput;
	public Button OkButton;
	
	void Awake(){
		OkButton.onClick.AddListener(() => CreateMatch());
		gameNameInput.text = Data.gameName;
		gameNameInput.onEndEdit.AddListener((string str) => Data.gameName = str);
		maxPlayerInput.text = Data.maxPlayer.ToString();
		maxPlayerInput.onValueChanged.AddListener((string str) => Data.maxPlayer = int.Parse(str));
	}
	
	void CreateMatch(){
		SpaceContestNetworkManager.HostMatch();
	}
}
