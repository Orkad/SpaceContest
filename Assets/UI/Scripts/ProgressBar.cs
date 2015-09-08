using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
	public Text display{get{return GetComponentInChildren<Text>();}}
	public RectTransform rectTransform{get{return GetComponent<RectTransform>();}}
	public RectTransform fill;
	public Color fillColor{set{fill.GetComponent<Image>().color = value;}}
	public float value;
	public float minValue;
	public float maxValue;
	public string endString;
	public string floatToStringParam = "F0";
	public bool disableText = false;
	public bool showMaxValueText = false;
	
	
	void Start(){
		if(disableText)
			display.text = "";
	}

	protected void Update () {
		value = Mathf.Clamp(value,minValue,maxValue);
		if(!disableText){
			display.text = value.ToString(floatToStringParam);
			if(showMaxValueText)
				display.text += "/" + maxValue.ToString(floatToStringParam);
			display.text += endString;
		}
		fill.sizeDelta = new Vector2(rectTransform.sizeDelta.x *MathExt.Ratio(value,minValue,maxValue),fill.sizeDelta.y);
	}
}
