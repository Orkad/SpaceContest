using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class Planet : GameElement,IDamageable{

    public enum PlanetMod
    {
        NONE,       // valeur par défaut, aucun effet
        FEED,       // NOURRIR : +++population --eau
        DRILL,      // FORRER : ++matière ++eau --population
        WORK,       // TRAVAILLER : ++ressource -matière -population
        BUILD,      // CONSTRUIRE : +vaisseau ---ressource -population -temps
        RESEARCH,   // RECHERCHER : +amélioration -temps
        DEFENCE     // DEFENSE : réduit les pertes de population lors d'affrontements
    }
	
	//NOM
	[SyncVar]
	public string planetName;

    //EAU
    [SyncVar]
    public float water;
    [SyncVar]
    public float maxWater;
	
	//POPULATION
	[SyncVar]
	public float population;
    [SyncVar]
    public float maxPopulation;

    //MATIERE
    [SyncVar]
    public float rawMaterial;
    [SyncVar]
    public float maxRawMaterial;

    //RESSOURCES
    [SyncVar]
    public float resources;
    [SyncVar]
	public float maxResources;

    //MODE DE PLANETE
    [SyncVar]
    public PlanetMod planetMod = PlanetMod.NONE;
	
	//FUNCTION
    public float temperature
    {
        get
        {
            return 0f;
        }
    }
    
    //COMMAND
    [Command]
    public void CmdChangePlanetMod(PlanetMod mod)
    {
        planetMod = mod;
    }

    //SERVER START
    [ServerCallback]
    void Start()
    {
        water = maxWater = 100f;
        rawMaterial = maxRawMaterial = 100f;
        maxPopulation = 100f;
        maxResources = 100f;
    }

    //SERVER UPDATE
    [ServerCallback]
    void Update()
    {
        float unit = Time.deltaTime;
        switch (planetMod)
        {
            case PlanetMod.FEED:
                population += 3 * unit;
                water -= 2 * unit;
                break;
            case PlanetMod.DRILL:
                rawMaterial += 2 * unit;
                water += 2 * unit;
                population -= unit;
                break;
            case PlanetMod.WORK:
                resources += 2 * unit;
                rawMaterial -= 2 * unit;
                population -= unit;
                break;
            case PlanetMod.BUILD:
                ShipBuildProcess();
                break;
            case PlanetMod.RESEARCH:
            case PlanetMod.DEFENCE:
            case PlanetMod.NONE:
                break;
        }
        water = Mathf.Clamp(water, 0f, maxWater);
        population = Mathf.Clamp(population, 0f, maxPopulation);
        rawMaterial = Mathf.Clamp(rawMaterial, 0f, maxRawMaterial);
        resources = Mathf.Clamp(resources, 0f, maxResources);
    }

    //SHIP BUILD
    public Ship currentShipToBuild;
    public float shipToBuildTimer;

    public void ShipBuildProcess()
    {
        if (currentShipToBuild)
        {
            shipToBuildTimer -= Time.deltaTime;
            if (shipToBuildTimer <= 0f)
                NetworkServer.SpawnWithClientAuthority(currentShipToBuild.gameObject,GetPlayerOwner());
        }
    }

    public bool CanBuy(Ship ship)
    {
        if (!ship)
            return false;
        return population >= ship.populationCost && resources >= ship.resourceCost;
    }

    public void Buy(Ship ship){
        population -= ship.populationCost;
        resources -= ship.resourceCost;
        currentShipToBuild = ship;
    }

    [Command]
    public void CmdAddShipToBuild(string shipPrefabName)
    {
        currentShipToBuild = NetworkManager.singleton.spawnPrefabs.Find(x => x.name == shipPrefabName).GetComponent<Ship>();
        if (currentShipToBuild)
            shipToBuildTimer = currentShipToBuild.buildTime;
    }

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

    public Vector3 GetRandomPositionAtSurface()
    {
        return GameManager.RandomCircle(transform.position, radius);
    }
}
