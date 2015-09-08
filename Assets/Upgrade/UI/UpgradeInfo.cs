using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class UpgradeInfo : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {
	public Image image{get{return GetComponent<Image>();}}
	public Text textField{get{return GetComponentInChildren<Text>();}}
	public Upgrade upgrade;
	
	void Start(){
		image.sprite = upgrade.icon;
		image.color = upgrade.iconColor;
	}
	
	void Update(){
		//textField.text = InputManager.Instance.selectedPlanet.CountUpgrade(upgrade).ToString();
	}

	public void OnPointerEnter (PointerEventData eventData){
		Tooltip.instance.title = upgrade.upgradeName;
		//Tooltip.instance.description = upgrade.GetDescription(upgrade.value*InputManager.Instance.selectedPlanet.CountUpgrade(upgrade));
		Tooltip.instance.Show();
	}
	
	public void OnPointerExit (PointerEventData eventData){
		Tooltip.instance.Hide();
	}
	
	public static UpgradeInfo Generate(Upgrade u,Transform parent = null){
		GameObject obj = Instantiate<GameObject>(Resources.Load<GameObject>("IconValue2"));
		UpgradeInfo instance = obj.AddComponent<UpgradeInfo>();
		instance.upgrade = u;
		instance.transform.SetParent(parent);
		return instance;
	}
}
