using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class Planet : GameElement,IDamageable{
	
	//NAME
	[SyncVar]
	public string planetName;
	
	//POPULATION
	[SyncVar]
	public float maxPopulation;
	[SyncVar]
	public float population;
	[SyncVar]
	public float populationProd;
	
	//INDUSTRY
	[SyncVar]
	public float maxIndustry;
	[SyncVar]
	public float industry;
	[SyncVar]
	public float industryProd;
	
	public float temperature{get{return Sun.Instance.GetTemperatureByDistance(gameObject);}}
	
	private Vector3 GetRandomPositionAtSurface(){
		return GameManager.RandomCircle(transform.position,radius);
	}
	
	//FUNCTIONS
	public void BuildShip (Ship shipPrefab){
		
	}
	
	public void Colonise(Player p,float pop){
		//_owner = p;
		population = pop;
	}
	
	public void Damage(float amount){
		population -= amount;
	}
	
	public bool IsDead(){
		return population <= 0f;
	}
	
	//STATIC
}
