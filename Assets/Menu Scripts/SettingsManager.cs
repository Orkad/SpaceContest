using UnityEngine;
using System.Collections;

public class SettingsManager:Singleton<SettingsManager>{

	public string playerName;
	public bool music;
	
	public void SetPlayerName(string newPlayerName){
		playerName = newPlayerName;
		PlayerPrefs.SetString("playerName",playerName);
	}
	
	public void SetMusic(bool value){
		music = value;
		PlayerPrefs.SetString("music",music.ToString());
	}
	
	void Start(){
		Debug.Log(music.ToString());
		playerName = PlayerPrefs.GetString("playerName");
		if(PlayerPrefs.GetString("music") == "False")
			music = false;
		else
			music = true;
	}
}
