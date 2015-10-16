using UnityEngine;
using UnityEngine.Networking;

public class GameManager:MonoBehaviour{
	public static GameManager singleton;
	public bool gameStarted = false;
	
	void Awake() {
		#if UNITY_EDITOR
		if(!NetworkClient.active)
			Application.LoadLevel("Menu");
		#endif
		if(singleton != null){
			Debug.LogError("Il ne peut y avoir qu'un seul GameManager");
			Destroy(gameObject);
			return;
		}
		singleton = this;
        if(NetworkServer.active)
		    GenerateSolarSystem();
	}
	
	public float distance = 150f;
	public float size = 5f;
	public float rotationSpeed = 2f; 
	[Range(10f,90f)]
	public float randomness = 50f;
	public GameObject planetPrefab;
	
	public void GenerateSolarSystem() {
		if(!planetPrefab)
			return;
		for(int i=0;i<3;i++){
			Planet planet = Instantiate<Planet>(planetPrefab.GetComponent<Planet>());
			Vector3 randomPlanetPosition = RandomCircle(Sun.Instance.transform.position,distance.RandomByPercent(randomness));
			float randomPlanetSize = size.RandomByPercent(randomness);
			
			planet.transform.position = randomPlanetPosition;
			planet.radius = randomPlanetSize;

            //Genere un nom aléatoire pour la planette créée
            planet.planetName = NameGenerator.generateName(Random.Range(3,10));
			NetworkServer.Spawn(planet.gameObject);

            //Server Extra component
            planet.gameObject.AddComponent<RotateAround>().around = Sun.Instance.gameObject;
			planet.GetComponent<RotateAround>().speed = rotationSpeed.RandomByPercent(randomness);
			planet.gameObject.AddComponent<Rotate>().speed = 20;
		}
		Debug.Log("World Generated");
	}
	
	public static Vector3 RandomCircle ( Vector3 center ,   float radius  ){
		float ang = Random.value * 360;
		Vector3 pos;
		pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
		pos.y = center.y;
		pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
		return pos;
	}

    public static Planet GetEmptyPlanet()
    {
        Planet[] pList = FindObjectsOfType<Planet>();
        foreach(Planet p in pList)
        {
            if (p.IsNeutral())
                return p;
        }
        return null;
    }

    public void StartGame()
    {
        foreach (NetworkConnection connection in NetworkServer.localConnections)
            GetEmptyPlanet().ChangeOwner(connection);
    }
}
