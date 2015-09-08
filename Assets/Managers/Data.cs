using UnityEngine;

public static class Data{
	// Player DATA
	public static string playerName;
	public static Color playerColor;
	
	// Sound DATA
	public static float volume;
	
	public static string gameType = "Default Game Type";
	public static int maxPlayer = 2;
	public static int gamePort = 25002;
	//GAME PREFS
	public static string gameName = "Default Game Name";
	public static int totalPlanet = 25;
	
	static Data(){
		playerName = PlayerPrefs.GetString("PlayerName","Name");
		playerColor = PlayerPrefsX.GetColor("PlayerColor",Color.red);
		volume = PlayerPrefs.GetFloat("Volume",1f);
		totalPlanet = PlayerPrefs.GetInt("totalPlanet",9);
	}
	
	public static void SaveData(){
		PlayerPrefs.SetString("PlayerName",playerName);
		PlayerPrefsX.SetColor("PlayerColor",playerColor);
		PlayerPrefs.SetFloat("Volume",volume);
	}
}