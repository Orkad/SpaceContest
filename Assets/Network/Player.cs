using UnityEngine;
using UnityEngine.Networking;


public class Player:NetworkBehaviour{
	
	public static readonly Color enemyColor = Color.red;
	public static readonly Color neutralColor = Color.yellow;
	public static readonly Color myColor = Color.green;
	public static Player me;
	
	public static Player FindPlayerByName(string pName){
		Player[] players = FindObjectsOfType<Player>();
		foreach(Player p in players)
			if(p.playerName == pName)
				return p;
		return null;
	}

    public static Player FindPlayerByID(uint id)
    {
        Player[] players = FindObjectsOfType<Player>();
        foreach (Player p in players)
            if (p.playerId == id)
                return p;
        return null;
    }
	
	[SyncVar]
	public string playerName;
	public uint playerId { get { return netId.Value; } }

	public Color color{get{return isLocalPlayer ? myColor : enemyColor;}}
	public bool isMe(){return isLocalPlayer;}
	
	public override void OnStartLocalPlayer (){
		me = this;
		CmdSendIdentity(Data.playerName);
		CmdSpawnGameElement("Viper");
	}
	
	public void Update(){
		name = playerName;
	}
	
	[Command]
	void CmdSendIdentity(string name){
		playerName = name;
	}
	
	[Command]
	void CmdSpawnGameElement(string prefabName){
		GameObject gameElement = Instantiate(NetworkManager.singleton.spawnPrefabs.Find(s => s.name == prefabName));
		if(!gameElement)
			return;
		gameElement.GetComponent<GameElement>().owner = netId.Value;
        NetworkServer.SpawnWithClientAuthority(gameElement, gameObject);
	}
}