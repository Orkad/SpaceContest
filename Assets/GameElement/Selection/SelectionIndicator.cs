using UnityEngine;
using System.Collections;

public class SelectionIndicator : MonoBehaviour {
	
	public void ChangeColor(Color color){
		foreach(Transform child in transform){
			child.GetComponent<Renderer>().material.color = color;
		}
	}
}
