using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LobbyChat : MonoBehaviour {
	public Transform content;
	private MessageRow messageRowPrefab {get{return Resources.Load<MessageRow>("MessageRow");}}
	private Button sendButton {get{return GetComponentInChildren<Button>();}}
	private InputField inputField {get{return GetComponentInChildren<InputField>();}}
	private NetworkView nView {get{return GetComponent<NetworkView>();}}

	void Awake(){
	}

	void Update(){

	}
}
