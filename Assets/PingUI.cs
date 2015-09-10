using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PingUI : MonoBehaviour {
    public Text valueText;
    public Image image;
    private NetworkClient myClient;
    private float ping;

	void Start () {
        myClient = NetworkManager.singleton.client;
    }
	
	// Update is called once per frame
	void Update () {
        ping = myClient.GetRTT();
        valueText.text = ping.ToString();
        if (ping < 70f)
            valueText.color = image.color = Color.green;
        else if (ping < 150f)
            valueText.color = image.color = Color.yellow;
        else
            valueText.color = image.color = Color.red;
	}
}
