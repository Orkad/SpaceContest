using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class MoveForward : MonoBehaviour {
	Rigidbody rb{get{return GetComponent<Rigidbody>();}}
	
	// Update is called once per frame
	void FixedUpdate () {
		rb.AddRelativeForce(new Vector3(0f,0f,10f));
	}
}
