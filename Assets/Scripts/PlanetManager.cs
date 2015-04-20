using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetManager : MonoBehaviour {

	public GameObject[] planetPrefabs;

	private List<GameObject> planets;

	void Awake(){
		this.planets = new List<GameObject>();
	}

	void Start(){
		GameObject planet = CreatePlanet(planetPrefabs[0], new Vector3(0, 0, 0));
		EventManager.TriggerSelectPlanet(planet);
	}
	
	// Update is called once per frame
	void Update(){
	
	}

	public GameObject CreatePlanet(GameObject prefab, Vector3 position){
		// TODO: Replace this when it's time to add multiple planets
		GameObject planet = Instantiate(prefab, position, Quaternion.identity) as GameObject;
		planet.transform.parent = transform;
		// Inject ref to the manager instance.
		planet.GetComponent<PlanetController>().planetManager = this;
		this.planets.Add(planet);
		return planet;
	}

	public void DestroyPlanet(GameObject planet){
		Debug.Log("PlanetManager.DestroyPlanet()");
		this.planets.Remove(planet);
		Destroy(planet);
	}
}
