using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum UpgradeEffect{
	PopulationProductionPCT,
	ShipCostPCT,
	DamageTakenPCT,
	MaxPopulationPCT,
	IncTemperatureFlat
}

public class Upgrade : MonoBehaviour {
	public Planet planet{get{return GetComponentInParent<Planet>();}}
	public string upgradeName;
	public string description{get{return GetDescription(value);}}
	public Sprite icon;
	public Color iconColor;
	public UpgradeEffect effect;
	public float value;
	public float baseCost;
	public float incFlatCostPerCount; //Per upgrade count
	public float incPctCostPerCount;
	public float GetCost(Planet p){return 0;}
	
	public bool Equals (Upgrade u){
		return upgradeName == u.upgradeName;
	}
	
	public string GetDescription(float value){
		switch (effect){
		case UpgradeEffect.PopulationProductionPCT:
			return "Augmente la production de population de " + "<color=#008000ff>" + value.ToString() + "%</color>";
		case UpgradeEffect.ShipCostPCT:
			return "Réduit le cout de construction des vaisseaux de " + "<color=#008000ff>" + value.ToString() + "%</color>";
		case UpgradeEffect.DamageTakenPCT:
			return "Réduit les dégat subit de " + "<color=#008000ff>" + value.ToString() + "%</color>";
		case UpgradeEffect.MaxPopulationPCT:
			return "Augmente la capacitée maximale de population de " + "<color=#008000ff>" + value.ToString() + "%</color>";
		case UpgradeEffect.IncTemperatureFlat:
			if(value > 0)
				return "Augmente la temperature de " + "<color=#008000ff>" + value.ToString() + "C°</color>";
			else
				return "Reduit la temperature de " + "<color=#008000ff>" + (-value).ToString() + "C°</color>";
		}
		return "";
	}
}
