using UnityEngine;
using System.Collections;

public class SmoothMovement : MonoBehaviour {
	private Transform initialParent;
	public Transform target;
	public float velocity;
	
	// Update is called once per frame
	void Update () {
		if(target != null){
			transform.position = Vector3.Lerp(transform.position,target.position,velocity*Time.deltaTime);
		}
	}
}
