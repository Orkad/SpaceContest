using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;


[System.Serializable]
public class FloatEvent:UnityEvent<float>{}

public class SelectionManager : Singleton<SelectionManager> {
	public RectTransform multipleSelectionRectangle;
	
	#region NEW Select
	public UnityEvent EventSelect;
	public UnityEvent EventDeselect;
	public List<GameElement> selection;
	
	public void Deselect(){
		if(selection.Count != 0){
			EventDeselect.Invoke();
			selection.ForEach(x => x.OnDeselect());
			selection.Clear();
		}
	}
	
	public void Select(GameElement element){
		Deselect();
		if(!element)
			return;
		element.OnSelect();
		selection.Add(element);
		EventSelect.Invoke();
	}
	
	public void Select(List<GameElement> elements){
		Deselect();
		if(elements.Count == 0)
			return;
		elements.ForEach(e => e.OnSelect());
		selection.AddRange(elements);
		EventSelect.Invoke();
	}
	
	#endregion
	
	private GameElement hoverGameElement;
	
	public enum MouseButton:int{left,right,middle}
	
	void Start(){
		
	}
	
	void Update () {
		hoverGameElement = MouseRaycast<GameElement>();
		//Keyboard
		if(Input.anyKeyDown)
			OnKeyDown();
		
		//Mouse
		if(Input.GetMouseButtonDown((int)MouseButton.left))
			OnPressLeftMouseButton();
		else if(Input.GetMouseButton((int)MouseButton.left))
			OnDragLeftMouseButton();
		else if(Input.GetMouseButtonUp((int)MouseButton.left))
			OnReleaseLeftMouseButton();
		
		if(Input.GetMouseButtonDown((int)MouseButton.right))
			OnPressRightMouseButton();
		else if(Input.GetMouseButton((int)MouseButton.right))
			OnDragRightMouseButton();
		else if(Input.GetMouseButtonUp((int)MouseButton.right))
			OnReleaseRightMouseButton();
			
		if(Input.GetMouseButtonDown((int)MouseButton.middle))
			OnPressMiddleMouseButton();
		else if(Input.GetMouseButtonUp((int)MouseButton.middle))
			OnReleaseMiddleMouseButton();
	}
	
	#region Left Mouse Button
	
	private Vector2 leftMouseButtonStartDrag;
	private float minDragDistance = 10f;
	public bool isDragingLeftMouseButton{get{return (Vector2.Distance((Vector2)Input.mousePosition,leftMouseButtonStartDrag) > minDragDistance);}}
	
	void OnPressLeftMouseButton(){
		leftMouseButtonStartDrag = Input.mousePosition;
	}
	
	void OnDragLeftMouseButton(){
		if(isDragingLeftMouseButton){	
			multipleSelectionRectangle.gameObject.SetActive(true);
			Rect screenRect = CreateScreenRect(leftMouseButtonStartDrag,Input.mousePosition);
			multipleSelectionRectangle.anchoredPosition = new Vector2(screenRect.xMin,screenRect.yMin);
			multipleSelectionRectangle.sizeDelta = new Vector2(screenRect.width,screenRect.height);
		}
	}
	
	void OnReleaseLeftMouseButton(){
		multipleSelectionRectangle.gameObject.SetActive(false);
		//No Drag
		if(!isDragingLeftMouseButton){
			if(MouseHoverUI())
				return;
			Select(hoverGameElement);
		}
		//Drag
		else{
			Select(ScreenRectCast<GameElement>(CreateScreenRect(leftMouseButtonStartDrag,Input.mousePosition)));
		}
	}
	
	#endregion
	
	#region Right Mouse Button
	
	void OnPressRightMouseButton(){
		if(hoverGameElement)
			selection.ForEach(x => x.Action(hoverGameElement));
		else
			selection.ForEach(x => x.Action(MousePositionOnPlane()));
	}
	
	void OnDragRightMouseButton(){
		
	}
	
	void OnReleaseRightMouseButton(){

	}
	
	#endregion
	
	#region Midle Mouse Button
	
	void OnPressMiddleMouseButton(){
		
	}
	
	void OnReleaseMiddleMouseButton(){
	}
	
	#endregion
	
	#region Keyboard
	
	void OnKeyDown(){
		if(Input.GetKeyDown(KeyCode.Escape))
			PauseMenu.instance.Toogle();
	}
	
	#endregion
	
	#region Static Functions
	
	private static Vector3 MousePositionOnPlane(){
		Plane p = new Plane(Vector3.zero,Vector3.forward,Vector3.right);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float distance;
		if(p.Raycast(ray,out distance))
			return ray.GetPoint(distance);
		return Vector3.zero;
	}
	
	public static T MouseRaycast<T>()where T:class{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // créé le ray correspondant de la main camera vers la position de ta souris
		RaycastHit hit;
		if(Physics.Raycast(ray,out hit)) // si le raycast rencontre quelque chose
			if(hit.collider.GetComponent<T>() != null) // si le raycast trouve un objet du type T
				return hit.collider.GetComponent<T>(); // retourne l'objet T
		return null;
	}
	
	private static bool MouseHoverUI(){
		return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
	}
	
	private static List<T> ScreenRectCast<T>(Rect screenRect)where T:MonoBehaviour{
		List<T> objectsInside = new List<T>();
		foreach(T obj in FindObjectsOfType<T>()){
			if(obj.GetComponent<Renderer>().isVisible){
				if(screenRect.Contains((Vector2)Camera.main.WorldToScreenPoint(obj.transform.position)))
					objectsInside.Add(obj);
				}
		}
		return objectsInside;
	}
	
	private static Rect CreateScreenRect(Vector2 v1,Vector2 v2){
		if(v1.x < v2.x)
			if(v1.y < v2.y)
				return new Rect(v1.x,v1.y,v2.x - v1.x,v2.y - v1.y);
			else
				return new Rect(v1.x,v2.y,v2.x - v1.x,v1.y - v2.y);
		else
			if(v1.y < v2.y)
				return new Rect(v2.x,v1.y,v1.x - v2.x,v2.y - v1.y);
			else
				return new Rect(v2.x,v2.y,v1.x - v2.x,v1.y - v2.y);
	}
	#endregion
}
