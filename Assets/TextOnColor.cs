using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextOnColor : MonoBehaviour {
	public string text {set{GetComponentInChildren<Text>().text = value;}}
	public Color color {set{GetComponentInChildren<Image>().color = value;}}
}
