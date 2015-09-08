using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlanetInformation : FadeInOut {
	public Planet focusedPlanet;
	public ProgressBar populationProgressBar;
	public ProgressBar industryPrograssBar;
	public Text planetNameText;
	public Text temperatureText;
	public Text productionText;
	
	//ON GAME START
	void Start () {
		//InputManager.Instance.OnPlanetSelect.AddListener((Planet p) => Show(p));
		//InputManager.Instance.OnDeselect.AddListener(() => Hide());
	}
	
	//ON PLANET SELECTION
	void Show(Planet p){
		planetNameText.text = p.planetName;
		focusedPlanet = p;
		Show ();
	}
	
	
	//EACH FRAME
	protected override void UpdateShow (){
		populationProgressBar.maxValue = focusedPlanet.maxPopulation;
		populationProgressBar.value = focusedPlanet.population;
		industryPrograssBar.maxValue = focusedPlanet.maxIndustry;
		industryPrograssBar.value = focusedPlanet.industry;
		//productionText.text = "+" + focusedPlanet.populationProduction.ToString("F2");
		temperatureText.text = focusedPlanet.temperature.ToString("F0") + "C°";
		//if(focusedPlanet.owner)
			;//populationProgressBar.fillColor = industryPrograssBar.fillColor = focusedPlanet.owner.color.WithAlpha(0.5f);
	}
}
