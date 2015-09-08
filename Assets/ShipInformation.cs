using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShipInformation : FadeInOut {
	public ProgressBar healthBar;
	public Text shipNameText;
	public Ship focusedShip;
	
	void Start () {
		canvasGroup.alpha = 0;
		//InputManager.Instance.OnShipSelect.AddListener((Ship ship) => Show(ship));
		//InputManager.Instance.OnDeselect.AddListener(() => Hide());
	}
	
	public void Show(Ship ship){
		focusedShip = ship;
		shipNameText.text = ship.name;
		healthBar.maxValue = ship.maxHealth;
		//healthBar.fillColor = ship.owner.color.WithAlpha(0.5f);
		Show();
	}
	
	protected override void UpdateShow ()
	{
		healthBar.value = focusedShip.health;
	}
}
