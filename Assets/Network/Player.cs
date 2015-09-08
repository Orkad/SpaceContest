using UnityEngine;
using UnityEngine.Networking;


public class Player:NetworkBehaviour{
	//Static
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
	
	[SyncVar]
	public short playerId;
	[SyncVar]
	public string playerName;
	
	public Color color{get{return isLocalPlayer ? myColor : enemyColor;}}
	public bool isMe(){return isLocalPlayer;}
	
	public override void OnStartLocalPlayer (){
		me = this;
		CmdSendIdentity(Data.playerName);
		//CmdSpawnGameElement("Coloniser",playerName);
		CmdSpawnGameElement("Viper",playerName);
	}
	
	public void Update(){
		name = playerName;
	}
	
	[Command]
	void CmdSendIdentity(string name){
		playerName = name;
		playerId = (short)netId.Value;
	}
	
	[Command]
	void CmdSpawnGameElement(string prefabName,string pName){
		GameObject gameElement = Instantiate(NetworkManager.singleton.spawnPrefabs.Find(s => s.name == prefabName));
		if(!gameElement)
			return;
		gameElement.GetComponent<GameElement>().owner = (short)netId.Value;
		NetworkServer.Spawn(gameElement);
	}
}