using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tooltip : FadeInOut {
	public static Tooltip instance {get{return FindObjectOfType<Tooltip>();}}
	
	[SerializeField]
	private Text TitleText;
	public string title {set{TitleText.text = value;}}
	
	[SerializeField]
	private Text DescriptionText;
	public string description {set{DescriptionText.text = value;}}
	
	[SerializeField]
	private Text CostText;
	public float cost {set{CostText.text = value.ToString("F0");}}
}
