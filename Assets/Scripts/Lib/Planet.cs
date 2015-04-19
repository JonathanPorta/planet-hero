using UnityEngine;
using System.Collections;

public class Planet {
	public GameObject gameObject;
	
	public Planet(GameObject gameObject) {
		this.gameObject = gameObject;
	}
	
	public GameObject GetGameObject(){
		return this.gameObject;
	}

	public Vector3 Position(){
		return this.gameObject.transform.position;
	}
}