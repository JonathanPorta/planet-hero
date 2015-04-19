using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetManager : MonoBehaviour {

	public GameObject[] planetPrefabs;

	private List<GameObject> planets;

	void Awake(){
		// Register events
		//EventManager.DestroyPlanet += OnDestroyPlanet;
	}

	void Start(){
		this.planets = new List<GameObject>();
		
		// TODO: Replace this when it's time to add multiple planets
		GameObject planet = Instantiate(planetPrefabs[0], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		// Inject ref to the manager instance.
		planet.GetComponent<PlanetController>().planetManager = this;
		this.planets.Add(planet);
		EventManager.TriggerSelectPlanet(planet);
	}
	
	// Update is called once per frame
	void Update(){
	
	}

	public void DestroyPlanet(GameObject planet){
		Debug.Log("PlanetManager.DestroyPlanet()");
		this.planets.Remove(planet);
		Destroy(planet);
	}
}
