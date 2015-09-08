using UnityEngine;
using System.Collections;

public class Circle : MonoBehaviour {
	
	
	//Center - Le centre du cercle
	//Radius - Le Rayon du cercle
	//Color - La couleur de la ligne qui compose le cercle
	//Thickness - L'épaisseur de la ligne qui compose le cercle
	//Definition - Le nombre de points, plus ce nombre est élevé plus le cercle sera lisse
	public static Circle Draw(Vector3 Center,float Radius,Color color,float thickness,int definition){
		GameObject gameObject = new GameObject("Circle");
		Circle instance = gameObject.AddComponent<Circle>();
		
		LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.SetColors(color, color);
		lineRenderer.SetWidth(0.2F, 0.2F);
		lineRenderer.SetVertexCount(definition);
		
		int i = 0;
		for(float theta = 0f; theta < 2 * Mathf.PI; theta += 0.1f) {
			float x = Radius*Mathf.Cos(theta);
			float z = Radius*Mathf.Sin(theta);
			
			Vector3 pos = new Vector3(x, 0, z) + Center;
			lineRenderer.SetPosition(i, pos);
			i+=1;
		}
		return instance;
	}
}
