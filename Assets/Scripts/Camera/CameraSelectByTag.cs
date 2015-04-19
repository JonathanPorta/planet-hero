using UnityEngine;
using System.Collections;

public class CameraSelectByTag : MonoBehaviour {

	// Use this for initialization
	void Start() {
	
	}
	
	// Update is called once per frame
	void Update() {
		if(Input.GetMouseButtonDown(0)){
			RaycastHit hit;
			//LayerMask mask = LayerMask.NameToLayer("Planet_Selectable");
			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000)){
				GameObject go = hit.transform.gameObject;
				if(go.tag == "Planet_Selectable"){
					EventManager.TriggerSelectPlanet(hit.transform.gameObject);
				}
				else {
					Debug.Log(string.Format("Found a GO with tag: {0}", go.tag));
				}
			}
		}
	}
}
