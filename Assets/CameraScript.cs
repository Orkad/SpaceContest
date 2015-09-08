using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
///  CameraScript établie la position de la caméra en fonction de son target, elle n'a aucune dépendance
/// </summary>
public class CameraScript: Singleton<CameraScript> {
	//FOCUS
	private GameElement target;
	private Vector3 smoothTargetPosition;
	[Range(1f,5f)]
	public float moveDamping = 2f;
	
	//ZOOM
	private float trueDistance;
	private float distance = 60f;
	private float minDistance = 30f;
	private float maxDistance = 100f;
	[Range(0.1f,1f)]
	public float zoomSpeed = 0.5f;
	[Range(1f,5f)]
	public float minDistanceRatio = 2f;
	[Range(10f,50f)]
	public float maxDistanceRatio = 20f;
	
	//ORBIT
	public float yMinLimit = -20f;
	public float yMaxLimit = 80f;
	public float xMouseSensibility = 120f;
	public float yMouseSensibility = 120f;
	
	private float x;
	private float y;
	
	void Start(){
		SelectionManager.Instance.EventSelect.AddListener(() => RefreshTarget());
	}
	
	void Update(){
		if(target)
			smoothTargetPosition = Vector3.Lerp(smoothTargetPosition,target.transform.position,moveDamping*Time.deltaTime);
	}
	
	void LateUpdate () {
		if(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.Mouse2)){
			x += Input.GetAxis("Mouse X") * xMouseSensibility * 0.02f;
			y -= Input.GetAxis("Mouse Y") * yMouseSensibility * 0.02f;
			y = ClampAngle(y, yMinLimit, yMaxLimit);
		}
			
		Quaternion rotation = Quaternion.Euler(y, x, 0);
		trueDistance = Mathf.Clamp(trueDistance - Input.GetAxis("Mouse ScrollWheel")*distance*zoomSpeed, minDistance, maxDistance);
		distance = Mathf.Lerp(distance,trueDistance,moveDamping*Time.deltaTime);
		
		Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
		Vector3 position = rotation * negDistance + smoothTargetPosition;
		
		transform.rotation = rotation;
		transform.position = position;
	}
	
	public void RefreshTarget(){
		target = SelectionManager.Instance.selection[0];
		if(target){
			minDistance = target.radius * minDistanceRatio;
			maxDistance = target.radius * maxDistanceRatio;
			trueDistance = target.radius * (minDistanceRatio + maxDistanceRatio) /2f;
		}
	}
	
	public static float ClampAngle(float angle, float min, float max){
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}
}
