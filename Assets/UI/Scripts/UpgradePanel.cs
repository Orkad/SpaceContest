using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpgradePanel : FadeInOut {
	
	void Start(){
		canvasGroup.alpha = 0;
		//InputManager.Instance.OnPlanetSelect.AddListener((Planet p) => Show (p));
		//InputManager.Instance.OnDeselect.AddListener(() => Hide());
	}
	
	void Show(Planet p){
		if(p.IsMine()){
			Show();
		}	
	}
}
