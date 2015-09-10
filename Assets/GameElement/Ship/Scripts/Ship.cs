using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;


[RequireComponent(typeof(NavMeshAgent))]
public class Ship : GameElement,IDamageable{
	public NavMeshAgent agent{get{return GetComponent<NavMeshAgent>();}}
	
	//SHIP NAME
	[SyncVar]
	public string shipName;
	
	public bool canColonise{get{return settlers > 0f;}}
	public string description;
    public float buildTime;
    public float populationCost;
    public float resourceCost;
	
	[SyncVar]
	public float health;
	public float maxHealth;
	public float range;
	public float attackSpeed;
	private float timeBeforeNextAttack;
	public Projectile projectilePrefab;
	
	public float cost; //Le cout industriel du vaisseau
	public float settlers; //Colons si > 0 devient un vaisseau de colonisation

    public Transform target;
	
	private void Attack(IDamageable enemy){
		Projectile.Shoot(transform,enemy.transform,projectilePrefab);
		timeBeforeNextAttack = attackSpeed;
	}

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
            CmdMoveToGameElement(gameElement.netId);
    }
	
	public override void Action (Vector3 position)
	{
        if (hasAuthority)
            CmdMoveToPosition(position);
	}
	
	#region Search Function
	
	private IDamageable GetClosestDamageable(IDamageable[] damageables){
		float range = Mathf.Infinity;
		IDamageable closest = null;
		foreach (IDamageable damageable in damageables)
		{
			float dist = Vector3.Distance(damageable.transform.position, transform.position);
			if (dist < range)
			{
				closest = damageable;
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
			if(damageable != null && this.CanAttack(damageable,range))
				list.Add(damageable);
		}
		return list.ToArray();
	}
	
	#endregion
}
