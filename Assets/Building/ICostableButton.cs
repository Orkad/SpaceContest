using UnityEngine;
using System.Collections;

public class ICostableButton : ButtonBehaviour {
	public ICostable costableItem;
	
	public override bool interactableCondition {
		get {
			return false;
		}
	}
	
	public override void OnClickEffect (){
		costableItem.BuyEffect();
	}
	
	public override void SetTooltipValue (){
		Tooltip.instance.title = costableItem.name;
		Tooltip.instance.description = costableItem.description;
		Tooltip.instance.cost = costableItem.cost;
	}
}
