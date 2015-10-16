using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;

public enum PlanetMod
{
    NONE,       // valeur par défaut, aucun effet
    FEED,       // NOURRIR : +++population --eau
    DRILL,      // FORRER : ++matière +eau -population
    BUILD,      // CONSTRUIRE : +vaisseau ---matière -population -temps
    RESEARCH,   // RECHERCHER : +amélioration -temps
    DEFENCE     // DEFENSE : réduit les pertes de population lors d'affrontements
}

public class Planet : GameElement,IDamageable{

    #region Properties
	
	/// <summary>
    /// Nom de la planete
    /// </summary>
	[SyncVar]
	public string planetName;

    /// <summary>
    /// Eau présente sur la planete
    /// </summary>
    [SyncVar]
    public float water;

    /// <summary>
    /// Quantité d'eau maximale pouvant être stockée
    /// </summary>
    [SyncVar]
    public float maxWater;
	
	/// <summary>
    /// Population présente sur la planete
    /// </summary>
	[SyncVar]
	public float population;

    /// <summary>
    /// Population maximale sur la planete
    /// </summary>
    [SyncVar]
    public float maxPopulation;

    /// <summary>
    /// Matière présente sur la planete
    /// </summary>
    [SyncVar]
    public float rawMaterial;

    /// <summary>
    /// Quantité maximale de matière possible sur la planete
    /// </summary>
    [SyncVar]
    public float maxRawMaterial;

    /// <summary>
    /// Le mode de production de la planete
    /// </summary>
    [SyncVar]
    public PlanetMod planetMod = PlanetMod.NONE;

    //SHIP BUILD
    public Ship shipToBuildPrefab;
    public float timerShipToBuild;

    /// <summary>
    /// Temperature de la planete
    /// </summary>
    public float temperature{get{return 0f;}}

    #endregion

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
            case PlanetMod.BUILD:
                ShipBuildUpdate();
                break;
            case PlanetMod.RESEARCH:
            case PlanetMod.DEFENCE:
            case PlanetMod.NONE:
                break;
        }
        water = Mathf.Clamp(water, 0f, maxWater);
        population = Mathf.Clamp(population, 0f, maxPopulation);
        rawMaterial = Mathf.Clamp(rawMaterial, 0f, maxRawMaterial);
    }

    

    [ServerCallback]
    public void ShipBuildUpdate()
    {
        if (shipToBuildPrefab)
        {
            timerShipToBuild -= Time.deltaTime;
            if (timerShipToBuild <= 0f)
                NetworkServer.SpawnWithClientAuthority(shipToBuildPrefab.gameObject, connectionToClient);
        }
    }

    [Command]
    public void CmdBuyShip(string shipName)
    {
        if (SpaceContestNetworkManager.IsPrefabExists<Ship>(shipName))
        {
            Ship shipPrefab = SpaceContestNetworkManager.GetPrefab<Ship>(shipName);
            //IF CAN BUY SHIP
            if (population >= shipPrefab.populationCost && rawMaterial >= shipPrefab.materialCost)
            {
                population -= shipPrefab.populationCost;
                rawMaterial -= shipPrefab.materialCost;
                shipToBuildPrefab = shipPrefab;
                RpcBuyShip(shipName);
            }
        }
    }

    [ClientRpc]
    public void RpcRemoveShipToBuild()
    {
        shipToBuildPrefab = null;
    }

    [ClientRpc]
    public void RpcBuyShip(string shipName)
    {
        shipToBuildPrefab = SpaceContestNetworkManager.GetPrefab<Ship>(shipName);
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
