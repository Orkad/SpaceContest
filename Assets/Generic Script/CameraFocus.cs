using UnityEngine;
using System.Collections;

public class CameraFocus : Singleton<CameraFocus> {
	private Transform focusTransform;
	private Vector3 focusPosition;
	public Vector3 focus;
	public float damping = 5f;
	
	
	void Update(){
		if(focusTransform != null)
			focusPosition = focusTransform.position;
		focus = Vector3.Lerp(focus,focusPosition,damping);
		transform.LookAt(focus);
	}
	
	public void ChangeFocus(Transform newFocus){
		focusTransform = newFocus;
	}
	
	public void ChangeFocus(Vector3 newFocus){
		focusPosition = newFocus;
	}
}
