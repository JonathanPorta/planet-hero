using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 10.0f;
	public GameObject characterPrefab;

	private GameObject character;
	private GameObject planet;
	private Orbit orbit;
	private Vector3 centerOfGravity;

	private void Awake(){
		// Register events
		EventManager.SelectPlanet += SetPlanet;
	}

	private void Start(){

	}

	private void Update(){
		// If we aren't on a planet or don't have a character, bail.
		if (planet == null || character == null) { return ;}

		orbit.SetAngleInDegrees(orbit.GetAngleInDegrees() + speed * Input.GetAxis("Horizontal"));
		character.transform.position = orbit.CalculatePosition();
		character.transform.LookAt(this.centerOfGravity);
	}

	private void SetPlanet(GameObject selection){
		// Destroy the current player object because we will instaniate a new one on the new planet
		if (character != null) {
			Destroy(character);
		}

		this.planet = selection;

		// Setup orbit based on selected planet
		centerOfGravity = this.planet.transform.position;
		float orbitHeight = this.planet.GetComponent<SphereCollider>().radius * this.planet.transform.localScale.x + this.characterPrefab.transform.localScale.x;
		this.orbit = new Orbit(centerOfGravity, orbitHeight, 0.0f, 10.0f);

		// Instantiate the characterPrefab in the given orbit
		this.character = Instantiate(characterPrefab, orbit.CalculatePosition (), Quaternion.identity) as GameObject;
	}
}