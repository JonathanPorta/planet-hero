using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 10.0f;
	public GameObject characterPrefab;

	private WeaponController weaponController;
	private GameObject character;
	private GameObject planet;
	private Orbit orbit;

	void Awake(){
		// Register events
		EventManager.SelectPlanet += SetPlanet;
	}

	void Start(){

	}

	void Update(){
		// If we aren't on a planet or don't have a character, bail.
		if (planet == null || character == null) { return ;}

		if(Input.GetButton("Jump")){
			Firing();
		}
		orbit.center = CalculateCenterOfGravity();
		orbit.SetAngleInDegrees(orbit.GetAngleInDegrees() + speed * Input.GetAxis("Horizontal"));
		character.transform.position = orbit.CalculatePosition();
		character.transform.LookAt(CalculateCenterOfGravity());
	}

	private void Firing(){
		Vector3 direction = 2.0f * character.transform.position - CalculateCenterOfGravity();
		weaponController.Fire(direction);
	}

	private void SetPlanet(GameObject selection){
		this.planet = selection;
		Debug.Log("SETTING NEW PLANET!");

		// Setup orbit based on selected planet
		float orbitHeight = this.planet.GetComponent<SphereCollider>().radius * this.planet.transform.localScale.x + this.characterPrefab.transform.localScale.x;
		this.orbit = new Orbit(CalculateCenterOfGravity(), orbitHeight, 0.0f, 10.0f);
		
		if(character == null){
			this.character = Instantiate(characterPrefab, orbit.CalculatePosition (), Quaternion.identity) as GameObject;
		}
		this.character.transform.parent = transform;
		this.weaponController = transform.GetComponentInChildren<WeaponController>();
	}

	private Vector3 CalculateCenterOfGravity(){
		return this.planet.transform.position;
	}
}