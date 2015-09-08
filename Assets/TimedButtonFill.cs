using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimedButtonFill : MonoBehaviour {
	public Image image;
	public Button button;
	public Text textTime;
	public bool useText = false;
	
	private float savedTimer;
	private float timer = 0f;
	private bool runTimer = false;
	
	void Awake(){
		image.type = Image.Type.Filled;
		image.fillAmount = 1f;
		if(useText)
			textTime.text = "";
		button.onClick.AddListener(() => PeriodicDesactivation(10f));	
		//PeriodicDesactivation(10f);
	}
	
	public void PeriodicDesactivation(float time,float startPercent = 0f){
		savedTimer = time;
		timer = 0f;
		Desactivate();
	}
	
	private void Activate(){
		button.interactable = true;
		image.fillAmount = 1f;
		runTimer = false;
	}
	
	private void Desactivate(){
		button.interactable = false;
		runTimer = true;
	}
	
	void Update () {
		if(runTimer){
			if(timer < savedTimer){
				timer += Time.deltaTime;
				image.fillAmount = timer / savedTimer;
			}
			else
				Activate();
		}
	}
}
