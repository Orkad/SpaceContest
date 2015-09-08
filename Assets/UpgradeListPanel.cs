using UnityEngine;
using System.Collections;

public class UpgradeListPanel : MonoBehaviour {
	
	void Start () {
		//InputManager.Instance.OnPlanetSelect.AddListener(Refresh);
		//InputManager.Instance.OnDeselect.AddListener(Clear);
	}

	void Refresh(Planet p){
		//On itére sur toute les Upgrades de la planète
		//foreach(Upgrade upgrade in p.upgrades)
			//AddUpgrade(upgrade);
		
		//p.BeforeBuyUpgrade.RemoveListener(AddUpgrade);
		//p.BeforeBuyUpgrade.AddListener(AddUpgrade);
	}
	
	void AddUpgrade(Upgrade upgrade){
		if(!Have(upgrade))
			UpgradeInfo.Generate(upgrade,transform);
	}
	
	private void Clear(){
		foreach(Transform child in transform){
			Destroy(child.gameObject);
		}
	}
	
	private bool Have(Upgrade upgrade){
		foreach(UpgradeInfo u in GetComponentsInChildren<UpgradeInfo>())
			if(u.upgrade.upgradeName == upgrade.upgradeName)
				return true;
		return false;
	}
}
