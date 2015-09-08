using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sun : Singleton<Sun> {
	public float maxTemperature = 1000f;
	public float zeroTemperatureDistance = 150f;
	public float minTemperature = -273.15f;
	public float idealTemperature = 30;
	public float worstProduction = 2f;
	public float bestProduction = 5f;
	
	public float GetTemperatureByDistance(GameObject obj){
		float distance = Vector3.Distance(transform.position,obj.transform.position);
		return Mathf.Clamp(maxTemperature * (zeroTemperatureDistance - distance) / zeroTemperatureDistance,minTemperature,Mathf.Infinity);
	}
	
	public float GetProductionByTemperature(float temperature){
		float minToIdealRatio = MathExt.Ratio(temperature,minTemperature,idealTemperature);
		float idealToMaxRatio = MathExt.Ratio(temperature,idealTemperature,maxTemperature);
		if(temperature > idealTemperature)
			return MathExt.Evaluate(idealToMaxRatio,bestProduction,worstProduction);
		return MathExt.Evaluate(minToIdealRatio,worstProduction,bestProduction);;
	}
}
