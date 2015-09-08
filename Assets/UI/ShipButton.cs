using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShipButton : ButtonBehaviour{
	public Ship ship;
	
	public override bool interactableCondition{get{return false;}}
	
	public override void SetTooltipValue (){
		Tooltip.instance.title = ship.name;
		Tooltip.instance.description = ship.description;
		Tooltip.instance.cost = ship.cost;
	}
	
	public override void OnClickEffect (){
		//InputManager.Instance.selectedPlanet.BuildShip(ship);
	}
}
