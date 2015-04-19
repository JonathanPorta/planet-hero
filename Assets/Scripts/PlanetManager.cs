using UnityEngine;
using System.Collections;

public class PlanetManager : MonoBehaviour {

	public GameObject[] planetPrefabs;

	// Use this for initialization
	void Start () {
		Vector3 center = new Vector3(0, 0, 0);
		GameObject planetGameObject = Instantiate(planetPrefabs [0], center, Quaternion.identity) as GameObject;

		// Set the starting planet.
		EventManager.TriggerSelectPlanet(planetGameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
