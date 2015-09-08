using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(LayoutElement))]
public class AnimatedLayoutElement : MonoBehaviour {

	private LayoutElement layoutElement {get{return GetComponent<LayoutElement>();}}
	
	public float speed = 100f;
	private float minHeight;
	
	void Start () {
		minHeight = layoutElement.minHeight;
		StartCoroutine(In ());
	}
	
	public IEnumerator In(){
		layoutElement.minHeight = 0f;
		while(layoutElement.minHeight < minHeight){
			layoutElement.minHeight += Time.deltaTime * speed;
			yield return null;
		}
		layoutElement.minHeight = minHeight;
	}
	
	public IEnumerator Out(){
		while(layoutElement.minHeight > 0){
			layoutElement.minHeight -= Time.deltaTime * speed;
			yield return null;
		}
	}
}
