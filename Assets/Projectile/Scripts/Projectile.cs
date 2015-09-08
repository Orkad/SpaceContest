using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public interface IEntity{
	Transform transform{get;}
	GameObject gameObject{get;}
	float radius{get;}
}

public interface IOwnable:IEntity{
	short owner{get;}
}

public interface IDamageable:IOwnable{
	void Damage(float amount);
	bool IsDead();
}

public interface ISelectable{
	void OnSelect();
	void OnDeselect();
}


public static class IExtension{
	public static bool CanAttack(this IOwnable ownable,IDamageable damageable,float range){
		return damageable != null && !damageable.IsMine() && !damageable.IsDead() 
		&& Vector3.Distance(ownable.transform.position,damageable.transform.position) - damageable.radius - ownable.radius < range;
	}
	
	public static bool IsMine(this IOwnable ownable){
		if(Player.me.playerId == ownable.owner)
			return true;
		return false;
	}
	
	public static bool IsNeutral(this IOwnable ownable){
		if(ownable.owner == 0)
			return true;
		return false;
	}
}

public class Projectile : NetworkBehaviour{
	public Transform dynamicTarget;
	public Vector3 fixedTarget;
	public float damages;
	public float explosionRadius;
	public float speed;
	public GameObject explosionPrefab;
	private bool hit = false;
	
	#region NetworkBehaviour
	
	void Update ()
	{
		if(hit)
			Destroy(gameObject);
		if(dynamicTarget != null)
			fixedTarget = dynamicTarget.position;
		transform.position = Vector3.MoveTowards(transform.position,fixedTarget,speed * Time.deltaTime);
		if(transform.position == fixedTarget)
			Destroy(gameObject);
		transform.LookAt(fixedTarget);
	}
	
	void OnCollisionEnter(Collision collision) {
		IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
		//if(damageable != null && damageable.owner == null || damageable != null && !damageable.owner)
		
			damageable.Damage(damages);
			if(explosionPrefab != null){
				foreach(Collider collider in Physics.OverlapSphere(transform.position,explosionRadius))
					//if(collider.gameObject.GetComponent<IDamageable>() != null && !collider.gameObject.GetComponent<IDamageable>())
						collider.gameObject.GetComponent<IDamageable>().Damage(damages);
				Network.Instantiate(explosionPrefab,transform.position,transform.rotation,1);
			}
			hit = true;
	}
	
	#endregion
	
	#region Constructor
	
	public static Projectile Shoot(Transform from,Transform target,Projectile prefab){
		Projectile instance = Network.Instantiate(prefab,from.position,Quaternion.identity,0) as Projectile;
		instance.dynamicTarget = target;
		return instance;
	}
	
	public static Projectile Shoot(Transform from,Vector3 target,Projectile prefab){
		Projectile instance = Network.Instantiate(prefab,from.position,Quaternion.identity,0) as Projectile;
		instance.fixedTarget = target;
		return instance;
	}
	
	#endregion
}
