using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Laser : NetworkBehaviour {

	public Transform origin;
	public Transform target;
	public float speed;
	public float damages;
	public GameObject explosionPrefab;
	
	#region NetworkBehaviour
	
	void Update ()
	{
		if(target == null)
			Explode();
		transform.position = Vector3.MoveTowards(transform.position,target.transform.position,speed * Time.deltaTime);
		transform.LookAt(target.transform);
	}
	
	#endregion
	
	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject == target.gameObject){
			if(true){
				ApplyDamages(collision.gameObject.GetComponent<IDamageable>());
			}
		}
	}
	
	private void ApplyDamages(IDamageable damageable){
		damageable.Damage(damages);
		ParticleSystem particles = GetComponentInChildren<ParticleSystem>();
		if(particles != null){
			particles.transform.SetParent(null);
			particles.enableEmission = false;
			particles.gameObject.AddComponent<AutoDestroy>().timer = 10f;
		}
		Explode();
	}
	
	private void Explode(){
			
	}
	
	public static Laser FireLaserOverNetwork(Transform from,Transform to,float spd,float dmg,Laser prefab = null){
		//Charge le prefab par defaut
		if(prefab == null)
			prefab = Resources.Load<Laser>("Laser");
		Laser instance = Network.Instantiate(prefab,from.position,Quaternion.identity,0) as Laser;
		instance.origin = from;
		instance.target = to;
		instance.speed = spd;
		instance.damages = dmg;
		return instance;
	}
}
