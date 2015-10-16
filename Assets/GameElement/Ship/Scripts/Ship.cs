using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class Ship : GameElement,IDamageable{
	public NavMeshAgent agent{get{return GetComponent<NavMeshAgent>();}}

    public bool canColonise;
	public string description;
    public float buildTime;
    public float maxHealth;
    public float range;
    public float attackSpeed;
    public float populationCost;
    public float materialCost;
    public Projectile projectilePrefab;

    [SyncVar]
	public float health;
	private float timeBeforeNextAttack;
    private Transform target;
	
    #region IDamageable

    void Explode()
    {

    }

    public void Damage(float amount){
		health -= amount;
		if(health < 0)
			Explode();
	}
	
	public bool IsDead(){return false;}
	
	public void Heal(float amount){
		health += amount;
		if(health >= maxHealth)
			health = maxHealth;
	}
	
	#endregion

    [ServerCallback]
    void Update()
    {
        if (target)
            agent.destination = target.position;
    }

    [Command]
    private void CmdAttack(NetworkInstanceId id)
    {
        Projectile.Shoot(transform, ClientScene.FindLocalObject(id).transform, projectilePrefab);
        timeBeforeNextAttack = attackSpeed;
    }

    [Command]
    void CmdMoveToGameElement(NetworkInstanceId id)
    {
        target = ClientScene.FindLocalObject(id).transform;
    }

    [Command]
    void CmdMoveToPosition(Vector3 pos)
    {
        agent.destination = pos;
        target = null;
    }
	
	public override void Action (GameElement gameElement)
	{
        if (hasAuthority)
        {
            if (gameElement.IsMine())
                CmdMoveToGameElement(gameElement.netId);
            else
                CmdAttack(gameElement.netId);
        }
            
    }
	
	public override void Action (Vector3 position)
	{
        if (hasAuthority)
            CmdMoveToPosition(position);
	}
	
	#region Search Function
	
	private GameObject GetClosestGameObject(GameObject[] gameObjects){
		float range = Mathf.Infinity;
        GameObject closest = null;
		foreach (GameObject go in gameObjects)
		{
			float dist = Vector3.Distance(go.transform.position, transform.position);
			if (dist < range)
			{
				closest = go;
				range = dist;
			}
		}
		return closest;
	}
	
	public IDamageable[] DamageableEnemiesInRange(){
		Collider[] objects = Physics.OverlapSphere(transform.position,range);
		List<IDamageable> list = new List<IDamageable>();
		foreach(Collider col in objects){
			IDamageable damageable = col.GetComponent<IDamageable>();
			if(damageable != null)
				list.Add(damageable);
		}
		return list.ToArray();
	}
	
	#endregion
}
