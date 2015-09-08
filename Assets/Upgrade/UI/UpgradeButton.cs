using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button),typeof(Image))]
public abstract class ButtonBehaviour : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler{
	public Button button{get{return GetComponent<Button>();}}
	public Image image{get{return GetComponent<Image>();}}
	public abstract bool interactableCondition{get;}
	public abstract void SetTooltipValue();
	public abstract void OnClickEffect();
	
	void Awake(){
		button.onClick.AddListener(() => OnClickEffect());
	}
	
	void Update () {
		if(interactableCondition)
			button.interactable = true;
		else
			button.interactable = false;
	}
	
	
	public void OnPointerEnter (PointerEventData eventData){
		SetTooltipValue();
		Tooltip.instance.Show();
	}
	
	public void OnPointerExit (PointerEventData eventData){
		Tooltip.instance.Hide();
	}
}


public class UpgradeButton : ButtonBehaviour {
	public Upgrade upgrade;
	public override bool interactableCondition{get{
			return false;//InputManager.Instance.selectedPlanet != null && upgrade.GetCost(InputManager.Instance.selectedPlanet) <= InputManager.Instance.selectedPlanet.population;
	}}
	
	void Start(){
		image.sprite = upgrade.icon;
		image.color = upgrade.iconColor;
	}
	
	public override void SetTooltipValue (){
		Tooltip.instance.title = upgrade.upgradeName;
		Tooltip.instance.description = upgrade.description;
		//Tooltip.instance.cost = upgrade.GetCost(InputManager.Instance.selectedPlanet);
	}
	
	public override void OnClickEffect (){
		//InputManager.Instance.selectedPlanet.AddUpgradeOnNetwork(upgrade);
	}
}