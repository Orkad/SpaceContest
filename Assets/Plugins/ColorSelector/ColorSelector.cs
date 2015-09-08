using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class SubmitColorEvent : UnityEvent<Color>{}

public class ColorSelector : MonoBehaviour {
	public Color color {
		get{return colorImage.color;}
		set{colorImage.color = value;
			onColorChange.Invoke(value);}
	}
	public int currentIndex = 0;
	public Image colorImage;
	public Button nextColorButton;
	public Button previousColorButton;
	public List<Color> colorList;
	public SubmitColorEvent onColorChange;

	void Awake(){
		currentIndex = FindColorIndex(colorImage.color);
		nextColorButton.onClick.AddListener(() => color = GetNextColor());
		previousColorButton.onClick.AddListener(() => color = GetPreviousColor());
	}

	Color GetNextColor(){
		if(currentIndex < colorList.Count-1)
			return colorList[++currentIndex] ;
		return colorList[currentIndex = 0];
	}

	Color GetPreviousColor(){
		if(currentIndex > 0)
			return colorList[--currentIndex] ;
		return colorList[currentIndex = colorList.Count - 1];
	}

	int FindColorIndex(Color c){
		for(int i=0;i<colorList.Count;i++)
			if(colorList[i] == c)
				return i;
		return 0;
	}
}
