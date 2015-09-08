using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class SphericalGravity : MonoBehaviour {
	public Rigidbody rb{get{return GetComponent<Rigidbody>();}}
	public bool fixedPosition = false;
	private static List<SphericalGravity> actors = new List<SphericalGravity>();
	public Vector3 initialForce;
	
	// Use this for initialization
	void Start () {
		//Enregistre l'objet dans la liste des acteurs
		actors.Add(this);
		rb.AddForce(initialForce);
	}
	
	// Update is called once per frame
	void Update () {
		if(fixedPosition)
			return;
		foreach(SphericalGravity actor in actors){
			if(actor != this){
				rb.AddForce((actor.transform.position - transform.position).normalized * actor.rb.mass / rb.mass);
			}
		}
	}
	
	
}
	
