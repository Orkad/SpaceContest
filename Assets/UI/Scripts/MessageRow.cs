using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageRow : MonoBehaviour {
	public Text senderText;
	public Text messageText;

	public void Init(string sender,string message){
		senderText.text = sender + " : ";
		messageText.text = message;
	}

	void OnDisconnectedFromServer(){ //Network Element
		Destroy(gameObject);
	}
}
