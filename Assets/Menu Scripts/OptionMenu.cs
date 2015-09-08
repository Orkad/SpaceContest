using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class OptionMenu : Menu {
	public InputField playerNameInput;
	public ColorSelector playerColorSelector;
	public Slider volume;
	public DropDown resolutionDropDown;
	public Toggle fullScreenToggle;
	
	public void Awake(){
		//Player Name
		playerNameInput.text = Data.playerName;
		playerNameInput.onEndEdit.AddListener((string value) => Data.playerName = value);
		//Volume
		AudioListener.volume = volume.value = Data.volume;
		volume.onValueChanged.AddListener((float value) => Data.volume = AudioListener.volume = value);
		//Resolution
		resolutionDropDown.text = Screen.currentResolution.ToCustomString();
		foreach(Resolution res in Screen.resolutions){
			resolutionDropDown.stringList.Add(res.ToCustomString());
		}
		resolutionDropDown.onValueChanged.AddListener((string value) => SetResolution(value.ToResolution()));
		//Fullscreen
		fullScreenToggle.isOn = Screen.fullScreen;
		fullScreenToggle.onValueChanged.AddListener((bool value) => Screen.fullScreen = value);
	}
	
	protected override void OnBack(){
		resolutionDropDown.container.Hide();
		Data.SaveData();
	}
	
	public static void SetResolution(Resolution resolution){
		Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen,resolution.refreshRate);
	}
}