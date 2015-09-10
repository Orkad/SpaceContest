using UnityEngine;
using UnityEngine.UI;

public class JoinMenu : Menu {
    public InputField ipInputField;
    public Button joinButton;
	
	void Awake()
    {
        ipInputField.text = "127.0.0.1";
        joinButton.onClick.AddListener(() => TryJoin(ipInputField.text));
    }

    void TryJoin(string ip)
    {
        SpaceContestNetworkManager.JoinMatch(ip);
    }
}
