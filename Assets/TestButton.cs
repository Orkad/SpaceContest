using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TestButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(() => (SelectionManager.Instance.selection[0] as Planet).ChangeOwner(Player.me));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
