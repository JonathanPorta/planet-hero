using UnityEngine;
using System.Collections;

[RequireComponent (typeof (HitpointController))]

public class PlanetController : MonoBehaviour {

	public PlanetManager planetManager;
	public HitpointController hitpointController;

	void Awake(){
		this.hitpointController = transform.GetComponent<HitpointController>();
		// Set handlers for the hitpoint related events that we care about.
		hitpointController.OnDeath += HandleDeath;
	}

	void Start(){

	}

	void Update(){

	}

	private void HandleDeath(){
		Debug.Log("PlanetController.HandleDeath()");
		planetManager.DestroyPlanet(this.gameObject);
	}
}