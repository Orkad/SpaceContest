using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class Line:MonoBehaviour{
	private LineRenderer LR{get{return GetComponent<LineRenderer>();}}
	public Transform dynamicFrom;
	public Transform dynamicTo;
	public Vector3 fixedFrom;
	public Vector3 fixedTo;
	
	#region Init
	
	public void Init(Transform from,Transform to){
		dynamicFrom = from;
		dynamicTo = to;
	}
	
	public void Init(Transform from,Vector3 to){
		dynamicFrom = from;
		fixedTo = to;
	}
	
	public void Init(Vector3 from,Transform to){
		fixedFrom = from;
		dynamicTo = to;
	}
	
	public void Init(Vector3 from,Vector3 to){
		fixedFrom = from;
		fixedTo = to;
	}
	
	#endregion
	
	void Update(){
		Vector3 from;
		Vector3 to;
		if(dynamicFrom != null)
			from = dynamicFrom.position;
		else
			from = fixedFrom;
		if(dynamicTo != null)
			to = dynamicTo.position;
		else
			to = fixedTo;
		LR.SetPosition(0,from);
		LR.SetPosition(1,to);
	}
	
	public static Line DrawLine(Transform From,Transform To,Color materialColor,Line prefab = null){
		Line l = TryInstantiate(prefab);
		l.Init(From,To);
		l.GetComponent<Renderer>().material.color = materialColor;
		return l;
	}
	
	public static Line DrawLine(Transform From,Vector3 To,Color materialColor,Line prefab = null){
		Line l = TryInstantiate(prefab);
		l.Init(From,To);
		l.GetComponent<Renderer>().material.color = materialColor;
		return l;
	}
	
	private static string defaultResourceName = "Line";
	private static Line InstantiateDefaultResource(){
		return Instantiate<Line>(Resources.Load<Line>(defaultResourceName));
	}
	private static Line TryInstantiate(Line prefab){
		if(prefab == null)
			return InstantiateDefaultResource();
		else
			return Instantiate<Line>(prefab);
	}
}
