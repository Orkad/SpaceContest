using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public float speed = 20f;
	
	void Update () {
		Vector3 r = new Vector3(0,speed * Time.deltaTime,0);
		gameObject.transform.Rotate(r,Space.World);
	}
}
