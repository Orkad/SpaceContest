using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;

public class SpaceContestNetworkManager : NetworkManager{

	//ASKER

	public static void HostMatch(){
		singleton.StartMatchMaker();
		singleton.matchMaker.CreateMatch(Data.gameName,(uint)Data.maxPlayer,true,"",OnMatchCreateResponse);
		Worker.singleton.Work("Création du match en cours");
	}
	
	public static void JoinMatch(NetworkID id,string password = ""){
		singleton.StartMatchMaker();
		singleton.matchMaker.JoinMatch(id,password,OnMatchJoinedResponse);
		Worker.singleton.Work("Connexion au serveur en cours");
	}

    public static void JoinMatch(string ip)
    {
        singleton.networkAddress = ip;
        singleton.StartClient();
        Worker.singleton.Work("Connexion au serveur (" + ip + ")");
    }
	
	public static void RefreshMatchList(){
		singleton.StartMatchMaker();
		singleton.matchMaker.ListMatches(0,10,"",OnMatchListRefresh);
		Worker.singleton.Work("Recherche de parties en cours");
	}
	
	//RESPONSE
	
	public static void OnMatchCreateResponse(CreateMatchResponse matchInfo){
		singleton.OnMatchCreate(matchInfo);
		if(!matchInfo.success){
			Worker.singleton.FailWork("Impossible de créer la partie");
		}
	}
	
	public static void OnMatchJoinedResponse(JoinMatchResponse matchInfo){
		singleton.OnMatchJoined(matchInfo);
		if(!matchInfo.success){
			//Popup.instance.ShowWith("Erreur","La partie n'existe plus");
			Worker.singleton.FailWork("La partie n'existe plus");
		}
	}
	
	public static void OnMatchListRefresh(ListMatchResponse listMatchInfo){
		singleton.OnMatchList(listMatchInfo);
		if(!listMatchInfo.success)
			Worker.singleton.FailWork("Impossible de récupérer la liste des parties");
		else if(listMatchInfo.matches.Count == 0)
			Worker.singleton.FailWork("Aucune partie disponible actuellement");
		else
			Worker.singleton.EndWork();
	}
	
	//NETWORK MANAGER
	
	public override void OnClientError (NetworkConnection conn, int errorCode)
	{
		base.OnClientError (conn, errorCode);
        Worker.singleton.FailWork("Impossible de rejoindre la partie selectionnée");
	}
	
	public override void OnClientDisconnect (NetworkConnection conn)
	{
        Worker.singleton.FailWork("Impossible de rejoindre la partie");
	}
}
