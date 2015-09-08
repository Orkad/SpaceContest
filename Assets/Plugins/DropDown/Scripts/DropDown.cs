using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class SubmitEvent : UnityEvent<string>{}


[RequireComponent(typeof(Button))]
public class DropDown:MonoBehaviour{
	public RectTransform rectTransform{get{return GetComponent<RectTransform>();}}
	public static DropDown activeDropDown;
	public string text{get{return GetComponentInChildren<Text>().text;}
		set{
			GetComponentInChildren<Text>().text = value;
			onValueChanged.Invoke(value);
		}
	}
	public Button button{
		get{return GetComponent<Button>();}
	}
	public DropDownContainer container;
	public List<string> stringList;
	public SubmitEvent onValueChanged;
	
	void Start () {
		button.onClick.AddListener(() => activeDropDown = this);
		button.onClick.AddListener(() => container.Toogle());
	}
}
