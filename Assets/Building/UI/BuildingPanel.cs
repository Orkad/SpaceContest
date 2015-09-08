using UnityEngine;
using System.Collections;

namespace UI{

	public class BuildingPanel : MonoBehaviour {
		
		public void Start(){
			
		}
		
		public void Refresh(){
			
		}
		
		private void RemoveChildrens(){
			foreach(Transform t in transform){
				Destroy(t.gameObject);
			}
		}
	}
}


