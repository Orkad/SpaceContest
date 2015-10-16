using UnityEngine;
using UnityEngine.Networking;

public interface IDamageable
{
    void Damage(float amount);
}

public class Projectile : NetworkBehaviour{

	public float damages;
	public float explosionRadius;
	public float speed;
	public GameObject explosionPrefab;

    // Game Variables
	private bool hit = false;
    private Transform target;
    private Vector3 fixedTarget;

    [ServerCallback]
    void Update ()
	{
		if(hit)
			Destroy(gameObject);
		if(target != null)
			fixedTarget = target.position;
		transform.position = Vector3.MoveTowards(transform.position,fixedTarget,speed * Time.deltaTime);
		if(transform.position == fixedTarget)
			Destroy(gameObject);
		transform.LookAt(fixedTarget);
	}
	
	void OnCollisionEnter(Collision collision) {
        IDamageable element = collision.gameObject.GetComponent<IDamageable>();
        element.Damage(damages);
			if(explosionPrefab != null){
				foreach(Collider collider in Physics.OverlapSphere(transform.position,explosionRadius))
					//if(collider.gameObject.GetComponent<IDamageable>() != null && !collider.gameObject.GetComponent<IDamageable>())
						collider.gameObject.GetComponent<IDamageable>().Damage(damages);
				Network.Instantiate(explosionPrefab,transform.position,transform.rotation,1);
			}
			hit = true;
	}
	
	#region Constructor
	
	public static Projectile Shoot(Transform from,Transform target,Projectile prefab){
		Projectile instance = Network.Instantiate(prefab,from.position,Quaternion.identity,0) as Projectile;
		instance.target = target;
		return instance;
	}
	
	public static Projectile Shoot(Transform from,Vector3 target,Projectile prefab){
		Projectile instance = Network.Instantiate(prefab,from.position,Quaternion.identity,0) as Projectile;
		instance.fixedTarget = target;
		return instance;
	}
	
	#endregion
}
