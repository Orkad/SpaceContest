using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class JoinMenu : Menu {
	public GameRow gameRowPrefab;
	public RectTransform GameListContainer;
	public MatchDesc selectedMatch;
	public Button joinButton;
	
	void Awake(){
		joinButton.onClick.AddListener(() => SpaceContestNetworkManager.JoinMatch(selectedMatch.networkId));
	}
	
	protected override void OnStack (){
		DestroyMatchList();
		SpaceContestNetworkManager.RefreshMatchList();
	}
	
	protected override void BeforeShow (){
		if(NetworkManager.singleton.matches == null)
			return;
		foreach(MatchDesc m in NetworkManager.singleton.matches){
			GameRow gameRow = Instantiate<GameRow>(gameRowPrefab);
			gameRow.Set (m);
			gameRow.transform.SetParent(GameListContainer);
			gameRow.button.onClick.AddListener(() => selectedMatch = m);
		}
	}

	void DestroyMatchList(){
		foreach(Transform t in GameListContainer)
			Destroy(t.gameObject);
	}
}
