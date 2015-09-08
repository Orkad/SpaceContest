using UnityEngine;
using System.Collections;

public class SetParentOnStart : MonoBehaviour {
	public string parentName;
	// Use this for initialization
	void Start () {
		transform.SetParent(GameObject.Find(parentName).transform);
	}
}
