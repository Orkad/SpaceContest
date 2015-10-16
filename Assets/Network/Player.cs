using UnityEngine;
using UnityEngine.Networking;


public class Player:NetworkBehaviour{
	
	public static readonly Color enemyColor = Color.red;
	public static readonly Color neutralColor = Color.yellow;
	public static readonly Color myColor = Color.green;
	public static Player me;

    /// <summary>
    /// Fonction de recherche de joueur par connexion
    /// </summary>
    /// <param name="conn">Connexion recherchée</param>
    /// <returns></returns>
    public static Player FindPlayer(NetworkConnection conn)
    {
        Player[] players = FindObjectsOfType<Player>();
        foreach (Player p in players)
            if (p.connectionToServer == conn)
                return p;
        return null;
    }
	
	[SyncVar]
	public string playerName;

	public Color color{get{return isLocalPlayer ? myColor : enemyColor;}}
	public bool isMe(){return isLocalPlayer;}
	
	public override void OnStartLocalPlayer (){
		me = this;
		CmdIdentity(Data.playerName);
		CmdSpawnGameElement("Viper");
	}
	
	public void Update(){
		name = playerName;
	}
	
	[Command]
	void CmdIdentity(string name){
		playerName = name;
	}
	
	[Command]
	void CmdSpawnGameElement(string prefabName){
		GameObject gameElement = Instantiate(NetworkManager.singleton.spawnPrefabs.Find(s => s.name == prefabName));
		if(!gameElement)
			return;
        NetworkServer.SpawnWithClientAuthority(gameElement, connectionToClient);
	}
}