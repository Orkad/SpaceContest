using UnityEngine;
using System.Collections;

public class ShipPanel : FadeInOut {
	void Start(){
		//InputManager.Instance.OnPlanetSelect.AddListener(Show);
		//InputManager.Instance.OnDeselect.AddListener(Hide);
	}
	
	void Show(Planet p){
		if(p.IsMine())
			Show ();
	}
}
