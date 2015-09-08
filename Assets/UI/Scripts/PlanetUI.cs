using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlanetUI : MonoBehaviour {

	public Planet reference;
	public Text planetNameText;
	public Text populationCountText;
	
	void Start(){
		//GetComponent<Image>().color = reference.owner.color;
		planetNameText.text = reference.name;
		//GetComponent<Button>().onClick.AddListener(() => InputManager.Instance.Select(reference));
	}
	
	void Update(){
		populationCountText.text = reference.population.ToString("F0");
		/*if(InputManager.Instance.selectedPlanet == reference)
			GetComponent<Button>().interactable = false;
		else
			GetComponent<Button>().interactable = true;*/
	}
}
