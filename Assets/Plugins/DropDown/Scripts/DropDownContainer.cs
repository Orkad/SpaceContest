using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DropDownContainer : FadeInOut {
	public GameObject dropDownElementPrefab;
	public Transform content;
	
	public void Start(){
		OnShow.AddListener(() => Unwrap());
		OnHide.AddListener(() => Wrap ());
		
	}
	
	public void AddItem(string text){
		GameObject obj = Instantiate<GameObject>(dropDownElementPrefab);
		obj.transform.SetParent(content,false);
		obj.GetComponentInChildren<Text>().text = text;
		obj.GetComponent<Button>().onClick.AddListener(() => DropDown.activeDropDown.text = text);
		obj.GetComponent<Button>().onClick.AddListener(() => Hide());
		obj.GetComponent<LayoutElement>().preferredHeight = DropDown.activeDropDown.rectTransform.sizeDelta.y;
	}
	
	void Unwrap(){
		//Destruction des boutons du container
		foreach(Transform child in content)
			Destroy(child.gameObject);
		//On positionne le container en dessous du dropdown
		transform.position = DropDown.activeDropDown.transform.position;
		//La largeur du container sera la meme que celle du dropdown.
		rectTransform.sizeDelta = new Vector2(DropDown.activeDropDown.rectTransform.sizeDelta.x,rectTransform.sizeDelta.y);
		//On récupère la liste des strings du dropdown
		foreach(string text in DropDown.activeDropDown.stringList){
			AddItem(text);
		}
	}
	
	void Wrap(){
		DropDown.activeDropDown = null;
	}
}
