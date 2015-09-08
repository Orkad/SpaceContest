using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour {

	public GameObject around;
	public float speed;
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.RotateAround(around.transform.position, Vector3.up, speed * Time.deltaTime);
	}
}
