using UnityEngine;
using UnityEngine.Networking.Match;
using System.Collections;
using UnityEngine.UI;

public class GameRow : MonoBehaviour {
	public Button button{get{return GetComponent<Button>();}}
	public Text gameNameText;
	public Text playerCountText;
	public Text pingText;
	private Ping ping;
	public HostData associatedHostData;
	
	public void Set(MatchDesc match){
		gameNameText.text = match.name;
		playerCountText.text = match.currentSize.ToString() + "/" + match.maxSize.ToString();
	}
}
