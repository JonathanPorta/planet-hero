using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {
	
	public float distanceMin;
	public float distanceMax;
	public float zoomSpeed;

	private Vector3 moveDirection;

	void Start(){
		this.moveDirection = Vector3.zero;
	}

	void Update(){
		
		moveDirection = new Vector3(0, 0, Input.GetAxis("Mouse ScrollWheel"));
		moveDirection *= zoomSpeed;

		if(Input.GetAxis("Mouse ScrollWheel") > 0){
			transform.Translate(moveDirection, Space.Self);
		}
		else if(Input.GetAxis("Mouse ScrollWheel") < 0){
			transform.Translate(moveDirection, Space.Self);
		}
	}
}