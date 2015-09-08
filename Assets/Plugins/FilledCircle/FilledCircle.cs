using UnityEngine;
using System.Collections;

public class FilledCircle : MonoBehaviour {
	public Color32 color;
	public float radius;

	public void Update(){
		GetComponent<Renderer>().material.color = color;
		//Selon l'axe Y
		transform.localScale = new Vector3(radius*2,0,radius*2);
	}
	
	public static void CreateFilledCircle(float _radius,Color _color,byte _colorAlpha = 100,Transform parent = null,FilledCircle prefab = null){
		//Charge le prefab par defaut
		if(prefab == null)
			prefab = Resources.Load<FilledCircle>("FilledCircle");
		FilledCircle instance = Instantiate<FilledCircle>(prefab);
		instance.radius = _radius;
		instance.color = _color;
		instance.color.a = _colorAlpha;
		instance.transform.SetParent(parent,false);
	}
}
