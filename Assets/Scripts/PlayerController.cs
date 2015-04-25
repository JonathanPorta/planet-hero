using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 10.0f;
	public GameObject characterPrefab;

	private WeaponController weaponController;
	private GameObject character;
	private GameObject planet;
	private Orbit playerOrbit;

	void Awake(){
		// Register events
		EventManager.SelectPlanet += SetPlanet;
	}

	void Start(){

	}

	void Update(){
		// If we aren't on a planet or don't have a character, bail.
		if (planet == null || character == null) { return ;}
		
		playerOrbit.center = CalculateCenterOfGravity();
		playerOrbit.SetAngleInDegrees(playerOrbit.GetAngleInDegrees() + speed * Input.GetAxis("Horizontal"));
		transform.position = playerOrbit.CalculatePosition();
		transform.LookAt(playerOrbit.center);
		 
		if(Input.GetButton("Jump")){
			Firing();
		}
	}

	private void Firing(){
		weaponController.Fire();
	}

	private void SetPlanet(GameObject selection){
		planet = selection;
		transform.parent = planet.transform;
		Debug.Log("SETTING NEW PLANET!");

		// Setup orbit based on selected planet
		float orbitHeight = planet.GetComponent<SphereCollider>().radius * planet.transform.localScale.x + characterPrefab.transform.localScale.x;
		playerOrbit = new Orbit(CalculateCenterOfGravity(), orbitHeight, 0.0f, 10.0f);
		
		if(character == null){
			//this.character = Instantiate(characterPrefab, playerOrbit.CalculatePosition(), Quaternion.identity) as GameObject;
			character = Instantiate(characterPrefab, transform.position, transform.rotation) as GameObject;
		}
		character.transform.parent = transform;
		weaponController = transform.GetComponentInChildren<WeaponController>();
	}

	private Vector3 CalculateCenterOfGravity(){
		return planet.transform.position;
	}
}