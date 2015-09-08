using UnityEngine;
using System.Collections;

public class Farm : Building {
	public Planet planet;
	
	const float percent = 30f;
	
	void Start(){
		planet = GetComponentInParent<Planet>();
		if(!planet)
			Destroy(gameObject);
	}
	
	void Update () {
		if(level == 0)
			return;
		planet.population += percent * level;
	}
}
